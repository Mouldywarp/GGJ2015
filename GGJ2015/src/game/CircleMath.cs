using System;
using SFML.Window;
using SFML.Graphics;

/*! \brief Static functions for commonly used functions for calulcating gravity
 * 
 * Gravity is fun!
 * */
class CircleMath
{
    //! Checks an objects distance to a circle
    public static bool Intersects(Vector2f p1, float rad1, Vector2f p2, float rad2)
    {
        // Distance Lengths
        Vector2f distanceBetween = p2 - p1;
        distanceBetween.X *= distanceBetween.X;
        distanceBetween.Y *= distanceBetween.Y;
        float dist = distanceBetween.X + distanceBetween.Y;

        // Radius Lengths
        float rad = rad1 + rad2;
        rad *= rad;

        return dist < rad;
    }
}

