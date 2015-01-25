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
    private delegate void UpdateDelegate();
    private UpdateDelegate UpdateSpecific;

    // Reusable variables
    float _timer = 0; // time since enemy spawned
    int _bulletsShot = 0; // bullets shot since spawned
    float _delayTimer = 0; // Custom timer

    // Construct meh
    public EnemyBehaviour(Enemy enemy)
    {
        _myEnemy = enemy;

        // Add delegate fucntions to an array
        _updateFuncs = new UpdateDelegate[(int)Type.NUM_BEHAVIOURS];
        _updateFuncs[(int)Type.SIMPLE] = SimpleUpdate;
        _updateFuncs[(int)Type.DIVE] = DiveUpdate;

        // Default Behaviour
        SetBehaviour(Type.SIMPLE);
    }

    public void SetBehaviour(EnemyBehaviour.Type type)
    {
        if (type == Type.NUM_BEHAVIOURS) return;
        UpdateSpecific = _updateFuncs[(int)type];
        Initialize(type);
    }

    void Initialize(EnemyBehaviour.Type type)
    {
        _timer = 0;
        _bulletsShot = 0;
        _delayTimer = 0;

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

    // Regular update
    public void Update()
    {
        if (_myEnemy.onScreen)
        {
            _timer += Time.deltaTime;
            _delayTimer += Time.deltaTime;
        }

        // Specific behaviour logic hnear
        UpdateSpecific();
    }

    // Shoot stuff
    private void Shoot(Vector2f velo)
    {
        _myEnemy.Shoot(velo);
        _bulletsShot += 1;
    }


    // ALL THE DIRTY UPDATE FUNCTIONS!
    private void SimpleUpdate()
    {
        if (_timer > 0.8f && _bulletsShot < 20)
        {
            Shoot(new Vector2f(Game.random.Next(-280, -180), Game.random.Next(-80, 80)));
        }
    }

    private void DiveUpdate()
    {
        // move
        if (Math.Abs(_myEnemy.velocity.Y) > 10)
        {
            _myEnemy.velocity = new Vector2f(_myEnemy.velocity.X, _myEnemy.velocity.Y * (1 - Time.deltaTime));
        }

        // shoot
        if (_timer < 0.5f || _timer > 6.0f) _delayTimer = 0;
        else if (_delayTimer > 0.9f)
        {
            _delayTimer = 0;

            // Spuff me sum buletz
            int rootAng = Game.random.Next(90, 270);

            for (int i = 0; i < 30; i++)
            {
                float ang = Game.random.Next(rootAng - 2, rootAng + 2);
                int speed = Game.random.Next(230, 280);
                Vector2f dir = new Vector2f((float)Math.Cos(ang), (float)Math.Sin(ang));

                Shoot(dir * speed);

            }
        }
    }



}
