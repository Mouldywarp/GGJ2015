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
    BulletManager _bullets;

    Vector2f position { get {return _sprite.Position; } set { _sprite.Position = value; } }
    

    
    Random random = new Random();
    public Enemy(Vector2f pos, BulletManager bulletManager)
    {
        _sprite = new Sprite(Assets.GetTexture("../../images/ship.png"));
        _sprite.Position = pos;
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