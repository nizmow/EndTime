namespace EndTime.Core.Data;

public record TileDefinition(
    int Id,
    string Name,
    bool IsWalkable,
    SpriteInfo Visual
);
