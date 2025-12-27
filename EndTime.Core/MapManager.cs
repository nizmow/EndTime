using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndTime.Core;

public class MapManager
{
    private int[,] _map;
    private readonly TileRegistry _tileRegistry;

    // Map id to tile
    // private Dictionary<int, Rectangle> _tileAtlas;

    public MapManager(int width, int height, TileRegistry tileRegistry)
    {
        _map = new int[width, height];
        _tileRegistry = tileRegistry;
    }

    public void SetTile(int x, int y, int tileId)
    {
        if (x >= 0 && x < _map.GetLength(0) && y >= 0 && y < _map.GetLength(1))
            _map[x, y] = tileId;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D tileSet)
    {
        for (var y = 0; y < _map.GetLength(1); y++)
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            {
                var tileId = _map[x, y];
                if (tileId == 0) continue; // Skip empty tiles

                var tile = _tileRegistry.Get(tileId);
                var position = SpriteMath.GetTilePosition(x, y);
            
                spriteBatch.Draw(tileSet, position, tile.Visual.Rect, tile.Visual.ForegroundColour);
            }
        }
    }

}