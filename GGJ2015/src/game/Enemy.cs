using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Enemy : Drawable
{
    Sprite _sprite;
    Vector2f _velocity;
    float _radius;
    bool _alive = false;
    BulletManager _bullets;

    public Vector2f position { get {return _sprite.Position; } set { _sprite.Position = value; } }

    public bool isActive { get { return _alive; } }
    public void SetActive(bool active) { _alive = active; }

    
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
    }


    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_sprite);
    }
   

}