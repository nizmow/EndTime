namespace EndTime.Core;

public class TileRegistry
{
    private readonly Dictionary<int, TileDefinition> _tilesById = new();

    // TODO: do we want a name based lookup as well? We might.

    public void Register(TileDefinition tile)
    {
        _tilesById[tile.Id] = tile;
    }

    public TileDefinition Get(int id)
    {
        // return some kind of sensible default, or something.
        return _tilesById.TryGetValue(id, out var tile) ? tile : _tilesById[0];
    }
}
