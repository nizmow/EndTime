namespace EndTime.Game;

using EndTime.Core.Data;
using EndTime.Core.Engine;
using EndTime.Core.Input;
using EndTime.Core.Utilities;
using Endtime.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EndTimeGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch = null!;

    private Renderer _renderer = null!;
    private Texture2D _tileAtlas = null!;
    private readonly TileRegistry _tileRegistry = new();
    private readonly EntityRegistry _entityRegistry = new();
    private readonly GameEngine _engine;
    private readonly InputManager _inputManager = new();

    private const int WIDTH = 80;
    private const int HEIGHT = 50;

    public EndTimeGame()
    {
        _engine = new(_entityRegistry, _tileRegistry);

        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = WIDTH * SpriteMath.Width,
            PreferredBackBufferHeight = HEIGHT * SpriteMath.Height,
        };

        Window.AllowUserResizing = true;

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: temporary, we will load tiles from JSON in the future (and maybe have a tile editor tool, ooh fun)
        _tileRegistry.Register(
            new TileDefinition(
                Id: 1,
                Name: "wall",
                IsWalkable: false,
                Visual: new SpriteInfo(
                    Symbol: CodePage437.LightShade,
                    HexForegroundColour: "#FFFFFF"
                )
            )
        );
        _tileRegistry.Register(
            new TileDefinition(
                Id: 2,
                Name: "floor",
                IsWalkable: true,
                Visual: new SpriteInfo(
                    Symbol: CodePage437.MiddleDot,
                    HexForegroundColour: "#CCCCCC"
                )
            )
        );

        // TODO: temporary, we will load entity definitions from JSON
        _entityRegistry.Register(
            new EntityDefinition(
                Id: 1,
                Name: "player",
                Visual: new SpriteInfo(
                    Symbol: CodePage437.SmileyBlack,
                    HexForegroundColour: "#FFFFFF"
                )
            )
        );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tileAtlas = Content.Load<Texture2D>("cp437_font");
        _renderer = new Renderer(_tileRegistry, _tileAtlas);

        base.LoadContent();
        // Start your engines here
        _engine.Start();
    }

    protected override void Update(GameTime gameTime)
    {
        var playerAction = _inputManager.GetInputAction(gameTime);

        // Do not tick until we get an action
        if (playerAction != null)
        {
            var result = _engine.Update(playerAction);

            if (result == GameStatus.Quit)
            {
                Exit();
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // clamp pixel edges for crispy pixels
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _renderer.Draw(_spriteBatch, _engine.World);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
