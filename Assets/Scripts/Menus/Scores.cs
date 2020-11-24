using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class Scores : MonoBehaviour
    {
        #region Variables
        public Saving.GameData game;
        private Saving.SaveGame saving;
        private scoreSet[] highScores;

        [SerializeField]
        private Text[] scoreNames, scoreValues;

        [System.Serializable]
        public struct scoreSet //one block of score
        {
            public int score; //score amount
            public string name; //name of player who achieved this score first
        }
        #endregion
        #region Properties
        public scoreSet[] HighScores { get => highScores; }
        #endregion
        #region Awake
        /// <summary>
        /// Connects important objects and sets up scores.
        /// </summary>
        void Awake()
        {
            saving = FindObjectOfType<Saving.SaveGame>();

            UpdateScores();
        }
        #endregion
        #region Start
        /// <summary>
        /// Update scores again just because.
        /// </summary>
        void Start()
        {
            UpdateScores();
        }
        #endregion
        #region Functions
        #region default high scores
        /// <summary>
        /// randomly generated high scores
        /// </summary>
        private void DefaultScores()
        {
            highScores = new scoreSet[10]; //new array of scoresets
            for (int i = 0; i < highScores.Length; i++)
            {
                scoreSet tempScore = new scoreSet(); //temp scoreset
                tempScore.score = (Random.Range(10, 100)); //give score
                tempScore.name = "Sam"; //give name (Sam the dragon)
                highScores[i] = tempScore; //add scoreset
            }
            saving.SaveButton();
        }
        #endregion

        #region order scores
        /// <summary>
        /// Reorders a scoreset to be highest to lowest. I'm very proud of this logic, I figured it out myself.
        /// </summary>
        /// <param name="_scores">scoreset array to be reordered</param>
        /// <returns>ordered list of scores</returns>
        public scoreSet[] OrderScores(scoreSet[] _scores)
        {
            scoreSet[] tempScores = new scoreSet[_scores.Length]; //create new scoreset array of the same length as the passed array
            for (int i = 0; i < _scores.Length; i++)
            {
                //find score with highest value
                //assign to position 0 of tempscores
                //set that score to 0
                //find next highest score
                //assign to position 1
                //set that score to 0
                //repeat for all scores
                int localHighScore = 0; //reset local high score to 0
                int localHighIndex = -1; //reset index to -1 so it does not refer to any position in the array
                for (int j = 0; j < _scores.Length; j++)
                {
                    if (_scores[j].score > localHighScore) //if checked score is higher than local high score
                    {
                        localHighScore = _scores[j].score; //update local high score
                        localHighIndex = j; //get index of this high score
                    }
                }
                tempScores[i] = _scores[localHighIndex]; //set this high score to the current high score position
                _scores[localHighIndex].score = 0; //reduce the checked score to 0 so it won't show up on the next time around
            }

            return tempScores; //return ordered array
        }
        #endregion

        #region display high scores
        /// <summary>
        /// Updates score display.
        /// </summary>
        private void DisplayHighScores()
        {
            for (int i = 0; i < HighScores.Length; i++) //for each high score
            {
                scoreNames[i].text = HighScores[i].name + ":"; //display name in name spot
                scoreValues[i].text = HighScores[i].score.ToString() + " m"; //and score in score spot
            }
        }
        #endregion

        #region update scores
        /// <summary>
        /// Check for scores (load or create if none found), order them correctly, update the display, and save the scores.
        /// </summary>
        public void UpdateScores()
        {
            if (highScores.Length < 1) //if there are no active scores
            {
                if (game.highScores.Length > 0) //if there are saved scores
                {
                    highScores = game.highScores; //get scores from save
                }
                else
                {
                    DefaultScores(); //generate random scores
                }
            }

            highScores = OrderScores(highScores); //reorder scores from highest to lowest
            DisplayHighScores(); //update the display
            game.highScores = highScores; //send these scores to the save file
            saving.SaveButton(); //save these scores
        }
        #endregion

        #region compare new score
        /// <summary>
        /// Considers the given score and puts it in array if it's high enough.
        /// </summary>
        /// <param name="_newScore">score amount to be tested</param>
        public void CompareNewScore(float _newScore)
        {
            scoreSet newScore = new scoreSet(); //new empty score set
            newScore.score = (int)_newScore; //set score as the achieved score
            newScore.name = "Player"; //set name as "Player", might become customisable in future

            for (int i = 0; i < highScores.Length; i++) //highest to lowest
            {
                if (_newScore > highScores[i].score) //if new score is higher
                {
                    highScores[9] = newScore; //replace last
                    break;
                }
            }

            UpdateScores(); //update scores
        }
        #endregion
        #endregion
    }
}