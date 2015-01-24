using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class PlanetManager
{
    Planet[] _planets = new Planet[5];

    public Planet[] planets { get { return _planets; } }

    public PlanetManager()
    {
        Random random = new Random();
        

        for (int i = 0; i < _planets.Length; ++i)
        {
            _planets[i] = new Planet();
            _planets[i].position = new Vector2f(200, 200);//random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
            _planets[i].velocity = new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
            _planets[i].sprite.FillColor = Color.Red;
            _planets[i].sprite.Radius = 16;
            _planets[i].sprite.Origin = new Vector2f(16, 16);
            _planets[i].angularVelocity = random.Next(10) + 10;
        }
    }


    public void Update(List<Bullet> bullets)
    {
        foreach (Planet planet in _planets) planet.Update(bullets);
    }

    public void DrawPlanets(RenderWindow window)
    {
        foreach (Planet planet in _planets) window.Draw(planet);
    }

}

