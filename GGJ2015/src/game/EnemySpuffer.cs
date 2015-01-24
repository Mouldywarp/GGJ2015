﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class EnemySpuffer
{
    Random _random = new Random();
    Stack<Enemy> _inactiveEnemies = new Stack<Enemy>();
    List<Enemy> _activeEnemies = new List<Enemy>();

    public List<Enemy> enemies { get { return _activeEnemies; } }


    float _spuffTimer = 0;
    float _spuffFrequency = 0.5f;


    public EnemySpuffer(BulletManager manageMe)
    {
        for (int i = 0; i < 100; ++i)
        {
            Enemy enemy = new Enemy(manageMe);
            _inactiveEnemies.Push(enemy);
        }
    }



    public void CreateEnemy(Vector2f position, Vector2f velocity)
    {
        if (_inactiveEnemies.Count == 0) return;
        Enemy newEnemy = _inactiveEnemies.Pop();
        newEnemy.SetActive(true);
        newEnemy.position = position;
        _activeEnemies.Add(newEnemy);
        newEnemy.velocity = velocity;
    }



    public void Update()
    {
        _spuffTimer += Time.deltaTime;
        if (_spuffTimer >= _spuffFrequency)
        {
            _spuffTimer = 0;
            CreateEnemy(new Vector2f(Game.RES_WIDTH + 100, _random.Next(50, Game.RES_HEIGHT - 50)), new Vector2f(-200, 0));
        }



        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            _activeEnemies[i].Update();

            if (!_activeEnemies[i].isActive)
            {
                _inactiveEnemies.Push(_activeEnemies[i]);
                _activeEnemies.RemoveAt(i);
            }
        }
    }

    public void DrawEnemies(RenderWindow window)
    {
        foreach (Enemy enemy in _activeEnemies) window.Draw(enemy);
    }
}

