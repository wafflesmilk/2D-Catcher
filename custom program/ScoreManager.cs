using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace custom_program
{
    public class ScoreManager : Manager
    {
        private int _currentscore, _highscore;

        //read-only property 
        public int CurrentScore
        {
            get
            {
                return _currentscore;
            }
        }

        //constructor 
        public ScoreManager() 
        {
            _currentscore = 0;
            LoadHighScore(); 

        }

        //used to update highscore 
        public bool CompareScores()
        {
            if (_currentscore > _highscore)
            {
                _highscore = _currentscore;
            }

            return _currentscore >= _highscore;
        }

        //methods
        
        //highscore is loaded from a textfile
        public void LoadHighScore()
        {
            StreamReader sr = new StreamReader(@"C:\\Users\\mh\\OneDrive\\Desktop\\OOP\\custom program\\score.txt");
            _highscore = Int32.Parse(sr.ReadLine());
            sr.Close();
        }

        //saves highscore to textfile
        public void SaveHighScore()
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"C:\\Users\\mh\\OneDrive\\Desktop\\OOP\\custom program\\score.txt");
                writer.WriteLine(_currentscore);
                writer.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error encountered while writing to file.", e);
            }
        }

        //increments player's score based on value of item
        public void ModifyScore(IModifyScore item)
        {
            _currentscore += item.Points;
        }

        public override void Draw()
        {
            SplashKit.DrawText(("Score:" + _currentscore.ToString()), Color.Black, _font, 20, 10, 5);
            SplashKit.DrawText(("High Score:" + _highscore.ToString()), Color.Black, _font, 20, 10, 20);
        }

        public  void Update()
        {
            CompareScores();
        }


        public override void Reset()
        {
            _currentscore = 0;
            LoadHighScore();
        }

    }
}
