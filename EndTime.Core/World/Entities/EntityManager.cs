using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;

namespace EndTime.Core.World.Entities;

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

    // Get all entities with a given name, pretty slow.
    public ImmutableArray<Entity> GetAll(string name)
    {
        var entities = new List<Entity>();
        foreach (var e in _entities)
        {
            if (e.Value.Name == name)
            {
                entities.Add(e.Value);
            }
        }
        return [.. entities];
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D tileAtlas)
    {
        foreach (var entity in _entities.Values)
        {
            var position = SpriteMath.GetTilePosition(entity.X, entity.Y);

            spriteBatch.Draw(tileAtlas, position, entity.EntityDefinition.Visual.Rect, entity.EntityDefinition.Visual.ForegroundColour);
        }
    }
}
