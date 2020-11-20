using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump
{
    public class Scores : MonoBehaviour
    {
        #region Variables
        public static Scores instance;
        public Saving.GameData game;
        private scoreSet[] highScores;

        [SerializeField]
        private Text[] scoreNames, scoreValues;

        [System.Serializable]
        public struct scoreSet
        {
            public int score;
            public string name;
        }
        #endregion
        #region Properties
        public scoreSet[] HighScores { get => highScores; }
        #endregion
        #region Awake
        void Awake()
        {
            #region set up instance
            if (instance == null) //if the instance doesn't exist
            {
                instance = this; //set this as instance
            }
            else if (instance != this) //if there is an instance but it isn't this object
            {
                Destroy(gameObject); //delete this
                return; //exit code early
            }
            DontDestroyOnLoad(gameObject); //always be able to access the original instance
            #endregion


            //load highScores from binary
            if (game.highScores != null)
            {

            }

            if (highScores == null) //if no high scores
            {
                DefaultScores(); //set up default scores
            }

            UpdateScores();

        }
        #endregion
        #region Start
        void Start()
        {

        }
        #endregion
        void Update()
        {
            UpdateScores();
        }
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
        }
        #endregion

        #region order scores
        /// <summary>
        /// Reorders a scoreset to be highest to lowest.
        /// </summary>
        /// <param name="_scores">scoreset array to be reordered</param>
        /// <returns></returns>
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
        private void DisplayHighScores()
        {
            for (int i = 0; i < HighScores.Length; i++)
            {
                scoreNames[i].text = HighScores[i].name + ":";
                scoreValues[i].text = HighScores[i].score.ToString() + " m";
            }
        }
        #endregion

        #region update scores
        public void UpdateScores()
        {
            highScores = OrderScores(highScores);
            DisplayHighScores();
        }
        #endregion
        
        #region compare new score
        public void CompareNewScore(float _newScore)
        {
            scoreSet newScore = new scoreSet();
            newScore.score = (int)_newScore;
            newScore.name = "Player";

            for (int i = 0; i < highScores.Length; i++) //highest to lowest
            {
                if (_newScore > highScores[i].score) //if new score is higher
                {
                    highScores[9] = newScore; //replace last
                    break;
                }
            }

            UpdateScores();
        }
        #endregion
        #endregion
    }
}