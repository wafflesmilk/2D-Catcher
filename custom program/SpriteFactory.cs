using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using Timer = SplashKitSDK.Timer;

namespace custom_program
{
    /// <summary>
    /// used to spawn a random GameItem
    /// </summary>
    public class SpriteFactory
    {
        private GameItem _sprite;
        private float _speedIncrease;

        //property 
        //used to increase speed of GameItems to be generated
        public float IncreaseSpeed
        {
            set
            {
                _speedIncrease = value;
            }
        }
       
        //constructor
        public SpriteFactory()
        {
           
            _speedIncrease = 0;
           
        }

        //methods
        public GameItem CreateSprite()
        {
            int num = SplashKit.Rnd(-1, 13);

            if (num >= 0)
            {
                switch (num % 2 == 0)
                {
                    case true: //food item is spawned if number generated is even
                        _sprite = new Food(3 + _speedIncrease);
                        break;
                            
                    case false: //insect is spawned if number generated is odd
                        _sprite = new Insect(4 + _speedIncrease);
                        break;
                }
            }
            //there is a 1 in 15 chance of a potion spawning
            else
            {
                _sprite = (new Misc(3.5f + _speedIncrease));
            }


            return _sprite;
        }

    }
}
