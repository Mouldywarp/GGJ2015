﻿using System;
using SFML.Window;
using SFML.Graphics;

/*! \brief Static functions for commonly used functions for calulcating gravity
 * 
 * Gravity is fun!
 * */
class CircleMath
{
    const float PI = 3.1412f;

    //! Checks an objects distance to a circle
    public static bool Intersects(Vector2f p1, float rad1, Vector2f p2, float rad2)
    {
       float dist = GetSquaredDistanceBetween(p1, p2);          // Get Squared distance between

        // Radius Lengths
        float rad = rad1 + rad2;
        rad *= rad;                                             // compare rad^2 to c^2

        return dist < rad;
    }

    // Returns Distance From a Circle to a Circle
    public static float GetSquaredDistanceBetween(Vector2f point1, Vector2f point2)
    {
        // Distance Lengths
        Vector2f distanceBetween = point2 - point1;
        distanceBetween.X *= distanceBetween.X;                  // x^2
        distanceBetween.Y *= distanceBetween.Y;                  // y^2
        return distanceBetween.X + distanceBetween.Y;            // dist = c^2
    }


    public static float CalculateGravitationalTimeEffect(Planet planet, Vector2f objPosition)
    {
        // Then calculate the gravitational time effect
        float density = planet.mass / (PI * planet.radius * planet.radius);
        return density / (CircleMath.GetSquaredDistanceBetween(planet.position, objPosition));      // Grav Effect
    }
}

