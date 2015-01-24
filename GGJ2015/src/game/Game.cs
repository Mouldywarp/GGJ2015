using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

/*! \brief This is a swell game wot I dun
 *         
 */
public class Game
{
    public const int RES_WIDTH = 1280;
    public const int RES_HEIGHT = 720;
    public const int FRAMES_PER_SECOND = 60;

    RenderWindow _window; //!< The window we draw everything to in the game
    private float _fixedFrameTimer;
    private float _frameDelay = 1 / FRAMES_PER_SECOND;
    Random random = new Random();

    // Game Objects
    BulletManager _bulletManager = new BulletManager();
    PlanetManager _planetManager = new PlanetManager();
    CollisionManager _collisionManager = new CollisionManager();

    CircleShape player = new CircleShape(16);

    Player JohnBervege;

    public Game()
    {
        JohnBervege = new Player(new Vector2f(100, RES_HEIGHT / 2), _bulletManager);
    }

    public void RunGame()
    {
        _window = new RenderWindow(new VideoMode(RES_WIDTH, RES_HEIGHT), "What do we do now?64");
        _window.SetVisible(true);
  
        // Event handlers - Add other events such as input in same way!!
        _window.Closed += new EventHandler(OnClosed); // Exactly same as Closed += OnClosed. Slightly different syntax but seems to work exactly the same
        _window.Resized += new EventHandler<SizeEventArgs>(OnResize); // Resize event handler
        _window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeypressed);
        _window.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyrelease);
        UpdateEvent += new UpdateEventHandler(Update); // Add Game's Update function to our Update event (this is an event to tie to Time class)

        // Player
        player.Position = new Vector2f(RES_WIDTH * 0.2f, RES_HEIGHT * 0.5f);
        player.Origin = new Vector2f(16, 16);
        player.FillColor = Color.Green;

        // Create Time!!
        Time.CreateTime(this);

        Initialize();

        // Game loop
        while (_window.IsOpen())
        {
            _window.DispatchEvents();
            _window.Clear(Color.Blue);
            
            UpdateEvent(); // call update event
            if(Input.getKey(Keyboard.Key.Escape)==true)
            _window.Close();
            // Fixed frame update
            _fixedFrameTimer += Time.deltaTime;
            if (_fixedFrameTimer >= _frameDelay)
            {
                FixedUpdate();
                _fixedFrameTimer = 0;
            }
            
            Draw();
            _window.Display();

        }
    }

    // Event subscribed to window's Closed delegate function (event dispatcher basically), called on closing the app
    void OnClosed(object sender, EventArgs e)
    {
        _window.Close();
    }


    void OnResize(object sender, SizeEventArgs e)
    {
    }

    void OnKeypressed(object sender, KeyEventArgs e)
    {
        Input.keyPressed(e.Code);
    }
    void OnKeyrelease(object sender, KeyEventArgs e)
    {
        Input.keyReleased(e.Code);
    }

    //! Update is an event so it can be passed to time to link framerates
    public delegate void UpdateEventHandler();
    public event UpdateEventHandler UpdateEvent;

    void Initialize()
    {
        
    }
    
    void Update()
    {
        // All update code here!
	_planetManager.Update();

        _bulletManager.Update();

        // Collision Updates
        _collisionManager.Update(_bulletManager.playerBullets, _bulletManager.enemyBullets, _planetManager.planets, player);

        

        if (Input.getKey(Keyboard.Key.P))
        {
            Vector2f position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
            Vector2f velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
            _bulletManager.CreateBullet(Bullet.Shooter.PLAYER, position, velocity);
        }


        if (Input.getKey(Keyboard.Key.E))
        {
            Vector2f position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
            Vector2f velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
            _bulletManager.CreateBullet(Bullet.Shooter.ENEMY, position, velocity);
        }

    }

    void FixedUpdate()
    {
        // All fixed frame rate Update code here!
        Vector2f position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
        Vector2f velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
        _bulletManager.CreateBullet(Bullet.Shooter.ENEMY, position, velocity);
        JohnBervege.update();
    }

    void Draw()
    {
        // All draw code here!
        _bulletManager.DrawBullets(_window);
        _planetManager.DrawPlanets(_window);
        _window.Draw(JohnBervege);
    }

}