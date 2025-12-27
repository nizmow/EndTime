
using System.Drawing;

namespace EndTime.Core;

public record Sprite
{

}

public class Entity
{
    public required Guid Id { get; init; }

    public required EntityDefinition EntityDefinition { get; init; }
    
    public required int X { get; set; }
    public required int Y { get; set; }

    // TODO: likely we will want components.
}