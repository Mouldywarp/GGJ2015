using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class PlanetManager
{
    Planet[] _planets = new Planet[3];
   // Texture[] _textures = new Texture[2];

    public Planet[] planets { get { return _planets; } }

    public PlanetManager()
    {
        //   _textures[0] = Assets.GetTexture("../../images/HassleMoon.png");
        //  _textures[1] = Assets.GetTexture("../../images/LionelMoon.png");

        // Planet Implementation
        _planets[0] = new Planet(Assets.GetTexture("../../images/HassleMoon.png"));
        _planets[1] = new Planet( Assets.GetTexture("../../images/LionelMoon.png"));
        _planets[2] = new Planet( Assets.GetTexture("../../images/HassleMoon.png"));
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

