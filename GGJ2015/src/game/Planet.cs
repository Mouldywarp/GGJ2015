using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Planet : Drawable
{
    CircleShape _sprite = new CircleShape();
    // using sprites properties (radius, position etc)
    Vector2f _position;
    Vector2f _velocity;
    float _radius;
    float _rotation;
    float _angularVelocity;
    float _mass;
    float _gravitationFieldRadius;

    public Vector2f position { get { return sprite.Position; } set { sprite.Position = value; } }
    public Vector2f velocity { set { _velocity = value; } }
    public float rotation { get { return sprite.Rotation; } set { sprite.Rotation = value; } }
    public float radius { get { return sprite.Radius; } set { sprite.Radius = value; } }
    public CircleShape sprite { get { return _sprite; } }
    public float angularVelocity { get { return _angularVelocity; } set { _angularVelocity = value; } }
    public float mass { get { return _mass; } set { _mass = value; } }
    public float gravitationalFieldRadius { get { return _gravitationFieldRadius; } set { _gravitationFieldRadius = value; } }

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
        sprite.Position += _velocity * Time.deltaTime;
        sprite.Rotation += _angularVelocity * Time.deltaTime;
    }
}