using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Player : Drawable
{

    const float SPEED = 300;

    const float BULLETSPEED = 1000;
    const float RATEOFFIRE = 0.035f;

    static float _timer;

    Sprite _johnBervege;
    Vector2f _velocity;
    Vector2f _acceleration;
    Vector2f _position;

    bool _creep;
    BulletManager _cuntingtonSmithe;
    float _radius;
    Random random = new Random();
    public Player(Vector2f Position, BulletManager bmIns)
    {
        _johnBervege = new Sprite(Assets.GetTexture("../../images/ship.png"));
        _johnBervege.Position = Position;
        _johnBervege.Origin = new Vector2f(32,16);
        _radius = 16;
        _cuntingtonSmithe = bmIns;
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
            _johnBervege.Position += (_velocity/2) * Time.deltaTime;
        }
        else
        {
            _johnBervege.Position += _velocity * Time.deltaTime;
        }
    }

    private void FireBullet()
    {
        if (_timer > RATEOFFIRE)
        {
            _timer = 0;
            _cuntingtonSmithe.CreateBullet(Bullet.Shooter.PLAYER, _johnBervege.Position, new Vector2f(_velocity.X + BULLETSPEED, 0));
        }
    }

    public void update()
    {
        _timer += Time.deltaTime;

        Move();
        FireBullet();
        handleInput();
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_johnBervege);
    }

}