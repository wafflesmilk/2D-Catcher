using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SplashKitSDK;
using Timer = SplashKitSDK.Timer;

namespace custom_program
{
    public class SpriteManager : Manager
    {
        private List<GameItem> _sprites, _toRemove;
        private Player _player;
        private SpriteFactory _spriteFactory;
        private int _currentFrame, _elapsedFrame, _spawnTime, _targetScore;

        public Player Player
        {
            get 
            {
                return _player; 
            }
        }

      
        //constructor
        public SpriteManager()
        {
            _player = new Player();
            _spriteFactory = new SpriteFactory();
            _sprites = new List<GameItem>();
            _toRemove = new List<GameItem>();
            _currentFrame = 0;
            _elapsedFrame = 0;
            _spawnTime = 60;
            _targetScore = 50;


        }

        //methods
        //draws player and all GameItems in the sprite list
        public override void Draw()
        {
            _player.Draw();

            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw();
            }

        }

        //collision detection between player and falling sprites
        public GameItem Collision() 
        {

            foreach(GameItem sprite in _sprites)
            {
                if (SplashKit.BitmapCollision(_player.GetBitmap, _player.Position, sprite.GetBitmap, sprite.Position))
                {
                    _toRemove.Add(sprite);
                    if (sprite is Insect)
                    {
                        _player.Hit = true; //when Hit is set to true, Damage animation is played 
                    }
                    return sprite;
                }
            }

            return null;
            
        }

        public  void Update()
        {
           
            _player.Update();


            if (_elapsedFrame%_spawnTime == 0)
            {
                _sprites.Add(_spriteFactory.CreateSprite());
                
            }
            _elapsedFrame++;

            //checking for collision
            Collision();

            //updates position of each sprite 
            foreach (GameItem sprite in _sprites)
            {
                sprite.Update();
                if (sprite.Position.Y >= 420)
                {
                    _toRemove.Add(sprite);
                }
            }

            //another list is used to remove sprites that have left the screen from _sprites
            foreach (GameItem sprite in _toRemove)
            {
                _sprites.Remove(sprite);
            }
            
            //increases speed at which items are dropped by 0.9f every 50 points earned
            if (Player.Score > _targetScore)
            {
                _spriteFactory.IncreaseSpeed = 0.9f;
                if (_spawnTime > 30)
                {
                    _spawnTime -= 5; //decreases amount of time between spawns 
                }
                _targetScore += 50;
            }


            //hit is only reset to false after 20 frames
            //ensures damage animation finishes playing 
            //damage animation is 4 frames, each frame is played 5 times
            
            if (_elapsedFrame - _currentFrame == 20)
            {
                _player.Hit = false;
                _currentFrame = _elapsedFrame;
            }


        }

        
       //resets sprites to default mode
       public override void Reset()
        {
            _player.Reset(); //player position is reset

            _currentFrame = 0;
            _elapsedFrame = 0;
            _spawnTime = 60;
            _targetScore = 50;

            //sprite list is cleared 
            foreach (GameItem sprite in _sprites)
            {
                _toRemove.Add(sprite);

            }

           foreach(GameItem sprite in _toRemove)
            {
                _sprites.Remove(sprite);
            }
        }

    }
}
