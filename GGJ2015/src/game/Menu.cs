using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Menu : Drawable
{
    // Menu Text

    Text _title;
    Text _pressGToPlay;
    Font _font;

    public Menu()
    {
        // Setup Main Menu Text

        _font = Assets.GetFont("../../fonts/arial.ttf");

        _title = new Text("Title Goes Here", _font);
        _title.Position = new Vector2f(20, 20);
        _title.CharacterSize = 24;

        _pressGToPlay = new Text("press G to play", _font);
        _pressGToPlay.Position = new Vector2f(20, 50);
        _pressGToPlay.CharacterSize = 24;
    }




    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(_title);
        target.Draw(_pressGToPlay);
    }

}
