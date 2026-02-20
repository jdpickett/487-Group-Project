using Microsoft.Xna.Framework.Input;

namespace Core;

public sealed class InputState
{
    public KeyboardState Current { get; private set; }
    public KeyboardState Previous { get; private set; }

    public void Update()
    {
        Previous = Current;
        Current = Keyboard.GetState();
    }

    public bool Down(Keys key) => Current.IsKeyDown(key);
    public bool Pressed(Keys key) => Current.IsKeyDown(key) && !Previous.IsKeyDown(key);
}