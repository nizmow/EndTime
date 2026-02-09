using EndTime.Core.Data;
using EndTime.Core.World;
using Microsoft.Xna.Framework.Graphics;

namespace Endtime.Game;

public class Renderer
{
    private readonly TileRegistry _tileRegistry;
    private readonly Texture2D _tileAtlas;

    public Renderer(TileRegistry tileRegistry, Texture2D tileAtlas)
    {
        _tileRegistry = tileRegistry;
        _tileAtlas = tileAtlas;
    }

    public void Draw(SpriteBatch spriteBatch, GameWorld world)
    {
        for (var y = 0; y < world.Map.Height; y++)
        {
            for (var x = 0; x < world.Map.Width; x++)
            {
                var tileId = world.Map.GetTile(x, y);
                if (tileId == 0)
                    continue; // skip empty tiles

                var tile = _tileRegistry.Get(tileId);
                var position = SpriteMath.GetTilePosition(x, y);
                spriteBatch.Draw(
                    _tileAtlas,
                    position,
                    tile.Visual.Rect,
                    tile.Visual.ForegroundColour
                );
            }
        }

        foreach (var entity in world.Entities.Entities)
        {
            var position = SpriteMath.GetTilePosition(entity.X, entity.Y);

            spriteBatch.Draw(
                _tileAtlas,
                position,
                entity.EntityDefinition.Visual.Rect,
                entity.EntityDefinition.Visual.ForegroundColour
            );
        }
    }
}
