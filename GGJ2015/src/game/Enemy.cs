using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Enemy : Drawable
{
    Sprite _sprite;
    EnemyBehaviour _behaveYourself;
    float _radius;
    bool _alive = false;
    BulletManager _bullets;
    Vector2f _velocity;

    public Vector2f position { get { return _sprite.Position; } set { _sprite.Position = value; } }
    public Vector2f velocity { get {return  _velocity; }  set { _velocity = value; } }

    public bool isActive { get { return _alive; } }
    public void SetActive(bool active) { _alive = active; }
    public FloatRect bounds { get { return _sprite.GetGlobalBounds(); } }

    
    Random random = new Random();
    

    // FUNCTIONS
    // Constructinator
    public Enemy(BulletManager bulletManager)
    {
        _sprite = new Sprite(Assets.GetTexture("../../images/enemy.png"));
        _sprite.Origin = new Vector2f(32, 16);
        _radius = 16;
        _bullets = bulletManager;
        _behaveYourself = new EnemyBehaviour(this);
    }

    public void OnCreate(EnemyBehaviour.Type type)
    {
        _behaveYourself.SetBehaviour(type);
    }


    public void Update()
    {
        position += _velocity * Time.deltaTime;
        _behaveYourself.Update();
    }


    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_sprite);
    }





}