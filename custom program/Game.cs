using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SplashKitSDK;
using System.Threading.Tasks;
using System.Numerics;
using Timer = SplashKitSDK.Timer;

namespace custom_program
{
    public class Game
    {
        private Window _w;
        private Bitmap _background;
        private SpriteManager _spriteManager;
        private ScoreManager _scoreManager;
        private HealthManager _healthManager;
        private Font _font;
        private SoundEffect _collect, _hit,_dead;
        private Music _bgmusic;
        private bool _gameover;
        private Timer _timer;
        
        //constructor
        public Game() 
        {
            //set path to fetch audio, image, animation resources
            SplashKit.SetResourcesPath("C:\\Users\\mh\\OneDrive\\Desktop\\OOP\\custom program\\Resources");

            //setting up window
            _w = new Window("Endless Eater", 360, 450);
            _background = SplashKit.LoadBitmap("background", "background.png");
            _font=SplashKit.LoadFont("main-font", "m5x7.ttf");

            //timer is used to check how long to display instructions screen for 
            _timer = new Timer("timer");

            //initializing game managers
            _scoreManager = new ScoreManager();
            _healthManager = new HealthManager();
            _spriteManager = new SpriteManager();


            //initialising sound effects and bg music 
            _collect = SplashKit.LoadSoundEffect("collect", "collect.ogg");
            _hit = SplashKit.LoadSoundEffect("damage", "hit-damage.ogg");
            _dead = SplashKit.LoadSoundEffect("dead", "gameover.wav");
            _bgmusic = SplashKit.LoadMusic("music", "yoshi.mp3");


            //set to true when player has 0 lives left
            _gameover = false;

        }

        //methods 
        //main method
        public void Run()
        {
          
            _timer.Start();

            //setting up animation for keys 
            Bitmap akey = SplashKit.LoadBitmap("a-key", "A-Key.png");
            akey.SetCellDetails(32, 32, 2, 1, 2);

            Bitmap dkey = SplashKit.LoadBitmap("d-key", "D-Key.png");
            dkey.SetCellDetails(32,32,2,1,2);

            AnimationScript script = SplashKit.LoadAnimationScript("script", "animation-script.txt");
            Animation key_anim = script.CreateAnimation("Key");
            DrawingOptions opt = SplashKit.OptionWithAnimation(key_anim);

            //starts playing bg music and setting volume
            SplashKit.PlayMusic(_bgmusic);
            SplashKit.SetMusicVolume(0.2f);

            //main game loop 
            while (!_w.CloseRequested)
            {
                SplashKit.ProcessEvents(); //processes any user interaction with game window
               
                SplashKit.DrawBitmap(_background, 0, 0, SplashKit.OptionScaleBmp(1, 1.8));
                Draw();
                //instruction screen displays for 7 seconds before game begins 
                if (_timer.Ticks/1000 < 7)
                {
                    key_anim.Update();
                    SplashKit.FillRectangleOnWindow(_w, SplashKit.RGBAColor(0, 0, 0, 0.35), SplashKit.RectangleFrom(0, 0, 360, 450));
                    SplashKit.DrawText("Use", Color.White, _font, 25,80,120);
                    SplashKit.DrawBitmap(akey, 110, 110, opt);
                    SplashKit.DrawText("and", Color.White, _font, 25, 147, 120);
                    SplashKit.DrawBitmap(dkey, 178, 110, opt);
                    SplashKit.DrawText("to move.", Color.White, _font, 25, 215, 120);
                    SplashKit.DrawText("Eat all the food and avoid insects!", Color.White, _font, 25, 40, 150);
                }

                else
                {
                    if (!_gameover)
                    {
                        Update();
                    }

                    else
                    {
                        SplashKit.FillRectangleOnWindow(_w, SplashKit.RGBAColor(0, 0, 0, 0.35), SplashKit.RectangleFrom(0, 0, 360, 450));
                        SplashKit.DrawText("Game Over!", Color.White, _font, 25, 140, 140);
                        SplashKit.DrawText("Press 'R' to retry.", Color.White, _font, 25, 120, 160);

                        //game restarts if R is pressed after player loses
                        if (SplashKit.KeyDown(KeyCode.RKey))
                        {
                            Reset();
                        }

                    }

                }

                
                //loops bg music 
                if (!SplashKit.MusicPlaying() && !_healthManager.IsDead())
                {
                    SplashKit.PlayMusic(_bgmusic);
                }

                //only saves to textfile if current score is equal to highscore 
                if (_scoreManager.CompareScores())
                {
                    _scoreManager.SaveHighScore();
                }

                SplashKit.RefreshScreen(60);
              
            }   
            
        }

        //main drawing method
        //tells all game components to draw itself
        private void Draw()
        {
                _spriteManager.Draw();
                _scoreManager.Draw();
                _healthManager.Draw();
        }

        //main update method 
        //tells all game components to update itself
        private void Update()
        {
            _spriteManager.Update();
            _scoreManager.Update();

            _spriteManager.Player.Score = _scoreManager.CurrentScore;
           

            GameItem item = _spriteManager.Collision();
            if (item is IModifyScore)
            {
                _scoreManager.ModifyScore(item as IModifyScore);
                SplashKit.PlaySoundEffect(_collect);
            }

            if (item is IModifyHealth)
            {
                _healthManager.ModifyHealth(item as IModifyHealth);
                if (item is Insect)
                {
                    SplashKit.PlaySoundEffect(_hit);
                }
               
            }

            if (_healthManager.IsDead())
            {
                //lets current animation finish before gameover screen is shown
                while (_spriteManager.Player.Hit)
                {
                    _spriteManager.Update();
                }
                SplashKit.StopMusic();
                SplashKit.PlaySoundEffect(_dead);
                _gameover = true;
            }

          
        }

        private void Reset()
        {
            _healthManager.Reset();
            _scoreManager.Reset();
            _spriteManager.Reset();

            _gameover = false;

            _timer.Reset();
            _timer.Start();

        }

    }
}
