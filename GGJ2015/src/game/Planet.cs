using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Planet : Drawable
{
    CircleShape _sprite = new CircleShape();
    Vector2f _position;
    Vector2f _velocity;
    float _radius;
    float _rotation;
    float _angularVelocity;

    public Vector2f position { get { return _position; } set { _position = value; } }
    public Vector2f velocity { set { _velocity = value; } }
    public float rotation { get { return _rotation; } set { _rotation = value; } }
    public float radius { get { return _radius; } set { _radius = value; } }
    public CircleShape sprite { get { return _sprite; } }
    public float angularVelocity { get { return _angularVelocity; } set { _angularVelocity = value; } }

    public Planet()
    {
        rotation = 0;
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_sprite);
    }

    public void Update()
    {
        position += _velocity * Time.deltaTime;
        rotation += _angularVelocity * Time.deltaTime;

        _sprite.Position = position;
        _sprite.Rotation = rotation;
    }
}