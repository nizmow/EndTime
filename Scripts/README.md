### Font spritesheets

We need a Codepage 437 font rendered into a 16x16 sprite sheet for the uh, 'graphics' of this game. The awesome [Ultimate Oldschool PC Font Pack](https://int10h.org/oldschool-pc-fonts/) is a great resource for these, but the bitmaps are in .FON and they're not super useful. We can convert them to a sprite sheet as such:

First generate a BDF:
```
fontforge -lang=ff -c 'Open($1); Generate($2)' your_font.fon your_font.bdf
```

Use the included script to turn that into the spritesheet, `uv` is required:
```
uv run bdf_to_png.py your_font.bdf
```
