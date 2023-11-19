using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using static System.Net.Mime.MediaTypeNames;

namespace custom_program
{
    public class Player : Sprite
    {
        private DrawingOptions _opt;
        private Animation _anim;
        private AnimationScript _script;
        private bool _hit;
        private int _score;
        public bool Hit
        {
            get
            {
                return _hit;
            }

            set
            {
                _hit = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }
        public Player()
        {
            _position.X = SplashKit.CurrentWindowWidth()/2 - 16;
            _position.Y = SplashKit.CurrentWindowHeight() - 50;
            _speed = 5;
            _score = 0;
            _image = SplashKit.LoadBitmap("player", "shroom.png");
            _hit = false;

            //drawing options
            _opt.Dest = SplashKit.CurrentWindow();
            _opt.FlipY = false;

            _image.SetCellDetails(32, 32, 5, 5, 25);

            //creating animation from script 
            _script = SplashKit.LoadAnimationScript("script", "animation-script.txt");
            _anim = _script.CreateAnimation("Idle"); //default animation is player being idle
            _opt = SplashKit.OptionWithAnimation(_anim);

            //scaling image to appropriate size
            _opt.ScaleX = 2;
            _opt.ScaleY = 2;

        }
        

        public override void Draw()
        {
            SplashKit.DrawBitmap(_image, _position.X, _position.Y, _opt);
            
        }

        public override void Update()
        {
            _anim.Update(); //updates animation

            if (SplashKit.KeyDown(KeyCode.DKey))
            {
                _opt.FlipY = false;

                if (_position.X < SplashKit.CurrentWindowWidth() - 32)
                {
                    _position.X += _speed;
                    if (SplashKit.AnimationName(_anim) != "move")
                    {
                        _anim.Assign("Move");
                    }
                }
            }

            else if (SplashKit.KeyDown(KeyCode.AKey))
            {
                //flips image vertically 
                _opt.FlipY = true;
               

                if (_position.X > 0)
                {
                    _position.X -= _speed;
                    if (SplashKit.AnimationName(_anim) != "move")
                    {
                        _anim.Assign("Move");
                    }
                }
            }

            else if (Hit && SplashKit.AnimationName(_anim) != "damage")
            {
                _anim.Assign("Damage");
            }

            //switches back to idle animation if player isn't moving and hasn't been hit
            else if (!Hit && SplashKit.AnimationName(_anim) != "idle")
            {
                _anim.Assign("Idle");
            }

           
        }

        //resets player position and animation back to default
        public void Reset()
        {
            
            _anim.Assign("Idle");
            _position.X = SplashKit.CurrentWindowWidth() / 2 - 16;
            _position.Y = SplashKit.CurrentWindowHeight() - 50;
            
        }
    }
}
