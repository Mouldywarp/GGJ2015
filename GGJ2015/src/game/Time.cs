using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

/*! \brief Time class
 *         
 *  Singleton mainly used to query delta time
 */
public class Time
{
    static Time _inst; //!< Single instance of time
    private Stopwatch _stopwatch; //!< Stopwatch to measure time
    private float _deltaTime = 0;

    static public float deltaTime { get { if (_inst != null) return _inst._deltaTime; return 0; } }

    // Static function to create single instance and hook up to game
    public static void CreateTime(Game game)
    {
        if (_inst != null) return;
        _inst = new Time(game);
    }

    // Constructor starts time and links update event with game
    private Time(Game game)
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        game.UpdateEvent += new Game.UpdateEventHandler(OnUpdate);
    }

    void OnUpdate()
    {
        _deltaTime = (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();
    }
}

