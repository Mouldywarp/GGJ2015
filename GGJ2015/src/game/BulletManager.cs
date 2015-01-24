using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class BulletManager
{
    Stack<Bullet> _inactiveBullets = new Stack<Bullet>();
    List<Bullet> _activeBullets = new List<Bullet>();

    public BulletManager()
    {
        for (int i = 0; i < 10000; ++i)
        {
            Bullet bullet = new Bullet();
            _inactiveBullets.Push(bullet);
        }
    }




    public void CreateBullet(Vector2f position, Vector2f velocity)
    {
        if (_inactiveBullets.Count == 0) return;
        Bullet newBullet = _inactiveBullets.Pop();
        newBullet.SetActive(true);
        newBullet.position = position;
        newBullet.velocity = velocity;
        _activeBullets.Add(newBullet);
    }


    public void Update(Planet[] planets)
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            _activeBullets[i].Update(planets);
            if (!_activeBullets[i].isActive)
            {
                _inactiveBullets.Push(_activeBullets[i]);
                _activeBullets.RemoveAt(i);
            }
        }
    }

    public void DrawBullets(RenderWindow window)
    {
        foreach (Bullet bullet in _activeBullets) window.Draw(bullet);
    }

}

