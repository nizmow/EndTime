using EndTime.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndTime.Core;

public class EndTimeGame : Game
{
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch = null!; // trust me

    private Texture2D _tileAtlas = null!; // trust me

    private TileRegistry _tileRegistry;

    private EntityRegistry _entityRegistry;

    private EntityManager _entityManager;

    // Things will get more complex with levels and scenes, punt that to later.
    private MapManager _mapManager;

    private InputManager _inputManager = new();

    private Entity _player;

    private const int WIDTH = 80;
    private const int HEIGHT = 50;

    public EndTimeGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _tileRegistry = new TileRegistry();
        _entityRegistry = new EntityRegistry();
        _entityManager = new EntityManager();

        _mapManager = new MapManager(WIDTH, HEIGHT, _tileRegistry);

        _graphics.PreferredBackBufferWidth = WIDTH * SpriteMath.Width;
        _graphics.PreferredBackBufferHeight = HEIGHT * SpriteMath.Height;

        Window.AllowUserResizing = true;

        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: temporary, we will load tiles from JSON in the future (and maybe have a tile editor tool, ooh fun)
        _tileRegistry.Register(new TileDefinition(Id: 1, Name: "wall", IsWalkable: false, Visual: new SpriteInfo(Symbol: CodePage437.LightShade, HexForegroundColour: "#FFFFFF")));
        _tileRegistry.Register(new TileDefinition(Id: 2, Name: "floor", IsWalkable: true, Visual: new SpriteInfo(Symbol: CodePage437.MiddleDot, HexForegroundColour: "#CCCCCC")));

        _mapManager.SetTile(40, 25, 1);

        // TODO: temporary, we will load entity definitions from JSON
        _entityRegistry.Register(new EntityDefinition(Id: 1, Name: "player", Visual: new SpriteInfo(Symbol: CodePage437.SmileyBlack, HexForegroundColour: "#FFFFFF")));
        _player = _entityRegistry.Spawn("player", 10, 10);
        _entityManager.Add(_player);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tileAtlas = Content.Load<Texture2D>("cp437_font");

        // Spawn initial entities

    }

    protected override void Update(GameTime gameTime)
    {
        var playerAction = _inputManager.GetInputAction(gameTime);
        if (playerAction is QuitAction)
        {
            Exit();
        }
        if (playerAction is MoveAction moveAction)
        {
            _player.X += moveAction.DeltaX;
            _player.Y += moveAction.DeltaY;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // clamp pixel edges for crispy pixels
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _mapManager.Draw(_spriteBatch, _tileAtlas);

        _entityManager.Draw(_spriteBatch, _tileAtlas);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
