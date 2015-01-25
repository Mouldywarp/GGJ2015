using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Enemy : Drawable
{
    enum State { ENTERING, ONSCREEN, EXPLODING }
    State _currentState;

    Sprite _sprite;
    EnemyBehaviour _behaveYourself;
    float _radius;
    float _health;
    bool _alive = false;
    BulletManager _bullets;
    Vector2f _velocity;
    Vector2f _gunPos = new Vector2f(50, 50); // offset to Gary's mouth from top corner of image, so he shoot stuff from his mouth

    public Vector2f position { get { return _sprite.Position; } set { _sprite.Position = value; } }
    public Vector2f velocity { get {return  _velocity; }  set { _velocity = value; } }
    public bool onScreen { get { return _currentState == State.ONSCREEN; } } 
    public bool isDead { get { return _currentState == State.EXPLODING; } }
    public float radius { get { return _radius; } }

    public bool isActive { get { return _alive; } }
    public void SetActive(bool active) { _alive = active; }
    public FloatRect bounds { get { return _sprite.GetGlobalBounds(); } }

    
    Random random = new Random();
    

    // FUNCTIONS
    // Constructinator
    public Enemy(BulletManager bulletManager)
    {
        _sprite = new Sprite(Assets.GetTexture("../../images/enemy.png"));
        _sprite.Origin = new Vector2f(77, 66);
        _radius = 20;
        _health = 10;       // Takes 10 shots to kill an enemy
        _bullets = bulletManager;
        _behaveYourself = new EnemyBehaviour(this);
    }

    public void OnCreate(EnemyBehaviour.Type type)
    {
        _behaveYourself.SetBehaviour(type);
        _currentState = State.ENTERING;
        _health = 10;
    }

    public void Shoot(Vector2f bulletVelocity)
    {
        _bullets.CreateBullet(Bullet.Shooter.ENEMY, position + _gunPos, bulletVelocity);
    }


    public void Update()
    {
        position += _velocity * Time.deltaTime;
        _behaveYourself.Update();

        switch (_currentState)
        {
            case State.ENTERING:
                if (!CircleMath.OffScreen(bounds)) _currentState = State.ONSCREEN;   
                break;

            case State.ONSCREEN:
                if (CircleMath.OffScreen(bounds)) SetActive(false); 
                break;

            case State.EXPLODING:
                // Animate explodyness then vanish after a while
                break;

        }
    }


    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_sprite);
    }


    // Hit by player bullets
    public void OnHit()
    {
        _health--;

        // Console.WriteLine(_health);

        if (_health < 1)
        {
            _alive = false;
        }

    }


}