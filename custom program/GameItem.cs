using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace custom_program
{
    public abstract class GameItem : Sprite
    {
        protected SpriteKind _kind;
        protected DrawingOptions _opt;
       
        //property
       
        public float Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        //constructor
        public GameItem(float speed) 
        {
            _speed = speed;
            _position.Y = -50;
            _position.X = SplashKit.Rnd(320);

            _opt.ScaleX = 1;
            _opt.ScaleY = 1;
            _opt.Dest = SplashKit.CurrentWindow();
        }

        //methods
        public override void Draw()
        {
            SplashKit.DrawBitmap(_image, _position.X, _position.Y,_opt);
        } 

        public override void Update()
        {
            if (_position.Y < 420)
            {
                _position.Y += _speed;
            }
          
        }

    }
}
