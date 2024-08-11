using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    public class Food : GameItem,IModifyScore
    {
        private int _points;

        //property
        public int Points
        {
            get
            {
                return _points;
            }
        }

        //constructor
        public Food(float speed): base(speed) 
        {
            _points = 10;
            _image = SplashKit.LoadBitmap("food", "food.png");
            _image.SetCellDetails(32, 32, 5, 4, 18);
            _kind = (SpriteKind)SplashKit.Rnd(0,3);

            switch (_kind)
            {
                case SpriteKind.Cake:
                    _opt.DrawCell = 0;
                    break;
                case SpriteKind.Donut:
                    _opt.DrawCell = 3;
                    break;
                case SpriteKind.Pizza:
                    _opt.DrawCell = 10;
                    break;
                case SpriteKind.Pie:
                    _opt.DrawCell = 12;
                    break;
            }

        }
    }
}
