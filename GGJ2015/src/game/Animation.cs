using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;

/*! \brief Simple animation class
 *         
 *  
 */
class Animation : Drawable
{
    //! Struct for storing start and end frmae of each animation clip, to slice up animation if required
    public struct Clip
    {
        public Clip(int startFrame, int endFrame) { _startFrame = startFrame; _endFrame = endFrame; }
        int _startFrame;
        int _endFrame;
        public int start { get { return _startFrame; } }
        public int end { get { return _endFrame; } }
    }
    private List<Clip> _clips = new List<Clip>();
    private int _currentClip = -1;

    // Clips
    public FloatRect bounds { get { return _sprite.GetGlobalBounds(); } }
    private Sprite _sprite;
    private int _tilesInRow;
    private Vector2i _tileSize = new Vector2i(32, 32); // default = 32 X 32 pixels per tile
    private bool _isPlaying = false;
    private float _animTimer = 0; //!< Timer for changing anims. As animation so basic I've left in this class but could break down into dedicated class later if gets messy
    private int _frame = 0; //!< Frame for anim
    private float _animDelay; //!< Delay in secs till anim changes frames
    public float speed { set { _animDelay = value; } }
    public Vector2f position { set { _sprite.Position = value; } get { return _sprite.Position; } }
    public Color tint { set { _sprite.Color = value; } }
    public Vector2i tileSize
    {
        set
        { 
            _tileSize = value;
            _sprite.Origin = new Vector2f((float)value.X * 0.5f, (float)value.Y * 0.5f);
        }
    }

    public Animation(Texture spriteSheet, int tilesInRow)
    {
        _sprite = new Sprite(spriteSheet);
        _tilesInRow = tilesInRow;
    }

    //! Add a new animation clip to the animation
    public int AddClip(int startFrame, int endFrame)
    {
        _clips.Add(new Clip(startFrame, endFrame));
        if (_clips.Count == 1)
        {
            SetCurrentClip(0); //Set as current clip if this is the first one being added
        }
        return _clips.Count - 1; // return index of clip in list
    }

    //!< Set current clip of animation
    public void SetCurrentClip(int clip)
    {
        if (clip < 0 || clip >= _clips.Count || clip == _currentClip) return;
        _currentClip = clip;
        _frame = _clips[_currentClip].start;
        UpdateSpriteRect();
    }

    //! Play animation
    public void Play()
    {
        if (_clips.Count == 0) return;
        _isPlaying = true;
    }

    //! Pause animation
    public void Pause()
    {
        _isPlaying = false;
    }

    //! Stop Animation and return to start of clip
    public void Stop()
    {
        _isPlaying = false;
        if (_clips.Count != 0)
        {
            _frame = _clips[_currentClip].start;
            UpdateSpriteRect();
        }
    }

    //! Draw to screen
    public void Draw(RenderTarget target, RenderStates states)
    {
        if (_isPlaying) UpdateAnimation(); // note cannot update anim if not playing and can't play if no clips - follow that rule and there will be no access vilations due to trying to play non-existant anim clip
        _sprite.Draw(target, states);
    }

    //! Update animation frame
    void UpdateAnimation()
    {
        // Update Animation
        _animTimer += Time.deltaTime;
        if (_animTimer >= _animDelay)
        {
            _frame += 1;
            if (_frame > _clips[_currentClip].end)
                _frame = _clips[_currentClip].start;

            UpdateSpriteRect();
            _animTimer = 0;
        }
    }


    //! Change the rectangle of sprite to match frame
    void UpdateSpriteRect()
    {
        _sprite.TextureRect = TileMath.IndexToRect(_frame, _tilesInRow, _tileSize);
    }


}

