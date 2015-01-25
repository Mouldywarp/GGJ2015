using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class CollisionManager
{
    List<Bullet> _playerBullets;
    List<Bullet> _enemyBullets;
    List<Enemy> _enemies;
    Planet[] _planets;
    Player _player;

    public CollisionManager(List<Bullet> playerBullets, List<Bullet> enemyBullets, List<Enemy> enemies, Planet[] planets, Player player)
    {
        _playerBullets = playerBullets;
        _enemyBullets = enemyBullets;
        _enemies = enemies;
        _planets = planets;
        _player = player;
    }


    public void Update()
    {
        /* Every player bullet with enemy with planet
         * Every enemy bullet with player with planet
         * Every planet with player with enemy
         *                                          */

        /*  Every planet with player and enemy
         *  Every playerBullet with planets and enemies(WIP)
         *  Every enemyBullet with planets and player(WIP)
         * */




        // Planets with player ========================================================================================================
        bool playerBummedByPlanet = false;
        foreach (Planet planet in _planets)
        {


            // 1 Player

            // This IF checks if the player is in the planet's gravitational field
            if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, _player.position, _player.radius))
            {

                //_player.timeScalar = (float)CircleMath.CalculateGravitationalTimeEffect(planet, _player.position);
                _player.timeScalar = 0.5f;

                // Then calculate the gravitational time effect
                playerBummedByPlanet = true;

            }
        }
        if (!playerBummedByPlanet) _player.timeScalar = 1;



        
        foreach (Enemy enemy in _enemies)
        {
            bool enemyBummedByPlanet = false;
            foreach (Planet planet in _planets)
            {
                // This IF checks if the enemy is in the planet's gravitational field
                if (CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, enemy.position, enemy.radius))
                {

                    //enemy.timeScalar = (float)CircleMath.CalculateGravitationalTimeEffect(planet, enemy.position);
                    enemy.timeScalar = (0.7f);
                    enemyBummedByPlanet = true;
                }
            }
            if (!enemyBummedByPlanet) enemy.timeScalar = 1;
        }


        // ========================================================================================================

        // Player Bullets
        foreach (Bullet bullet in _playerBullets)
        {

            // Check if offscreen
            if (CircleMath.OffScreen(bullet.bounds))
            {
                // Delete Stuff
                bullet.SetActive(false);
                continue;
            }

            bool playerBulBum = false;

            foreach (Planet planet in _planets)
            {
                if (planet.isActive)
                {
                    // If intersecting with gravity field
                    if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                    {
                        //bullet.timeScalar = (float)CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);
                        bullet.timeScalar = 0.3f;
                        playerBulBum = true;

                        // Bullet Collide with Planet
                        /*
                        if (CircleMath.Intersects(planet.sprite.Position, planet.radius, bullet.position, bullet.radius))
                        {
                            _player.score(10);
                            bullet.SetActive(false);    // Destroy bullet
                        }
                         * */
                    }
                }
            }

            if (!playerBulBum) bullet.timeScalar = 1;

            // Bullet Collide with enemy
            foreach (Enemy enemy in _enemies)
            {
                if (CircleMath.Intersects(bullet.position, bullet.radius, enemy.position, enemy.radius))
                {
                    _player.score(10);
                    bullet.SetActive(false);    // Destroy bullet
                    enemy.OnHit();
                }
            }

        }

        // ========================================================================================================

        // Enemy Bullets
        foreach (Bullet bullet in _enemyBullets)
        {

            //Check if offscreen
            if (CircleMath.OffScreen(bullet.bounds))
            {
                // Delete Stuff
                bullet.SetActive(false);
                continue;
            }

            bool enBulBum = false;

            foreach (Planet planet in _planets)
            {
                if (planet.isActive)
                {
                    // If intersecting with gravity field
                    if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                    {
                        //bullet.timeScalar = (float)CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);
                        bullet.timeScalar = 0.3f;
                        enBulBum = true;



                        // Bullet Collide with Planet
                        /*
                        if (CircleMath.Intersects(planet.sprite.Position, planet.radius, bullet.position, bullet.radius))
                        {
                            bullet.SetActive(false);    // Destroy bullet
                        }
                         * */

                    }
                    
                }
            }

            if(!enBulBum) bullet.timeScalar = 1;

            //Player
            if (_player.isAlive)
            {
                if (CircleMath.Intersects(_player.position, _player.radius, bullet.position, bullet.radius)) _player.Die();
            }

        }

    }

}