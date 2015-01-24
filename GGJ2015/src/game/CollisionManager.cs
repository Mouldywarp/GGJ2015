using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class CollisionManager
{
    public CollisionManager()
    {
    }


    public void Update(List<Bullet> playerBullets, List<Bullet> enemyBullets, Planet[] planets, CircleShape player)
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

        foreach (Planet planet in planets)
        {
            // 1 Player

            // This IF checks if the player is in the planet's gravitational field
            if (CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, player.Position, player.Radius))
            {

                // Kaboom player if colliding with planet function


                // Colliding between planets and player
                if (CircleMath.Intersects(planet.position, planet.radius, player.Position, player.Radius))
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
        foreach (Bullet bullet in playerBullets)
        {
            foreach (Planet planet in planets)
            {
                // If intersecting with gravity field
                if (CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                {
                    // Then calculate the gravitational time effect
                    CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);

                    // Bullet Collide with Planet
                    if (CircleMath.Intersects(planet.position, planet.radius, bullet.position, bullet.radius))
                    {
                        bullet.SetActive(false);    // Destroy bullet
                    }
                }
            }

            // Bullet Collide with enemy
            // foreach(Enemy enemy in enemies)

        }

        // ========================================================================================================

        // Enemy Bullets
        foreach (Bullet bullet in enemyBullets)
        {
            foreach (Planet planet in planets)
            {
                // If intersecting with gravity field
                if (CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                {
                    // Then calculate the gravitational time effect
                    CircleMath.CalculateGravitationalTimeEffect(planet, bullet.position);

                    // Bullet Collide with Planet
                    if (CircleMath.Intersects(planet.position, planet.radius, bullet.position, bullet.radius))
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