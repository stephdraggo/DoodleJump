using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance = null;

        
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
        }
        #endregion
        #region Start
        void Start()
        {
           

            
        }
        #endregion
        #region Update
        void Update()
        {

        }
        #endregion
        #region Functions
        public void GameOver(float _heightAchieved)
        {

        }

        
        #endregion
    }
}