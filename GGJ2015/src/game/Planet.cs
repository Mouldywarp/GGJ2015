using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Planet : Drawable
{
    Sprite _sprite = new Sprite();
    bool _active = false;
    Vector2f _position;
    Vector2f _velocity;
    float _radius;
    float _rotation;
    float _angularVelocity;
    float _mass;
    float _gravitationFieldRadius;
    public FloatRect bounds { get { return _sprite.GetGlobalBounds(); } }

    public Vector2f position { get { return _sprite.Position; } set { _sprite.Position = value; } }
    public Vector2f velocity { set { _velocity = value; } }
    public float rotation { get { return _sprite.Rotation; } set { _sprite.Rotation = value; } }
    public float radius { get { return _radius; } set { _radius = value; } }
    public Sprite sprite { get { return _sprite; } }
    public float angularVelocity { get { return _angularVelocity; } set { _angularVelocity = value; } }
    public float mass { get { return _mass; } set { _mass = value; } }
    public float gravitationalFieldRadius { get { return _gravitationFieldRadius; } set { _gravitationFieldRadius = value; } }
    public Vector2u textureSize { get { return _sprite.Texture.Size; } }


    public bool isActive { get { return _active; } }
    public void SetActive(bool active) { _active = active; }
    bool onscreen = false;



    public Planet(Texture texture)
    {
        _sprite.Texture = texture;
        Reset();
        //org.FillColor = Color.Red;
        //org.Position = _sprite.Origin;
        //org.Origin = new Vector2f(1, 1);
        //org.Radius = 2;
               
    }

    public void Reset()
    {
        onscreen = false;
        rotation = 0;


        float scale = ((float)Game.random.NextDouble() * 0.5f) + 0.5f;

        position = new Vector2f(Game.RES_WIDTH + (scale * textureSize.X), Game.random.Next(Game.RES_HEIGHT));
        velocity = new Vector2f(-80, 0);
        _sprite.Origin = new Vector2f(textureSize.X * 0.5f, textureSize.Y * 0.5f);
        radius = textureSize.X * 0.5f;


        SetScale(scale);

        angularVelocity = Game.random.Next(10) + 10;
        gravitationalFieldRadius = radius + 250;
    }


    public void SetScale(float scale)
    {
        _radius = textureSize.X * scale * 0.5f;
        _sprite.Scale = new Vector2f(scale, scale);
    }


    public void Draw(RenderTarget target, RenderStates states)
    {
        if (!_active) return;
        target.Draw(_sprite);
        //target.Draw(org);
    }

    public void Update()
    {
        if (!_active) return;
        sprite.Position += _velocity * Time.deltaTime;
        sprite.Rotation += _angularVelocity * Time.deltaTime;
       //org.Position = sprite.Position;

        // CHECK SCREEN LIKE ENEMIES
        switch (onscreen)
        {
            case false:
                if (!CircleMath.OffScreen(bounds)) onscreen = true;
                break;

            case true:
                if (CircleMath.OffScreen(bounds)) SetActive(false);
                break;
        }
    }
}