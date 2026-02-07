using EndTime.Core.Data;
using EndTime.Core.World.Entities;
using EndTime.Core.World.Map;

namespace EndTime.Core.World;

public class GameWorld
{
    private GameMap _map = null!;
    private readonly EntityManager _entityManager;
    private Entity? _player;

    public GameMap Map
    {
        get { return _map; }
        set { _map = value; }
    }

    public EntityManager Entities
    {
        get { return _entityManager; }
    }

    public GameWorld(TileRegistry tileRegistry)
    {
        _entityManager = new();
    }

    public bool IsWalkable(int x, int y)
    {
        return _map.IsWalkable(x, y);
        // TODO: check bounds, entities, etc.
    }

    public void AddEntity(Entity entity)
    {
        _entityManager.Add(entity);
    }

    public void SetPlayer(Entity entity)
    {
        _player = entity;
        _entityManager.Add(entity);
    }

    public Entity GetPlayer()
    {
        return _player ?? throw new InvalidOperationException("No player set");
    }
}
