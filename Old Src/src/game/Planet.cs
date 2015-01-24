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
    float _mass;
    float _gravitationFieldRadius;
    const float PI = 3.1412f;

    public Vector2f position { get { return _position; } set { _position = value; } }
    public Vector2f velocity { set { _velocity = value; } }
    public float rotation { get { return _rotation; } set { _rotation = value; } }
    public float radius { get { return _sprite.Radius; } set { sprite.Radius = value; } }
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

    public void Update(List<Bullet> bullets)
    {
        position += _velocity * Time.deltaTime;
        rotation += _angularVelocity * Time.deltaTime;

        _sprite.Position = position;
        _sprite.Rotation = rotation;

        foreach(Bullet bullet in bullets)
        {
            if(CircleMath.Intersects(this.position, this.gravitationalFieldRadius, bullet.position, bullet.radius))
            {
                // Calculate Gravity Formula
                float density = mass / (PI * radius * radius);
                float GravEffect = density / (CircleMath.GetSquaredDistanceBetween(this.position, bullet.position));

                bullet.velocity *= GravEffect;
            }
        }
    }
}