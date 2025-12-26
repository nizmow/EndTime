# /// script
# dependencies = [ "Pillow" ]
# ///

"""
BDF to Image Spritesheet Converter (Baseline Corrected)
----------------------------------
Original Logic: makutamoto (https://github.com/makutamoto/bdf-to-image)
Modified for: UV, CP437, Baseline Alignment, and Encoding Robustness.
License: MIT
"""

import sys
from PIL import Image, ImageDraw

def parse_bdf(path):
    glyphs = {}
    fbb_w = fbb_h = fbb_xoff = fbb_yoff = 0
    current_encoding = None
    in_bitmap = False

    with open(path, 'r', encoding='latin-1') as f:
        for line in f:
            if line.startswith("FONTBOUNDINGBOX"):
                parts = line.split()
                fbb_w, fbb_h = int(parts[1]), int(parts[2])
                fbb_xoff, fbb_yoff = int(parts[3]), int(parts[4])
            elif line.startswith("ENCODING"):
                current_encoding = int(line.split()[1])
            elif line.startswith("BBX"):
                parts = line.split()
                if current_encoding is not None:
                    glyphs[current_encoding] = {
                        'bbx': [int(p) for p in parts[1:]], # w, h, xoff, yoff
                        'bitmap': []
                    }
            elif line.startswith("BITMAP"):
                in_bitmap = True
            elif line.startswith("ENDCHAR"):
                in_bitmap = False
            elif in_bitmap and current_encoding is not None:
                clean_line = line.strip()
                if clean_line:
                    glyphs[current_encoding]['bitmap'].append(int(clean_line, 16))
    
    return glyphs, fbb_w, fbb_h, fbb_xoff, fbb_yoff

def create_spritesheet(bdf_path):
    glyphs, fw, fh, fx, fy = parse_bdf(bdf_path)
    
    if fw == 0 or fh == 0:
        print("Error: Could not detect font dimensions.")
        return

    # The baseline is calculated relative to the top of the cell
    # In BDF, fy is typically a negative number representing the descent
    baseline = fh + fy
    cols, rows = 16, 16
    
    # Create canvas (Transparent background)
    img = Image.new("RGBA", (cols * fw, rows * fh), (0, 0, 0, 0))
    draw = ImageDraw.Draw(img)

    for i in range(256):
        if i not in glyphs: continue
            
        col, row = i % cols, i // cols
        grid_x, grid_y = col * fw, row * fh
        
        glyph = glyphs[i]
        gw, gh, gx, gy = glyph['bbx']
        
        # Vertical placement:
        # Align the glyph's internal baseline with the cell's baseline
        y_start = baseline - (gh + gy)
        
        for y, row_data in enumerate(glyph['bitmap']):
            # BDF hex rows are padded to 8-bit boundaries
            bits_needed = ((gw + 7) // 8) * 8
            bits = bin(row_data)[2:].zfill(bits_needed)
            
            for x, bit in enumerate(bits[:gw]):
                if bit == '1':
                    draw.point((grid_x + gx + x, grid_y + y_start + y), 
                               fill=(255, 255, 255, 255))

    output = "bdf_spritesheet.png"
    img.save(output)
    print(f"Generated {output} at {fw}x{fh} per cell.")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: uv run bdf_to_png.py <font.bdf>")
    else:
        create_spritesheet(sys.argv[1])