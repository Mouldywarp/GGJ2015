using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

// Fast but shitty way of doing this, all behaviour (movement and bullet patterns) in one class, different update functions
// called based on type of behaviour. That way we can just slap on Enemy
class EnemyBehaviour
{
    public Enemy _myEnemy;
    public enum Type { SIMPLE, MANIC };


    public EnemyBehaviour(Enemy enemy)
    {
        _myEnemy = enemy;


        Update = SimpleUpdate;


    }


    public delegate void UpdateDelegate();
    public UpdateDelegate Update;


    public void SimpleUpdate()
    {
        Console.WriteLine("I am simple");
    }

    public void ManicUpdate()
    {
        Console.WriteLine("I am manic");
    }



}
