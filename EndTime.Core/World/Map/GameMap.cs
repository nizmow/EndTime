namespace EndTime.Core.World.Map;

public class GameMap(int width, int height)
{
    private readonly int[,] _map = new int[width, height];

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
}
