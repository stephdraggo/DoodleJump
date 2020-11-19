using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Generation
{
    public class TrunkManager : MonoBehaviour
    {
        #region Variables
        public static TrunkManager instance = null;

        [SerializeField, Tooltip("Array of trunk prefabs to spawn from.")]
        private GameObject[] trunkPrefabs;

        [SerializeField, Tooltip("Get the dimensions of the trunk from the disabled collider.")]
        private float trunkHeight, trunkWidth;

        [SerializeField, Tooltip("List of trunk objects in game.")]
        private List<GameObject> trunks;

        [SerializeField, Tooltip("Last trunk spawned.")]
        private GameObject lastTrunk;
        #endregion
        #region Properties
        public float TrunkWidth { get => trunkWidth; }
        public float TrunkHeight { get => trunkHeight; }
        public float TreeTop { get => lastTrunk.transform.position.y + trunkHeight / 2; }
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
            trunkHeight = trunkPrefabs[0].GetComponent<BoxCollider2D>().size.y;
            trunkWidth = trunkPrefabs[0].GetComponent<BoxCollider2D>().size.x;

            #region spawn base trunk & branches
            GameObject newTrunk = Instantiate(trunkPrefabs[0], gameObject.transform);
            newTrunk.name = "Base Trunk";
            trunks.Add(newTrunk);
            lastTrunk = newTrunk;

            //spawn base trunk's branches here
            for (int i = 0; i < 3; i++)
            {
                float offset = -trunkHeight;
                offset += i * (trunkHeight / 3);
                BranchManager.instance.SpawnBranch(lastTrunk, true, offset, i);
                BranchManager.instance.SpawnBranch(lastTrunk, false, offset, i);
            }

            #endregion

        }
        #endregion
        #region Update
        void Update()
        {

        }
        #endregion
        #region Functions
        /// <summary>
        /// Spawn new trunk directly above last trunk and set its position in the hierarchy.
        /// Add to trunk list and assign this trunk as last trunk.
        /// </summary>
        public void SpawnTrunk()
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, lastTrunk.transform.position.y + trunkHeight);
            GameObject newTrunk = Instantiate(trunkPrefabs[Random.Range(0, trunkPrefabs.Length)], position, Quaternion.identity, gameObject.transform);
            newTrunk.name = "Trunk";
            trunks.Add(newTrunk);
            lastTrunk = newTrunk;

            //spawn this trunk's branches here
            for (int i = 0; i < 3; i++)
            {
                float offset = -trunkHeight;
                offset += i * (trunkHeight / 3);
                BranchManager.instance.SpawnBranch(lastTrunk, true, offset,i);
                BranchManager.instance.SpawnBranch(lastTrunk, false, offset,i);
            }

        }
        #endregion

        #region Testing
#if UNITY_EDITOR
        private void OnGUI()
        {
            if (GUI.Button(new Rect(50f, 50f, 50f, 50f), "add trunk"))
            {
                SpawnTrunk();
            }
            if (GUI.Button(new Rect(100f, 50f, 50f, 50f), "add trunk"))
            {
                //BranchManager.instance.SpawnBranch(lastTrunk,true);
            }
        }
#endif
        #endregion
    }
}