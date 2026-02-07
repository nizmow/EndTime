using EndTime.Core.Data;

namespace EndTime.Core.World.Map;

public class GameMap(int width, int height, TileRegistry tileRegistry)
{
    private readonly int[,] _map = new int[width, height];
    private readonly TileRegistry _tileRegistry = tileRegistry;

    public int Width
    {
        get { return _map.GetLength(0); }
    }

    public int Height
    {
        get { return _map.GetLength(1); }
    }

    public void SetTile(int x, int y, int tileId)
    {
        if (x >= 0 && x < _map.GetLength(0) && y >= 0 && y < _map.GetLength(1))
        {
            _map[x, y] = tileId;
        }
    }

    public int GetTile(int x, int y)
    {
        return _map[x, y];
    }

    public bool IsWalkable(int x, int y)
    {
        var tileId = _map[x, y];
        // We should get rid of this special case and use floor tiles
        if (tileId == 0)
            return true;
        var tile = _tileRegistry.Get(tileId);
        return tile.IsWalkable;
    }
}
