using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    public abstract class Manager
    {
        protected Font _font;
        public Manager() 
        {
            _font = SplashKit.LoadFont("main-font", "m5x7.ttf");
        }
        public abstract void Draw();

        public abstract void Reset();
       
        
    }
}
