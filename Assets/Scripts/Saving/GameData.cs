using System.Collections;
using UnityEngine;

namespace DoodleJump.Saving
{
    [System.Serializable]
    public class GameData
    {
        #region Variables
        public static GameData current; //for the current game save:
        public Scores.scoreSet[] highScores; //the local highscores
        #endregion
        #region Functions
        public GameData(Scores _scores)
        {
            if (SaveLoad.Load()) //if can load a save
            {
                //this should get the last GameData saved I think
                //savedGames is a list of GameData
                highScores = SaveLoad.savedGames[SaveLoad.savedGames.Count-1].highScores;
            }
            else if (_scores.HighScores != null) //if no save but scores has made default scores
            {
                highScores = _scores.HighScores;
            }
            else //if no scores available
            {
                highScores = null; //highscores is null
            }
        }

        public void Save(Scores _scores)
        {
            highScores = _scores.HighScores;
            SaveLoad.Save(this);
        }
        #endregion
    }
}