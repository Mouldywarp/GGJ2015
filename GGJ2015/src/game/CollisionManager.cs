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

        foreach (Planet planet in _planets)
        {

            // 1 Player

            // This IF checks if the player is in the planet's gravitational field
            if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, _player.position, _player.radius))
            {

                // Kaboom player if colliding with planet function


                // Colliding between planets and player
                if (CircleMath.Intersects(planet.sprite.Position, planet.radius, _player.position, _player.radius))
                {


                }
            }


           // Planets with enemy 

           /* foreach (Enemy enemy in enemies)
            {
                // This IF checks if the enemy is in the planet's gravitational field
                if (CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, enemy.Position, enemy.Radius))
                {

                    // Kaboom enemy if colliding with planet function


                    // Colliding between planets and enemies
                    if (CircleMath.Intersects(planet.position, planet.radius, player.Position, player.Radius))
                    {

                    }
                }
            }*/
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

            foreach (Planet planet in _planets)
            {
                if (planet.isActive)
                {
                    // If intersecting with gravity field
                    if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                    {
                        // Then calculate the gravitational time effect
                        CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);

                        // Bullet Collide with Planet
                        if (CircleMath.Intersects(planet.sprite.Position, planet.radius, bullet.position, bullet.radius))
                        {
                            _player.score(10);
                            bullet.SetActive(false);    // Destroy bullet
                        }
                    }
                }
            }

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

            foreach (Planet planet in _planets)
            {
                if (planet.isActive)
                {
                    // If intersecting with gravity field
                    if (CircleMath.Intersects(planet.sprite.Position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                    {
                        // Then calculate the gravitational time effect
                        CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);

                        // Bullet Collide with Planet
                        if (CircleMath.Intersects(planet.sprite.Position, planet.radius, bullet.position, bullet.radius))
                        {
                            bullet.SetActive(false);    // Destroy bullet
                        }

                    }
                }

                // Bullet Collide with player
            }


            // Bullet Collisions

            // Player Collisions

        }

    }

}