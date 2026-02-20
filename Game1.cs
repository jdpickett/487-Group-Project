using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core;

namespace _487_Group_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics = null!;
    private SpriteBatch _spriteBatch = null!;

    // Core stuff
    private SimpleDrawer _drawer = null!;
    private InputState _input = null!;

    // visual
    private Vector2 _testPos;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = GameConfig.ScreenWidth;
        _graphics.PreferredBackBufferHeight = GameConfig.ScreenHeight;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _drawer = new SimpleDrawer(GraphicsDevice);
        _input = new InputState();

        // start test object in center of playfield
        _testPos = new Vector2(
            GameConfig.Playfield.Center.X,
            GameConfig.Playfield.Center.Y
        );
    }

    protected override void Update(GameTime gameTime)
    {
        // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //     Exit();

        // TODO: Add your update logic here

        _input.Update();

        if (_input.Down(Keys.Escape))
            Exit();

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Test movement using GameConfig speeds
        bool slow = _input.Down(Keys.LeftShift);
        float speed = slow ? GameConfig.PlayerSpeedSlow : GameConfig.PlayerSpeedNormal;
        
        Vector2 move = Vector2.Zero;
        
        if (_input.Down(Keys.W) || _input.Down(Keys.Up)) move.Y -= 1;
        if (_input.Down(Keys.S) || _input.Down(Keys.Down)) move.Y += 1;
        if (_input.Down(Keys.A) || _input.Down(Keys.Left)) move.X -= 1;
        if (_input.Down(Keys.D) || _input.Down(Keys.Right)) move.X += 1;

        if (move != Vector2.Zero)
            move.Normalize();

        _testPos += move * speed * dt;

        // Clamp inside playfield
        _testPos = new Vector2(
            MathHelper.Clamp(_testPos.X, GameConfig.Playfield.Left + 10, GameConfig.Playfield.Right - 10),
            MathHelper.Clamp(_testPos.Y, GameConfig.Playfield.Top + 10, GameConfig.Playfield.Bottom - 10)
        );

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw playfield boundary
        _drawer.DrawRectOutline(_spriteBatch, GameConfig.Playfield, 3, Color.DarkGray);

        // Draw test square (fake player)
        var rect = new Rectangle((int)_testPos.X - 10, (int)_testPos.Y - 10, 20, 20);
        _drawer.DrawRect(_spriteBatch, rect, Color.White);

        // Draw slow-mode indicator
        if (_input.Down(Keys.LeftShift))
        {
            var hit = new Rectangle((int)_testPos.X - 2, (int)_testPos.Y - 2, 4, 4);
            _drawer.DrawRect(_spriteBatch, hit, Color.LightGray);
        }

        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
