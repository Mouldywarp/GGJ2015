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
    bool _alive = false;
    float _radius;

    public Vector2f position { get { return _animation.position; } set { _animation.position = value; } }
    public Vector2f velocity { set { _velocity = value; } get { return _velocity; } }
    public float radius { get { return _radius; } }
    public bool isActive { get { return _alive; } }
    public void SetActive(bool active) { _alive = active; }

    public Bullet()
    {
        Texture texture = Assets.GetTexture("../../images/bullet.png");
        _animation = new Animation(texture, 4);
        _animation.tileSize = new Vector2i(16, 16);
        _animation.AddClip(0, 3);
        _animation.speed = (1.0f / 16);
        _animation.Play();

        _radius = 8; // one would assume, if tile is 16X16
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        _animation.Draw(target, states);
    }

    public void Update(Planet[] planets)
    {
        position += _velocity * Time.deltaTime;
        foreach(Planet planet in planets)
        {
            if (CircleMath.Intersects(this.position, this.radius, planet.position, planet.radius))
            {
                _alive = false;
                break;
            }
        }
    }
}