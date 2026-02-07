using EndTime.Core.Data;
using EndTime.Core.World.Entities;
using EndTime.Core.World.Map;

namespace EndTime.Core.World;

public class GameWorld
{
    private GameMap _map;
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
        // Useless default map just in case nobody else sets one.
        _map = new(1, 1);
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
