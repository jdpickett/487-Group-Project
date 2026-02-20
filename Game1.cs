using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core;
using Entities;

namespace _487_Group_Project;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics = null!;
    private SpriteBatch _spriteBatch = null!;

    // Core stuff
    private SimpleDrawer _drawer = null!;
    private InputState _input = null!;

    // player
    private Player _player = null!;

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
        _player = new Player(_drawer, _input);
    }

    protected override void Update(GameTime gameTime)
    {
        _input.Update();
        if(_input.Down(Keys.Escape))
            Exit();
        
        _player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // Draw playfield boundary
        _drawer.DrawRectOutline(_spriteBatch, GameConfig.Playfield, thickness: 3, color: Color.DarkGray);

        // Draw player
        _player.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
