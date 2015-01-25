using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;


class Menu : Drawable
{
    // Menu Text

    Text _play;
    Text _highScore;
    Text _quit;
    Text _title;
    Text _authors;
    Font _font = Assets.GetFont("../../fonts/arial.ttf");
    Sprite _pointer = new Sprite(Assets.GetTexture("../../images/Arrow.png"));
    Vector2f[] menuPositions = new Vector2f[3];
    int curMenuOption = 0;
    enum MenuSection { TITLE, HOME, END };
    MenuSection e_MenuSection;
    RectangleShape blackout = new RectangleShape(new Vector2f(Game.RES_WIDTH, Game.RES_HEIGHT));
    Byte alphaBlackOut = 0;
    float timer = 0;
    float timer2 = 0;


    public Menu()
    {
        // Setup Title Menu Text
        e_MenuSection = MenuSection.TITLE;
        blackout.FillColor = new Color(0, 0, 0, alphaBlackOut);

        _title = new Text("Transfeather: Fight of what do we do now?", _font);
        _title.Position = new Vector2f(Game.RES_WIDTH * 0.02f, Game.RES_HEIGHT * 0.1f);
        _title.CharacterSize = 64;

        _authors = new Text("Created by: Alex Ellis , Matt Armes , Sam Lighton, Sam Haxworth", _font);
        _authors.Position = new Vector2f(Game.RES_WIDTH * 0.3f, Game.RES_HEIGHT * 0.5f);
        _authors.CharacterSize = 24;
       

        // MainMenu
        _play = new Text("Play Game", _font);
        _highScore = new Text("High Scores", _font);
        _quit = new Text("Quit Game", _font);

        _play.Position = new Vector2f(200, 300);
        _highScore.Position = new Vector2f(200, 350);
        _quit.Position = new Vector2f(200, 400);

        menuPositions[0] = _play.Position;
        menuPositions[1] = _highScore.Position;
        menuPositions[2] = _quit.Position;

        menuPositions[0].X -= 80;
        menuPositions[1].X -= 80;
        menuPositions[2].X -= 80;

        // Pointer
        _pointer.Position = _play.Position;
        _pointer.Position = new Vector2f(_play.Position.X - 80.0f, _play.Position.Y);
    }

    public void Update()
    {
        switch (e_MenuSection)
        {
            case MenuSection.TITLE:
                if (FadeOut()) { timer2 = 0; e_MenuSection = MenuSection.HOME; }
                break;
            case MenuSection.HOME:
                FadeIn();
                if (Input.IsKeyReleased(Keyboard.Key.Down)) { if (++curMenuOption == 3) curMenuOption = 0; _pointer.Position = menuPositions[curMenuOption]; }
                if (Input.IsKeyReleased(Keyboard.Key.Up)) { if (--curMenuOption == -1) curMenuOption = 2; _pointer.Position = menuPositions[curMenuOption]; }
                break;
            case MenuSection.END:
                break;
        }
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        switch (e_MenuSection)
        {
            case MenuSection.TITLE:
                    target.Draw(_title);
                    target.Draw(_authors);
                    target.Draw(blackout);
                break;
            case MenuSection.HOME:
                    target.Draw(_title);
                target.Draw(_pointer);
                target.Draw(_play);
                target.Draw(_highScore);
                target.Draw(_quit);
                target.Draw(blackout);
                break;
            case MenuSection.END:
                break;
        }
    }

    bool FadeOut()
    {
        timer2 += Time.deltaTime;
        if (timer2 > 1) timer += Time.deltaTime;
        if (timer > 0.01)
        {
            timer = 0;
            blackout.FillColor = new Color(0, 0, 0, ++alphaBlackOut);
        }   return alphaBlackOut > 254;
    }
    void FadeIn()
    {
        timer += Time.deltaTime;
        if (timer > 0.01 && alphaBlackOut > 1)
        {
            timer = 0;
            blackout.FillColor = new Color(0, 0, 0, --alphaBlackOut);
        }
    }
}
