using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Menus
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField,Tooltip("HUD, Menu, Settings, Scores")]
        private GameObject[] panels;
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
        public void ChangePanel(int _index)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
            panels[_index].SetActive(true);
            Debug.Log(panels[_index].name);
        }
        #endregion
    }
}