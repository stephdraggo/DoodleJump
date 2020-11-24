using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class BranchManager : MonoBehaviour
    {
        #region Variables
        [SerializeField, Tooltip("Array of left branch prefabs to spawn from.")]
        private GameObject[] branchLeftPrefabs;
        [SerializeField, Tooltip("Array of right branch prefabs to spawn from.")]
        private GameObject[] branchRightPrefabs;

        [SerializeField, Tooltip("List of branch objects in game.")]
        private List<GameObject> branches;

        private TrunkManager trunkManager;
        #endregion
        #region Awake
        /// <summary>
        /// Connects important objects.
        /// </summary>
        void Awake()
        {
            trunkManager = FindObjectOfType<TrunkManager>();
        }
        #endregion
        #region Functions
        /// <summary>
        /// Function for spawning one branch.
        /// </summary>
        /// <param name="_trunk">the trunk parent for this branch</param>
        /// <param name="left">if this branch is for the left side of the trunk or not(right side)</param>
        /// <param name="_offset">how far up the trunk this branch goes</param>
        /// <param name="_index">which branch prefab to use</param>
        public void SpawnBranch(GameObject _trunk, bool left, float _offset,int _index)
        {
            GameObject branch = null; //create reference to prefab
            float x = 0; //create x coordinate
            if (left) //if left
            {
                branch = branchLeftPrefabs[_index]; //get random left branch prefab
                x -= trunkManager.TrunkWidth / 3; //move x coordinate to the left
            }
            else //if right
            {
                branch = branchRightPrefabs[_index]; //get random right branch prefab
                x += trunkManager.TrunkWidth / 3; //move x coordinate to the right
            }
            x += _trunk.transform.position.x + branch.transform.position.x; //add trunk position and modified branch prefab position to x coordinate
            float y = _trunk.transform.position.y + _offset; //create y coordinate and add offset
            Vector3 position = new Vector3(x, y); //create position from coordinates
            GameObject newBranch = Instantiate(branch, position, branch.transform.rotation, _trunk.transform); //create branch from selected prefab at position, facing the selected direction and as a child of its trunk

            branches.Add(newBranch); //add branch to list of branches in scene
        }
        /// <summary>
        /// Delete all branches from the scene and remove from list of branches.
        /// </summary>
        public void ClearBranches()
        {
            int number = branches.Count;
            for (int i = 0; i < number; i++)
            {
                Destroy(branches[0]);
                branches.RemoveAt(0);
            }
        }
        #endregion
    }
}