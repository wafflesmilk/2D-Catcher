using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    //miscellaneous items increase player's score and health
    public class Misc : GameItem, IModifyHealth, IModifyScore
    {
        private int _hp, _points;
  
        //properties
        public int Hp
        {
            get
            {
                return _hp;
            }
        }
        public int Points
        {
            get
            {
                return _points;
            }
        }
        public Misc(float speed) : base(speed)
        {
            _hp = 1;
            _points = 5;
            _image = SplashKit.LoadBitmap("potion", "potion.png");
        }


    }
}
