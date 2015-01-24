﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Bullet : Drawable
{
    Animation _animation;
    Vector2f _velocity;
    float _radius;

    public Vector2f position { get { return _animation.position; } set { _animation.position = value; } }
    public Vector2f velocity { set { _velocity = value; } }


    public bool alive { set { _alive = value; } }
    public float radius { get { return _radius; } }



    public Bullet()
    {
        Texture texture = Assets.GetTexture("../../images/bullet.png");
        _animation = new Animation(texture, 4);
        _animation.tileSize = new Vector2i(16, 16);
        _animation.AddClip(0, 3);
        _animation.speed = (1.0f / 16);
        _animation.Play();

    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        _animation.Draw(target, states);
    }

    public void Update(Planet[] planets)
    {
        position += _velocity * Time.deltaTime;

        // Collision Check
        foreach (Planet planet in planets)
        {
            if (CircleMath.Intersects(this.position, this._radius, planet.position, planet.radius)) _alive = false;
        }

        // Screen View
    }
}