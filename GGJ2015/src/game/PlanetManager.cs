using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class PlanetManager
{
    Planet[] _planets = new Planet[2];

    public Planet[] planets { get { return _planets; } }

    public PlanetManager()
    {
        Random random = new Random();
        

        for (int i = 0; i < _planets.Length; ++i)
        {
            _planets[i] = new Planet();
            _planets[i].position = new Vector2f(200, 200);//random.Next(Game.RES_WIDTH), random.Next(Game.RES_HEIGHT));
            _planets[i].velocity = new Vector2f(0, 0);//new Vector2f(random.Next(-20, 20), random.Next(-20, 20));
            _planets[i].sprite.FillColor = Color.Red;
            _planets[i].sprite.Radius = 16;
            _planets[i].sprite.Origin = new Vector2f(16, 16);
            _planets[i].angularVelocity = random.Next(10) + 10;
            _planets[i].gravitationalFieldRadius = _planets[i].sprite.Radius + 8;
        }

        _planets[1].position = new Vector2f(300, 300);
            _planets[1].sprite.Radius = 32;
            _planets[1].sprite.Origin = new Vector2f(32, 32);
    }


    public void Update()
    {
        foreach (Planet planet in _planets) planet.Update();
    }

    public void DrawPlanets(RenderWindow window)
    {
        foreach (Planet planet in _planets) window.Draw(planet);
    }

}

