using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndTime.Core;

public class EndTimeGame : Game
{
    private GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch = null!; // trust me

    private Texture2D _tileAtlas = null!; // trust me

    private TileRegistry _tileRegistry;

    // Things will get more complex with levels and scenes, punt that to later.
    private MapManager _mapManager;

    private const int WIDTH = 80;
    private const int HEIGHT = 50;

    public EndTimeGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _tileRegistry = new TileRegistry();
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
        _tileRegistry.Register(new TileDefinition(Id: 1, Name: "wall", IsWalkable: false, Symbol: CodePage437.LightShade, HexForegroundColour: "#FFFFFF"));
        _tileRegistry.Register(new TileDefinition(Id: 2, Name: "floor", IsWalkable: true, Symbol: CodePage437.MiddleDot, HexForegroundColour: "#CCCCCC"));

        _mapManager.SetTile(40, 25, 1);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _tileAtlas = Content.Load<Texture2D>("cp437_font");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // clamp pixel edges for crispy pixels
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _mapManager.Draw(_spriteBatch, _tileAtlas);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
