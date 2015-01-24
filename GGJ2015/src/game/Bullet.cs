using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Bullet : Drawable
{
    Animation _animation;
    Vector2f _velocity;

    public Vector2f position { get { return _animation.position; } set { _animation.position = value; } }
    public Vector2f velocity { set { _velocity = value; } }

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

    public void Update()
    {
        position += _velocity * Time.deltaTime;
    }
}