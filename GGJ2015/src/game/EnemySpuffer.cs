using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;


class EnemySpuffer
{
    //Random _random = new Random();
    Stack<Enemy> _inactiveEnemies = new Stack<Enemy>();
    List<Enemy> _activeEnemies = new List<Enemy>();

    public List<Enemy> enemies { get { return _activeEnemies; } }


    float _spuffTimer = 0;
    float _spuffFrequency = 1.0f;


    public EnemySpuffer(BulletManager manageMe)
    {
        for (int i = 0; i < 20; ++i)
        {
            Enemy enemy = new Enemy(manageMe);
            _inactiveEnemies.Push(enemy);
        }
    }


    public void Reset()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            _activeEnemies[i].SetActive(false);
            _inactiveEnemies.Push(_activeEnemies[i]);
            _activeEnemies.RemoveAt(i);
        }
    }



    public void CreateEnemy(Vector2f position, EnemyBehaviour.Type type)
    {
        if (_inactiveEnemies.Count == 0) return;
        Enemy newEnemy = _inactiveEnemies.Pop();
        newEnemy.SetActive(true);
        newEnemy.position = position;
        newEnemy.OnCreate(type);
        _activeEnemies.Add(newEnemy);
    }



    public void Update()
    {
        _spuffTimer += Time.deltaTime;
        if (_spuffTimer >= _spuffFrequency)
        {
            _spuffTimer = 0;
            int max = (int)EnemyBehaviour.Type.NUM_BEHAVIOURS;
            int type = Game.random.Next(max);

            for (int i = 0; i < 3; i++)
            {
                CreateEnemy(new Vector2f(Game.RES_WIDTH + Game.random.Next(150, 750), Game.random.Next(50, Game.RES_HEIGHT - 50)), (EnemyBehaviour.Type)type);
                type = Game.random.Next(max);
            }
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

