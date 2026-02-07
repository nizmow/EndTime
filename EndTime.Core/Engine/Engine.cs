namespace EndTime.Core.Engine;

using EndTime.Core.Data;
using EndTime.Core.Input;
using EndTime.Core.World;
using EndTime.Core.World.Map;

public class Engine(EntityRegistry _entityRegistry, TileRegistry _tileRegistry)
{
    private const int DEFAULT_WIDTH = 80;
    private const int DEFAULT_HEIGHT = 50;

    private GameWorld _world = new(_tileRegistry);

    public GameWorld World
    {
        get { return _world; }
    }

    public void Start()
    {
        var player = _entityRegistry.Spawn("player", 10, 10);
        var map = new GameMap(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        map.SetTile(40, 25, 1);
        _world.SetPlayer(player);
        _world.Map = map;
    }

    public GameStatus Update(Action playerAction)
    {
        if (playerAction is QuitAction)
        {
            return GameStatus.Quit;
        }

        var player = _world.GetPlayer();
        if (playerAction is MoveAction moveAction)
        {
            player.X += moveAction.DeltaX;
            player.Y += moveAction.DeltaY;
        }

        return GameStatus.Continue;
    }
}

public enum GameStatus
{
    Continue,
    Quit,
}
