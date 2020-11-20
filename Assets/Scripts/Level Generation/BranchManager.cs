using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class BranchManager : MonoBehaviour
    {
        #region Variables
        public static BranchManager instance = null;

        [SerializeField, Tooltip("Array of left branch prefabs to spawn from.")]
        private GameObject[] branchLeftPrefabs;
        [SerializeField, Tooltip("Array of right branch prefabs to spawn from.")]
        private GameObject[] branchRightPrefabs;

        [SerializeField, Tooltip("List of branch objects in game.")]
        private List<GameObject> branches;
        #endregion
        #region Properties

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
        public void SpawnBranch(GameObject _trunk, bool left, float _offset,int _index)
        {
            GameObject branch = null; //create reference to prefab
            float x = 0; //create x coordinate
            if (left) //if left
            {
                branch = branchLeftPrefabs[_index]; //get random left branch prefab
                x -= TrunkManager.instance.TrunkWidth / 3; //move x coordinate to the left
            }
            else //if right
            {
                branch = branchRightPrefabs[_index]; //get random right branch prefab
                x += TrunkManager.instance.TrunkWidth / 3; //move x coordinate to the right
            }
            x += _trunk.transform.position.x + branch.transform.position.x; //add trunk position and modified branch prefab position to x coordinate
            float y = _trunk.transform.position.y + _offset; //create y coordinate and add offset
            Vector3 position = new Vector3(x, y); //create position from coordinates
            GameObject newBranch = Instantiate(branch, position, branch.transform.rotation, _trunk.transform); //create branch from selected prefab at position, facing the selected direction and as a child of its trunk



            branches.Add(newBranch); //add branch to list of branches in scene
        }
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