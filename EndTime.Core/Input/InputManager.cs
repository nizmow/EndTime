using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EndTime.Core.Input;

public class InputManager
{
    private const int InitialDelayMs = 300;
    private const int RepeatDelayMs = 30;

    private int _keyTimer = 0;
    private KeyboardState _oldState;
    private readonly Dictionary<Keys, Action> keys = new()
    {
        { Keys.J, new MoveUpAction() },
        { Keys.K, new MoveDownAction() },
        { Keys.H, new MoveLeftAction() },
        { Keys.L, new MoveRightAction() },
        { Keys.Escape, new QuitAction() }
    };

    public InputManager()
    {
        _oldState = Keyboard.GetState();
    }

    // Update input state
    public Action? GetInputAction(GameTime gameTime)
    {
        var newState = Keyboard.GetState();

        Action? returnAction = null;

        // Give a delay / repeat feeling like a standard OS keyboard controller.
        // Pretty naieve loop, last in the dictionary will win the action.
        foreach (var k in keys)
        {
            var key = k.Key;

            if (newState.IsKeyDown(key))
            {
                if (_oldState.IsKeyUp(key))
                {
                    // first press detected, so immediate action
                    _keyTimer = InitialDelayMs;
                    returnAction = k.Value;
                }
                else
                {
                    // key already pressed, so delay action
                    _keyTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (_keyTimer <= 0)
                    {
                        _keyTimer = RepeatDelayMs;
                        returnAction = k.Value;
                    }
                }
            }

        }

        _oldState = newState;
        return returnAction;
    }
}

public record Action();

public record QuitAction() : Action();

public record MoveAction(int DeltaX, int DeltaY) : Action();
public record MoveUpAction() : MoveAction(0, 1);
public record MoveDownAction() : MoveAction(0, -1);
public record MoveLeftAction() : MoveAction(-1, 0);
public record MoveRightAction() : MoveAction(1, 0);
