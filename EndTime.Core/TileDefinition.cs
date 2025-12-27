namespace EndTime.Core;


public record TileDefinition(
    int Id,
    string Name,
    bool IsWalkable,
    SpriteInfo Visual
);
