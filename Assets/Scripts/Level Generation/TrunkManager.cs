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

        [SerializeField, Tooltip("Get the height of the trunk from the disabled collider.")]
        private float trunkHeight;

        [SerializeField, Tooltip("List of trunk objects in game.")]
        private List<GameObject> trunks;

        [SerializeField, Tooltip("Last trunk spawned.")]
        private GameObject lastTrunk;
        #endregion
        void Start()
        {
            trunkHeight = trunkPrefabs[0].GetComponent<BoxCollider2D>().size.y;

            GameObject newTrunk = Instantiate(trunkPrefabs[0], gameObject.transform);
            newTrunk.name = "Base Trunk";
            trunks.Add(newTrunk);
            lastTrunk = newTrunk;
        }

        void Update()
        {

        }

        public void SpawnTrunk()
        {
            Vector3 position = new Vector3(gameObject.transform.position.x, lastTrunk.transform.position.y + trunkHeight);
            GameObject newTrunk = Instantiate(trunkPrefabs[Random.Range(0, trunkPrefabs.Length)], position, Quaternion.identity, gameObject.transform);
            newTrunk.name = "Trunk";
            trunks.Add(newTrunk);
            lastTrunk = newTrunk;
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (GUI.Button(new Rect(50f, 50f, 50f, 50f), "add trunk"))
            {
                SpawnTrunk();
            }
        }
#endif
    }
}