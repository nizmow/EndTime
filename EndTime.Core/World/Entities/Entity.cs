using EndTime.Core.Data;

namespace EndTime.Core.World.Entities;

public record Sprite { }

public class Entity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }

    public required EntityDefinition EntityDefinition { get; init; }

    public required int X { get; set; }
    public required int Y { get; set; }
    //
    // TODO: likely we will want components.
}
