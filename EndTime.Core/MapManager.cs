using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndTime.Core;

public class MapManager
{
    private int[,] _tileMap;
    private Texture2D _tileset;
    private int _tileSize;

    // Map id to tile
    private Dictionary<int, Rectangle> _tileAtlas;

    public MapManager(int width, int height, int tileSize, Texture2D tileset)
    {
        _tileMap = new int[width, height];
        _tileSize = tileSize;
        _tileset = tileset;
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        for (var y = 0; y < _tileMap.GetLength(1); y++)
        {
            for (var x = 0; x < _tileMap.GetLength(0); x++)
            {
                var tileId = _tileMap[x, y];
                if (tileId == 0) continue; // Skip empty tiles

                var position = new Vector2(x * _tileSize, y * _tileSize);
                spriteBatch.Draw(_tileset, position, _tileAtlas[tileId], Color.White);
            }
        }
    }

}