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
    const bool SKIP_MENU = false;
    public const int RES_WIDTH = 1280;
    public const int RES_HEIGHT = 720;
    public const int FRAMES_PER_SECOND = 60;

    RenderWindow _window; //!< The window we draw everything to in the game
    private float _fixedFrameTimer;
    private float _frameDelay = 1 / FRAMES_PER_SECOND;
    public static Random random = new Random();

    // Game Objects
    BulletManager _bulletManager = new BulletManager();
    PlanetManager _planetManager = new PlanetManager();
    EnemySpuffer _enemySpuffer;
    CollisionManager _collisionManager;

    Player _player;
    Menu _menu = new Menu();
    Background _background = new Background(RES_WIDTH, RES_HEIGHT);


    float _resetTimer = 0;
 

    // Create an enum of states for switching between menus and levels
    public enum GameStates { MAIN_MENU, PLAYING_LEVEL, PAUSE, GAME_OVER };
    GameStates _currentState = GameStates.MAIN_MENU;



    public Game()
    {
        _enemySpuffer = new EnemySpuffer(_bulletManager);
        _player = new Player(new Vector2f(100, RES_HEIGHT / 2), _bulletManager);
        _collisionManager = new CollisionManager(_bulletManager.playerBullets, _bulletManager.enemyBullets, _enemySpuffer.enemies, _planetManager.planets, _player);
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

        // Create Time!!
        Time.CreateTime(this);

        if (SKIP_MENU) _currentState = GameStates.PLAYING_LEVEL;

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



    // Call to reset teh game
    void Reset()
    {
        _player.Reset();
        _enemySpuffer.Reset();
        _bulletManager.Reset();
        _planetManager.Reset();
        _currentState = GameStates.MAIN_MENU;
    }



    //################################   UPDATE!!!!!!!!

    void Update()
    {
        switch (_currentState)
        {
            //~~~~~~~~~~~~~~~~~~~~~~ MAIN MENU!
            case GameStates.MAIN_MENU:
                if (Input.getKey(Keyboard.Key.Return) == true || Input.getKey(Keyboard.Key.Space) == true)
                {
                    // Reset function needed here
                    _currentState = GameStates.PLAYING_LEVEL;
                }
                break;

            //~~~~~~~~~~~~~~~~~~~~~~ PLAYING LEVEL!
            case GameStates.PLAYING_LEVEL:
                if (Input.getKey(Keyboard.Key.H)) Reset(); // H is quit?

                // All update code here!
                _planetManager.Update();
                _bulletManager.Update();
                _background.update();
                _player.update();
                _enemySpuffer.Update();
                _collisionManager.Update();

                if (!_player.isAlive)
                {
                    _currentState = GameStates.GAME_OVER;
                    _resetTimer = 0;
                }

                break;

            //~~~~~~~~~~~~~~~~~~~~~~ U R DED
            case GameStates.GAME_OVER:
                _resetTimer += Time.deltaTime;

                if (_resetTimer >= 0.5f)
                {
                    _resetTimer = 0;
                    Reset();
                }

                break;

        }
    }



    //#################################### FIXED UPDATE! PROBABLY NOT USED LOLLLLL!

    void FixedUpdate()
    {
        // All fixed frame rate Update code here!
        //Vector2f position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
        //Vector2f velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
        //_bulletManager.CreateBullet(Bullet.Shooter.ENEMY, position, velocity);
        _menu.Update();
    }




    //#################################### DRAW EVERYTHING!!!!!!
    void Draw()
    {
        switch (_currentState)
        {
            //~~~~~~~~~~~~~~~~~~~~~~ MAIN MENU!
            case GameStates.MAIN_MENU:
                _background.Draw(_window);
                _window.Draw(_menu);
                break;

            //~~~~~~~~~~~~~~~~~~~~~~ PLAYING LEVEL!
            case GameStates.PLAYING_LEVEL:
                _background.Draw(_window);
                _planetManager.DrawPlanets(_window);
                _enemySpuffer.DrawEnemies(_window);
                _window.Draw(_player);
                _bulletManager.DrawBullets(_window);
                break;

        }

    }

}