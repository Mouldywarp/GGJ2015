using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class PlanetManager
{
    int _currentPlanet = 0;
    Planet[] _planets = new Planet[3];
    float _planetFreq = 30;
    float _timeMeBro;

    public Planet[] planets { get { return _planets; } }

    public PlanetManager()
    {

        // Planet Implementation
        _planets[0] = new Planet(Assets.GetTexture("../../images/HassleMoon.png"));
        _planets[0].mass = 100;

        
        _planets[1] = new Planet(Assets.GetTexture("../../images/LionelMoon.png"));
        _planets[1].mass = 200;


        _planets[2] = new Planet(Assets.GetTexture("../../images/HassleMoon.png"));
        _planets[2].mass = 50;
         

        _timeMeBro = 1;
    }


    public void Update()
    {
        foreach (Planet planet in _planets) planet.Update();

        _timeMeBro -= Time.deltaTime;
        if (_timeMeBro <= 0)
        {
            MakePlanet();
            _timeMeBro = _planetFreq;
        }
    }

    private void MakePlanet()
    {
        if (_planets[_currentPlanet].isActive) return;
        _planets[_currentPlanet].SetActive(true);
        _planets[_currentPlanet].Reset();
        _currentPlanet += 1;
        if(_currentPlanet >= planets.Length) _currentPlanet = 0;
    }

    public void DrawPlanets(RenderWindow window)
    {
        foreach (Planet planet in _planets) window.Draw(planet);
    }

}

