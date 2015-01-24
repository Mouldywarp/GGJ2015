using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Player : Drawable
{
 
    const float SPEED = 500;
    const float BULLETSPEED = 500;
    const float RATEOFFIRE = 0.075f;

    static float _timer;

    Sprite _johnBervege;
    Vector2f _velocity;
    Vector2f _acceleration;
    Vector2f _position;

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
    }

    private void handleInput()
    {
        if (Input.getKey(Keyboard.Key.W) == true || Input.getKey(Keyboard.Key.Up))
        {
            _johnBervege.Position += new Vector2f(0, -Time.deltaTime*SPEED);
        }
        if (Input.getKey(Keyboard.Key.S) == true || Input.getKey(Keyboard.Key.Down))
        {
            _johnBervege.Position += new Vector2f(0, Time.deltaTime * SPEED);
        }
    }
   
    private void FireBullet()
    {
        if (_timer > RATEOFFIRE)
        {
            _timer = 0;
            _cuntingtonSmithe.CreateBullet(Bullet.Shooter.PLAYER, _johnBervege.Position, new Vector2f(BULLETSPEED, 0));
        }
    }

    public void update()
    {
        _timer += Time.deltaTime;
        FireBullet();
        handleInput();
        _johnBervege.Color = new Color((byte)random.Next(), (byte)random.Next(), (byte)random.Next());
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_johnBervege);
    }

}