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

    public Game()
    {
    }

    public void RunGame()
    {
        _window = new RenderWindow(new VideoMode(RES_WIDTH, RES_HEIGHT), "What do we do now?64");
        _window.SetVisible(true);

        // Event handlers - Add other events such as input in same way!!
        _window.Closed += new EventHandler(OnClosed); // Exactly same as Closed += OnClosed. Slightly different syntax but seems to work exactly the same
        _window.Resized += new EventHandler<SizeEventArgs>(OnResize); // Resize event handler
        UpdateEvent += new UpdateEventHandler(Update); // Add Game's Update function to our Update event (this is an event to tie to Time class)

        // Create Time!!
        Time.CreateTime(this);

        Initialize();

        // Game loop
        while (_window.IsOpen())
        {
            _window.DispatchEvents();
            _window.Clear(Color.Blue);
            
            UpdateEvent(); // call update event
            
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
        _bulletManager.Update(_planetManager.planets);
    }

    void FixedUpdate()
    {
        // All fixed frame rate Update code here!
        //Vector2f position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
        //Vector2f velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
        //_bulletManager.CreateBullet(position, velocity);
    }

    void Draw()
    {
        // All draw code here!
        _bulletManager.DrawBullets(_window);
        _planetManager.DrawPlanets(_window);
    }

}