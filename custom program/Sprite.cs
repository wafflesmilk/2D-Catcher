using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    public abstract class Sprite
    {
        
        protected Point2D _position;
        protected float _speed;
        protected Bitmap _image;
      
        //properties
        public Point2D Position
        {
            get
            {
                return _position;
            }
           
        }

        public Bitmap GetBitmap
        {
            get
            {
                return _image;
            }
        }


        //methods 
        public abstract void Draw();
        public abstract void Update();

       
    }
}
