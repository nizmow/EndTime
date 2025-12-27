using EndTime.Core.Utilities;
using Microsoft.Xna.Framework;

namespace EndTime.Core;


public record TileDefinition(
    int Id,
    string Name,
    bool IsWalkable,
    CodePage437 Symbol,
    string HexForegroundColour)
{
    private Rectangle? _sourceRect;
    public Rectangle SoureRect => _sourceRect ??= SpriteMath.GetSourceRect((int) Symbol);

    private Color? _foregroundColour;
    public Color ForegroundColour => _foregroundColour ??= ColourUtils.FromHex(HexForegroundColour);
}
