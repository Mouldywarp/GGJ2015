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
            _planets[i] = new Planet(random);
        }
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

