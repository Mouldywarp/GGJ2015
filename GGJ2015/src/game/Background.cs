using System;
using System.Diagnostics;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

class Background
{

    Sprite _sprite;
    Texture _texture;

    int _width, _height, _inc;

    float _scrollSpeed,_scrollTimer;

    public Background(int width, int height)
    {
        _width = width;
        _height = height;
        _inc = 0;
        _scrollSpeed = 0.04f;
        _scrollTimer = 0;
        _texture = new Texture(Assets.GetTexture("../../images/BackGround.png"));
        _texture.Repeated = true;
        _sprite = new Sprite(_texture);
        _sprite.Scale = new Vector2f(2, 2);
        _sprite.Position = new Vector2f(0, 0);
    }

    public void update()
    {
        _scrollTimer += Time.deltaTime;
        if (_scrollTimer > _scrollSpeed)
        {
            _inc++;
            _sprite.TextureRect = new IntRect(_inc, 0, _width, _height);
            _scrollTimer = 0;
        }
    }
    
    public void Draw(RenderWindow _window)
    {
        _window.Draw(_sprite);
    }

}

