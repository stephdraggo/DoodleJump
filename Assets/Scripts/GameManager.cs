using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance = null;
        private Saving.SaveGame saving;
        private Menus.MainMenu menus;
        
        #endregion
        #region Awake - set up instance
        void Awake()
        {
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

            saving = FindObjectOfType<Saving.SaveGame>();
            menus = FindObjectOfType<Menus.MainMenu>();
        }
        #endregion
        #region Start
        void Start()
        {
            Pause();

            saving.LoadButton();
        }
        #endregion
        #region Update
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause();
            }
        }
        #endregion
        #region Functions
        public void GameOver(float _heightAchieved)
        {
            Time.timeScale = 0;
            menus.ChangePanel(3); //High Scores
        }
        public void BackToMenu()
        {
            //save options here
            menus.ChangePanel(1); //Menu
            Debug.Log("main menu");
        }
        public void Play()
        {
            Time.timeScale = 1;
            menus.ChangePanel(0); //HUD
        }
        public void Pause()
        {
            Time.timeScale = 0;
            menus.ChangePanel(1); //Menu
        }
        public void Quit()
        {
            saving.SaveButton();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        #endregion
    }
}