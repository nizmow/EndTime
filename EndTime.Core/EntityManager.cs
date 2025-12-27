using System.Collections.Immutable;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace EndTime.Core;

/// <summary>
/// Manage entities.
/// 
/// Long term I think I want a 'world' and this may not be relevant anymore, but this gets stuff
/// on screen.
/// </summary>
public class EntityManager
{
    private readonly Dictionary<Guid, Entity> _entities = new();

    public IList<Entity> Entities => _entities.Values.ToImmutableList();

    public void Add(Entity entity)
    {
        _entities[entity.Id] = entity;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D tileAtlas)
    {
        foreach (var entity in _entities.Values)
        {
            var position = new Vector2(
                entity.X * SpriteMath.Width,
                entity.Y * SpriteMath.Height
            );

            spriteBatch.Draw(tileAtlas, position, entity.EntityDefinition.Visual.Rect, entity.EntityDefinition.Visual.ForegroundColour);
        }
    }
}
