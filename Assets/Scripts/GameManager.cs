using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoodleJump
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private Saving.SaveGame saving;
        private Menus.MainMenu menus;
        private ScoresNew scores;
        private Generation.TrunkManager trunkManager;
        #endregion
        #region Awake
        /// <summary>
        /// Connect all the important objects.
        /// </summary>
        void Awake()
        {
            saving = FindObjectOfType<Saving.SaveGame>();
            menus = FindObjectOfType<Menus.MainMenu>();
            scores = FindObjectOfType<ScoresNew>();
            trunkManager = FindObjectOfType<Generation.TrunkManager>();
        }
        #endregion
        #region Start
        void Start()
        {
            Pause(); //freeze time and open menu

            //this does not currently work
            saving.LoadButton(); //load saved high scores if there are any, in theory
        }
        #endregion
        #region Update
        void Update()
        {
            //pause game with esc
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause();
            }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Stops time, shows score panel, updates scores and incorporates new score.
        /// </summary>
        /// <param name="_heightAchieved">Score achieved this game.</param>
        public void GameOver(float _heightAchieved)
        {
            Time.timeScale = 0;
            menus.ChangePanel(3); //High Scores
            scores.CompareNewScore(_heightAchieved);
        }
        /// <summary>
        /// For after game over, clears the tree and reloads scene for new game.
        /// </summary>
        public void BackToMenu()
        {
            trunkManager.ClearTree();
            SceneManager.LoadScene(0); //use this instead of panels so you can replay after dying
        }
        /// <summary>
        /// Unfreezes time and shows HUD panel.
        /// </summary>
        public void Play()
        {
            Time.timeScale = 1;
            menus.ChangePanel(0); //HUD
        }
        /// <summary>
        /// Freezes time and shows menu panel.
        /// </summary>
        public void Pause()
        {
            Time.timeScale = 0;
            menus.ChangePanel(1); //Menu
        }
        /// <summary>
        /// Saves high scores and exits application.
        /// </summary>
        public void Quit()
        {
            saving.SaveButton(scores.game); //save high scores, in theory

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
        #endregion
    }
}