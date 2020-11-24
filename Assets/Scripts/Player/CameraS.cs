using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class CameraS : MonoBehaviour
    {
        #region Variables
        private Player.Movement playerMove;
        private TrunkManager trunks;
        private float camSize;
        #endregion
        #region Awake
        /// <summary>
        /// Connect important objects and get camera size
        /// </summary>
        void Awake()
        {
            playerMove = FindObjectOfType<Player.Movement>();
            trunks = FindObjectOfType<TrunkManager>();
            camSize = gameObject.GetComponent<Camera>().orthographicSize;
        }
        #endregion
        #region Update
        void Update()
        {
            Player(); //follow player upwards

            Trunks(); //spawn trunks when needed
        }
        #endregion
        #region Functions
        /// <summary>
        /// Move camera upwards with player and kills player if it falls out the bottom of the view
        /// </summary>
        private void Player()
        {
            if (playerMove.transform.position.y > transform.position.y) //if player is higher than current camera position
            {
                transform.position = new Vector3(transform.position.x, playerMove.transform.position.y, transform.position.z); //move camera up to the player's position
            }
            if (playerMove.transform.position.y < transform.position.y - camSize&&Time.timeScale>0) //if player is below the bottom of the camera
            {
                playerMove.GameOver(); //player dies
            }
        }
        /// <summary>
        /// Check there is at least one trunk and add a trunk if needed
        /// </summary>
        private void Trunks()
        {
            if (trunks.LastTrunk == null) //if there are no trunks yet
            {
                trunks.Start(); //run the trunk setup
            }
            if (trunks.TreeTop < transform.position.y + camSize) //if the tree does not reach the top of the view
            {
                trunks.SpawnTrunk(); //spawn a new trunk
            }
        }
        #endregion
    }
}