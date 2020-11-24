using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class TrunkManager : MonoBehaviour
    {
        #region Variables
        [SerializeField, Tooltip("Array of trunk prefabs to spawn from.")]
        private GameObject[] trunkPrefabs;

        [SerializeField, Tooltip("Get the dimensions of the trunk from the disabled collider.")]
        private float trunkHeight, trunkWidth;

        [SerializeField, Tooltip("List of trunk objects in game.")]
        private List<GameObject> trunks;

        [SerializeField, Tooltip("Last trunk spawned.")]
        private GameObject lastTrunk;

        private BranchManager branchManager;
        #endregion
        #region Properties
        public float TrunkWidth { get => trunkWidth; }
        public float TrunkHeight { get => trunkHeight; }
        public float TreeTop { get => lastTrunk.transform.position.y + trunkHeight / 2; }
        public GameObject LastTrunk { get => lastTrunk; }
        #endregion
        #region Awake
        /// <summary>
        /// Connects important objects and gets static values for spawning.
        /// </summary>
        private void Awake()
        {
            branchManager = FindObjectOfType<BranchManager>();

            trunkHeight = trunkPrefabs[0].GetComponent<BoxCollider2D>().size.y;
            trunkWidth = trunkPrefabs[0].GetComponent<BoxCollider2D>().size.x;
        }
        #endregion
        #region Start
        /// <summary>
        /// Spawn base trunk with branches.
        /// </summary>
        public void Start()
        {
            if (trunks.Count<1) //check if no trunks are in scene
            {
                GameObject newTrunk = Instantiate(trunkPrefabs[0], gameObject.transform); //spawn base trunk
                newTrunk.name = "Base Trunk"; //name it
                trunks.Add(newTrunk); //add to list of trunks
                lastTrunk = newTrunk; //set as last trunk for future spawning

                SpawnBranches(); //spawn some branches for the base trunk
            }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Spawn new trunk directly above last trunk and set its position in the hierarchy.
        /// Add to trunk list and assign this trunk as last trunk.
        /// </summary>
        public void SpawnTrunk()
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, lastTrunk.transform.position.y + trunkHeight); //location of trunk calculated from center of screen and height of last spawned trunk
            int prefab = Random.Range(0, trunkPrefabs.Length); //random trunk prefab
            GameObject newTrunk = Instantiate(trunkPrefabs[prefab], position, Quaternion.identity, gameObject.transform); //spawn random trunk at position with no rotation as a child of the trunk manager object
            newTrunk.name = "Trunk"; //name new trunk
            trunks.Add(newTrunk); //add trunk to trunk list
            lastTrunk = newTrunk; //set as last trunk spawned

            SpawnBranches(); //spawn branches for this trunk
        }
        /// <summary>
        /// random number of branches each side
        /// branches are offset from each other on the y axis based on how many there are
        /// branch is randomly selected from prefabs
        /// </summary>
        private void SpawnBranches()
        {
            //left side
            int numberOfBranches = Random.Range(1, 5); //random number of branches for this side of the tree
            for (int i = 0; i < numberOfBranches; i++) //for each branch
            {
                float offset = -trunkHeight; //y coordinate offset is bottom of this trunk
                offset += i * (trunkHeight / numberOfBranches); //plus the index of this branch multiplied by the interval between branches for this trunk
                int prefab = Random.Range(0, 3); //random prefab for this branch
                branchManager.SpawnBranch(lastTrunk, true, offset, prefab); //spawn branch on left according to these instructions
            }
            //right side
            numberOfBranches = Random.Range(2, 5);
            for (int i = 0; i < numberOfBranches; i++)
            {
                float offset = -trunkHeight;
                offset += i * (trunkHeight / numberOfBranches);
                int prefab = Random.Range(0, 3);
                branchManager.SpawnBranch(lastTrunk, false, offset, prefab); //spawn branch on right according to these instructions
            }
        }
        /// <summary>
        /// Deletes all trunks from scene and removes them from list of trunks.
        /// </summary>
        public void ClearTree()
        {
            branchManager.ClearBranches();
            int number = trunks.Count;
            for (int i = 0; i < number; i++)
            {
                Destroy(trunks[0]);
                trunks.RemoveAt(0);
            }
        }
        #endregion
    }
}