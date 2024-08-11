using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    public class Insect : GameItem, IModifyHealth
    {
        private int _hp;

        //property
        public int Hp
        {
            get
            {
                return _hp;
            }
        }

        //constructor
        public Insect(float speed) : base(speed)
        {
            _hp = -1;
            _kind = (SpriteKind)SplashKit.Rnd(4,8);
            _image = SplashKit.LoadBitmap("insects", "insects.png");
            _image.SetCellDetails(32, 32, 15, 2, 30);

            switch (_kind)
            {
                case SpriteKind.Worms:
                    _opt.DrawCell = 0;
                    break;
                case SpriteKind.Fly:
                    _opt.DrawCell = 1;
                    break;
                case SpriteKind.Ladybug:
                    _opt.DrawCell = 2;
                    break;
                case SpriteKind.Caterpillar:
                    _opt.DrawCell = 15;
                    break;
            }
        }
    }
}
