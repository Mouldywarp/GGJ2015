using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Enemy : Drawable
{
    Sprite _sprite;
    float _radius;
    bool _alive = false;
    BulletManager _bullets;
    Vector2f _velocity;

    public Vector2f position { get { return _sprite.Position; } set { _sprite.Position = value; } }
    public Vector2f velocity { set { _velocity = value; } }

    public bool isActive { get { return _alive; } }
    public void SetActive(bool active) { _alive = active; }
    public FloatRect bounds { get { return _sprite.GetGlobalBounds(); } }

    
    Random random = new Random();
    public Enemy(BulletManager bulletManager)
    {
        _sprite = new Sprite(Assets.GetTexture("../../images/enemy.png"));
        _sprite.Origin = new Vector2f(32, 16);
        _radius = 16;
        _bullets = bulletManager;
    }

    

    public void Update()
    {
        position += _velocity * Time.deltaTime;
    }


    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_sprite);
    }
   

}