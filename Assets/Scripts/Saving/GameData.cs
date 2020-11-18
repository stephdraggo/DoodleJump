using System.Collections;
using UnityEngine;

namespace DoodleJump.Saving
{
    [System.Serializable]
    public class GameData
    {
        #region Variables
        public static GameData current; //for the current game save:
        public Scores.scoreSet[] highScores; //the highscores
        #endregion
        #region Constructors
        public GameData()
        {
            highScores = null;
        }
        public GameData(Scores _scores)
        {
            highScores = _scores.HighScores;
            
        }
        #endregion
    }
}