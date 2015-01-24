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


    public void Update(List<Bullet> bullets, Planet[] planets, CircleShape player)
    {
        /* Every player bullet with enemy with planet
         * Every enemy bullet with player with planet
         * Every planet with player with enemy
         *                                          */




        // Bullet Collisions
        BulletCollision(bullets, planets, player);

        // Planet Collisions

        // Player Collisions
    }

    private void BulletCollision(List<Bullet> bullets, Planet[] planets, CircleShape player)
    {
        foreach(Bullet bullet in bullets)
        {
            foreach(Planet planet in planets)
            {
                // Calculate Gravity Formula
                if(CircleMath.Intersects(planet.position, planet.gravitationalFieldRadius, bullet.position, bullet.radius))
                {
                    float density = mass / (PI * radius * radius);
                    float GravEffect = density / (CircleMath.GetSquaredDistanceBetween(this.position, bullet.position));

                    // Bullet Collide with Planet
                    if (CircleMath.Intersects(planet.position, planet.radius, bullet.position, bullet.radius))
                    {
                        bullet.SetActive(false);    // Destroy bullet
                    }
                }
            }

            // Enemy Bullet Collide with Player
            if(
        }
    }
}