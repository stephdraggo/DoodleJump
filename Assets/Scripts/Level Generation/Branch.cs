using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class Branch : MonoBehaviour
    {
        //remember bounciness
        #region Variables
        [SerializeField, Tooltip("The trunk segment this branch belongs to.")]
        private GameObject trunk;
        #endregion
        #region Properties

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
        public void AssignToBranch(GameObject _trunk)
        {
            trunk = _trunk;
        }
        #endregion
    }
}