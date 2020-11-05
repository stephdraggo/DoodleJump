using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Player
{
    [AddComponentMenu("Doodle Jump/Player/Movement")]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        #region Variables
        private Animator animate;
        private SpriteRenderer render;
        private Collider2D collide;
        private Rigidbody2D body;


       
        private int currentHeightAchieved, currentHeight, highestHeight;


        [SerializeField] private Text currentHeightAchievedText,currentHeightText, highestHeightText;

        
        #endregion
        #region Start
        void Start()
        {
            //connect components
            animate = GetComponent<Animator>();
            render = GetComponent<SpriteRenderer>();
            collide = GetComponent<CircleCollider2D>();
            body = GetComponent<Rigidbody2D>();

            //get highest score
            highestHeight = Scores.instance.HighScores[0].score;
            highestHeightText.text = (Scores.instance.HighScores[0].name+ ": "+ highestHeight.ToString()+" metres");

        }
        #endregion
        #region Update
        void Update()
        {
            UpdateHeight(); //update height ui

            //if velocity upwards, disable collide

            //if velocity downwards too fast
            //GameManager.instance.GameOver(currentHeightAchieved);

            //flip sprite based on input
            if (Input.GetButton("Left"))
            {

            }
        }
        #endregion
        #region Functions
        #region update height done
        /// <summary>
        /// sets current height according to y position, checks and updates achieved height, updates ui
        /// </summary>
        private void UpdateHeight()
        {
            currentHeight = (int)gameObject.transform.position.y;
            currentHeightText.text = currentHeight.ToString();
            if (currentHeight > currentHeightAchieved)
            {
                currentHeightAchieved = currentHeight;
                currentHeightAchievedText.text = currentHeightAchieved.ToString();
            }
        }
        #endregion

        #endregion
    }
}