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
        private GameObject branchLeftPrefab;
        [SerializeField, Tooltip("Array of right branch prefabs to spawn from.")]
        private GameObject branchRightPrefab;

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
        public void SpawnBranchLeft(GameObject _trunk)
        {
            float offset = Random.Range(-1f, 1f);
            Vector3 position = new Vector3(_trunk.transform.position.x-TrunkManager.instance.TrunkWidth,gameObject.transform.position.y+offset);
            GameObject newBranch = Instantiate(branchLeftPrefab, position, Quaternion.identity, gameObject.transform);
        }
        #endregion
    }
}