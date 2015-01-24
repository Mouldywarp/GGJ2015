using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Player : Drawable
{ 
    int curScore = 0;
    Text t_score;
    Font font;

    const float SPEED = 180;

    const float BULLETSPEED = 1000;
    const float RATEOFFIRE = 0.035f;

    static float _timer;

    public void score(int num)
    {
        curScore += num;
        t_score = new Text(curScore.ToString(), font);
    }

    bool _creep;
    BulletManager _cuntingtonSmithe;
    float _radius;
    Random random = new Random();


    Sprite _sprite;
    Vector2f _velocity;
    public Vector2f position { set { _sprite.Position = value; } get { return _sprite.Position; } }
    public float radius { get { return _radius; } }

    public Player(Vector2f Position, BulletManager bmIns)
    {
        _sprite = new Sprite(Assets.GetTexture("../../images/ship.png"));
        _sprite.Position = Position;
        _sprite.Origin = new Vector2f(32, 16);
        _radius = 16;
        _cuntingtonSmithe = bmIns;

        // Score Stuff
        font = new Font("../../fonts/arial.ttf");
        t_score = new Text(curScore.ToString(), font);
        t_score.Position = new Vector2f(10, 10);
        t_score.CharacterSize = 24;
        _creep = false;
    }

    private void handleInput()
    {
        _velocity = new Vector2f();
        _creep = false;

        if (Input.getKey(Keyboard.Key.W) == true || Input.getKey(Keyboard.Key.Up))
        {
           _velocity.Y = -SPEED;
        }
        if (Input.getKey(Keyboard.Key.S) == true || Input.getKey(Keyboard.Key.Down))
        {
           _velocity.Y = SPEED;
        }
        if (Input.getKey(Keyboard.Key.D) == true || Input.getKey(Keyboard.Key.Right))
        {
            _velocity.X = SPEED;
        }
        if (Input.getKey(Keyboard.Key.A) == true || Input.getKey(Keyboard.Key.Left))
        {
            _velocity.X = -SPEED;
        }
        if (Input.getKey(Keyboard.Key.LShift) == true)
        {
            if (_creep == false)
            {
                _creep = !_creep;
            }
        }
    }

    private void Move()
    {
        if (_creep == true)
        {
            _sprite.Position += (_velocity / 2) * Time.deltaTime;
        }
        else
        {
            _sprite.Position += _velocity * Time.deltaTime;
        }
    }

    private void FireBullet()
    {
        if (_timer > RATEOFFIRE)
        {
            _timer = 0;
            _cuntingtonSmithe.CreateBullet(Bullet.Shooter.PLAYER, position, new Vector2f(_velocity.X + BULLETSPEED, 0));
        }
    }

    public void update()
    {
        _timer += Time.deltaTime;

        handleInput();
        Move();
        FireBullet();
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(t_score);
        target.Draw(_sprite);
    }

}