using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

/*! \brief Loads assets into memory such as textures and sound
 *         
 *  This class is a singleton with static functions that can be used anywhere in the app
 *  to request assets.
 */
public class Assets
{
    // Single static instance of assets, instantiated once when inst accessed for the first time, otherwise inst returns previously instantiated object
    static Assets _inst;
    static Assets inst { get { if (_inst == null) _inst = new Assets(); return _inst; } }

    // Variable for holding the loaded data
    SortedList<string, Texture> _textures;
    SortedList<string, Font> _fonts;


    // Constructor needs to init vars
    private Assets()
    {
        _textures = new SortedList<string, Texture>();
        _fonts = new SortedList<string, Font>();
    }

    //! Returns texture at filepath
    public static Texture GetTexture(string filePath)
    {
        // If texture already loaded then return it
        if (inst._textures.ContainsKey(filePath)) return inst._textures[filePath];

        // ..Else try to load texture and return it
        Texture tex = new Texture(filePath);
        if (tex != null)
        {
            inst._textures.Add(filePath, tex);
            return tex;
        }

        // ..Else couldn't load, return null
        Console.Write("Couldn't find file " + filePath + "\n");
        return null;
    }

    // Returns font at filepath
    public static Font GetFont(string filePath)
    {
        // If texture already loaded then return it
        if (inst._fonts.ContainsKey(filePath)) return inst._fonts[filePath];

        // ..Else try to load texture and return it
        Font font = new Font(filePath);
        if (font != null)
        {
            inst._fonts.Add(filePath, font);
            return font;
        }

        // ..Else couldn't load, return null
        Console.Write("Couldn't find file " + filePath + "\n");
        return null;
    }
}

