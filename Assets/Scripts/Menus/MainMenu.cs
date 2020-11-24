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
        #region Functions
        /// <summary>
        /// Function for hiding all panels except the desired panel.
        /// </summary>
        /// <param name="_index">index of panel to show (0:HUD, 1:Menu, 2:Settings, 3:Scores)</param>
        public void ChangePanel(int _index)
        {
            for (int i = 0; i < panels.Length; i++) //for every panel
            {
                panels[i].SetActive(false); //deactivate
            }
            panels[_index].SetActive(true); //activate selected panel
        }
        #endregion
    }
}