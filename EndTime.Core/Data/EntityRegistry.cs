using EndTime.Core.World.Entities;

namespace EndTime.Core.Data;

public class EntityRegistry
{
    private readonly Dictionary<string, EntityDefinition> _entities = new();

    public void Register(EntityDefinition entity)
    {
        _entities[entity.Name] = entity;
    }

    public Entity Spawn(string name, int x, int y)
    {
        return new Entity { Id = Guid.NewGuid(), Name = name, EntityDefinition = _entities[name], X = x, Y = y };
    }
}
