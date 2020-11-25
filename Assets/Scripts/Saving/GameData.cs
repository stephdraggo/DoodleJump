using System.Collections;
using UnityEngine;

namespace DoodleJump.Saving
{
    [System.Serializable]
    public class GameData
    {
        #region Variables
        public static GameData current; //for the current game save:
        public ScoresNew.scoreSet[] highScores; //the local highscores
        #endregion
        #region Constructors
        public GameData()
        {
            highScores = null;
        }
        public GameData(ScoresNew _scores)
        {
            highScores = _scores.HighScores;
            
        }
        #endregion
    }
}