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
        #region Start
        void Start()
        {
            playerMove = FindObjectOfType<Player.Movement>();
            trunks = FindObjectOfType<TrunkManager>();
            camSize = gameObject.GetComponent<Camera>().orthographicSize;
        }
        #endregion
        #region Update
        void Update()
        {
            Player();

            Trunks();
        }
        #endregion
        #region Functions
        private void Player()
        {
            if (playerMove.transform.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, playerMove.transform.position.y, transform.position.z);
            }
            if (playerMove.transform.position.y < transform.position.y - camSize)
            {
                playerMove.GameOver();
            }
        }
        private void Trunks()
        {
            if (trunks.TreeTop < transform.position.y + camSize)
            {
                trunks.SpawnTrunk();
            }
        }
        #endregion
    }
}