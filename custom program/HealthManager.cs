using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace custom_program
{
    //keeps track and manages player's health
    public class HealthManager : Manager
    {
        private int _health;
        private List<Bitmap> _lives;

        //constructor
        public HealthManager() 
        {
            _health = 3;
            _lives = new List<Bitmap>();
            Bitmap _image = SplashKit.LoadBitmap("lives", "heart.png");
            for (int i = 0; i< 3; i++)
            {
                _lives.Add(_image);
            }
        } 
        //methods 
        public void ModifyHealth(IModifyHealth item)
        {
            if (_health > 0)
            {
                if (item.Hp > 0 && _health < 3) //prevents player from having more than 3 lives 
                {
                    _health += item.Hp;
                }
                else if (item.Hp < 0)
                {
                    _health += item.Hp;
                }
            }

        }

        //used to check if player has no lives left 
        public bool IsDead()
        {
            return (_health == 0);
        }

        //draws number of lives player has left
        public override void Draw()
        {
            SplashKit.DrawText("Lives:", Color.Black, _font, 20, 10, 35);
            int i = 45;
            for(int j = 0; j < _health; j++)
            {
                SplashKit.DrawBitmap(_lives[j], i, 30);
                i += 20;
            }
        }

        public override void Reset()
        {
            _health = 3;
        }

    }
}
