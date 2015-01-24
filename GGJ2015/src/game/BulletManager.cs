using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class BulletManager
{
    Bullet[] _bullets = new Bullet[100];

    public BulletManager()
    {
        Random random = new Random();
        

        for (int i = 0; i < _bullets.Length; ++i)
        {
            _bullets[i].position = new Vector2f(random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
            _bullets[i].velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
        }
    }


    public void Update()
    {
        foreach (Bullet bullet in _bullets) bullet.Update();
    }

    public void DrawBullets(RenderWindow window)
    {
        foreach (Bullet bullet in _bullets) window.Draw(bullet);
    }

}

