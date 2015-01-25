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
    public enum Type { SIMPLE, DIVE, NUM_BEHAVIOURS };
    UpdateDelegate[] _updateFuncs;

    // Delegates for behaviours, sorry
    public delegate void UpdateDelegate();
    public UpdateDelegate Update;

    // Construct meh
    public EnemyBehaviour(Enemy enemy)
    {
        _myEnemy = enemy;

        // Add delegate fucntions to an array
        _updateFuncs = new UpdateDelegate[(int)Type.NUM_BEHAVIOURS];
        _updateFuncs[(int)Type.SIMPLE] = SimpleUpdate;
        _updateFuncs[(int)Type.DIVE] = ManicUpdate;

        // Default Behaviour
        SetBehaviour(Type.SIMPLE);
    }

    public void SetBehaviour(EnemyBehaviour.Type type)
    {
        if (type == Type.NUM_BEHAVIOURS) return;
        Update = _updateFuncs[(int)type];
        Initialize(type);
    }

    void Initialize(EnemyBehaviour.Type type)
    {
        switch (type)
        {
            case Type.SIMPLE:
                _myEnemy.velocity = new Vector2f(-150, 0);
                break;

            case Type.DIVE:
                float centre = Game.RES_HEIGHT * 0.5f;
                if (_myEnemy.position.Y < centre) _myEnemy.velocity = new Vector2f(-200, 200);
                else _myEnemy.velocity = new Vector2f(-200, -200);
                break;
        }
    }



    // ALL THE DIRTY UPDATE FUNCTIONS!
    public void SimpleUpdate()
    {
    }

    public void ManicUpdate()
    {
        if (Math.Abs(_myEnemy.velocity.Y) > 10)
        {
            _myEnemy.velocity = new Vector2f(_myEnemy.velocity.X, _myEnemy.velocity.Y * (1 - Time.deltaTime));
        }
    }



}
