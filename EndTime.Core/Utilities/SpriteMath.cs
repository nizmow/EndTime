
using Microsoft.Xna.Framework;

public static class SpriteMath
{
    public const int Width = 8;
    public const int Height = 16;
    public const int ColumnsInSheet = 16;

    public static Rectangle GetSourceRect(int spriteId)
    {
        int x = spriteId % ColumnsInSheet * Width;
        int y = spriteId / ColumnsInSheet * Height;
        return new Rectangle(x, y, Width, Height);
    }

    public static Vector2 GetTilePosition(int x, int y)
    {
        return new Vector2(x * Width, y * Height);
    }
}
