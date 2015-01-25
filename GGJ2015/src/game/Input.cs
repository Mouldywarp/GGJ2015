using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Input
{
    private HashSet<Keyboard.Key> _hassleHoff = new HashSet<Keyboard.Key>();
    private static bool[] pressed = new bool[(int)Keyboard.Key.KeyCount];

    static Input _inst;
    static Input inst { get { if (_inst == null) _inst = new Input(); return _inst; } }

    private Input()
    {
    }

    public static bool getKey(Keyboard.Key i)
    {
        foreach (Keyboard.Key k in inst._hassleHoff)
        {
            if(i==k)
                return true; 
        }
        return false;
    }
    public static void keyPressed(Keyboard.Key i)
    {
        inst._hassleHoff.Add(i);
    }
    public static void keyReleased(Keyboard.Key i)
    {
        if (inst._hassleHoff.Contains(i)==true)
        {
            pressed[(int)i] = true;
            inst._hassleHoff.Remove(i);
        }
    }

    public static bool IsKeyReleased(Keyboard.Key i)
    {
        if (pressed[(int)i])
        {
            pressed[(int)i] = false;
            return true;
        }   else return false;
    }
}

