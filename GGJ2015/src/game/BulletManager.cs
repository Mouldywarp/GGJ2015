using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class BulletManager
{
    Stack<Bullet> _inactiveBullets = new Stack<Bullet>();
    List<Bullet> _playerBullets = new List<Bullet>();
    List<Bullet> _enemyBullets = new List<Bullet>();

    public List<Bullet> playerBullets { get { return _playerBullets; } }
    public List<Bullet> enemyBullets { get { return _enemyBullets; } }

    public BulletManager()
    {
        for (int i = 0; i < 10000; ++i)
        {
            Bullet bullet = new Bullet();
            _inactiveBullets.Push(bullet);
        }
    }


    public void CreateBullet(Bullet.Shooter whoShotme, Vector2f position, Vector2f velocity)
    {
        if (_inactiveBullets.Count == 0) return;
        Bullet newBullet = _inactiveBullets.Pop();
        newBullet.SetActive(true);
        newBullet.position = position;
        newBullet.velocity = velocity;
        newBullet.SetShooter(whoShotme);

        switch (whoShotme)
        {
            case Bullet.Shooter.PLAYER:     _playerBullets.Add(newBullet);      break;
            case Bullet.Shooter.ENEMY:      _enemyBullets.Add(newBullet);       break;
        }
    }

    public void Update()
    {
        // Enemy
        for (int i = _enemyBullets.Count - 1; i >= 0; i--)
        {
            _enemyBullets[i].Update();
            _inactiveBullets.Push(_enemyBullets[i]);
            _enemyBullets.RemoveAt(i);
        }

        // Player
        for (int i = _playerBullets.Count - 1; i >= 0; i--)
        {
            _playerBullets[i].Update();
            _inactiveBullets.Push(_playerBullets[i]);
            _playerBullets.RemoveAt(i);
        }
    }

    public void DrawBullets(RenderWindow window)
    {
        foreach (Bullet bullet in _enemyBullets) window.Draw(bullet);
        foreach (Bullet bullet in _playerBullets) window.Draw(bullet);
    }
}

