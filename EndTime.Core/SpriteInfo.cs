using EndTime.Core;
using EndTime.Core.Utilities;
using Microsoft.Xna.Framework;

public record SpriteInfo(string HexForegroundColour, CodePage437 Symbol)
{
    private Color? _foregroundColour;
    public Color ForegroundColour => _foregroundColour ??= ColourUtils.FromHex(HexForegroundColour);

    private Rectangle? _rect;
    public Rectangle Rect => _rect ??= SpriteMath.GetSourceRect((int)Symbol);
}
