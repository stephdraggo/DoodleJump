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


        private float direction;
        private int currentHeightAchieved, currentHeight, highestHeight;
        [SerializeField]private bool grounded;


        [SerializeField] private Text currentHeightAchievedText, currentHeightText, highestHeightText;
        [SerializeField] private float jumpSpeed,heightMultiplyer;


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
            highestHeightText.text = ("Beat "+Scores.instance.HighScores[0].name + "'s " + highestHeight.ToString() + " metres");

        }
        #endregion
        #region Update
        void Update()
        {
            UpdateHeight(); //update height ui

            UpdateMove(); //move


        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            grounded = true;
        }
        #endregion
        #region Functions
        #region update height done
        /// <summary>
        /// sets current height according to y position, checks and updates achieved height, updates ui
        /// </summary>
        private void UpdateHeight()
        {
            currentHeight = (int)(gameObject.transform.position.y*heightMultiplyer);
            currentHeightText.text = "Current height: "+currentHeight.ToString();
            if (currentHeight > currentHeightAchieved)
            {
                currentHeightAchieved = currentHeight;
                currentHeightAchievedText.text = "Highest this round: "+currentHeightAchieved.ToString();
            }
        }
        #endregion
        #region update move
        /// <summary>
        /// everything in here works fine and doesn't need changing
        /// </summary>
        private void UpdateMove()
        {
            //flip sprite based on input
            if (direction > 0)
            {
                render.flipX = false;
            }
            else if (direction < 0)
            {
                render.flipX = true;
            }

            //jump
            if (grounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    body.AddForce(new Vector2(0, jumpSpeed));
                    grounded = false;
                }
            }

            //side direction
            direction = (Input.GetAxis("Horizontal"));
            body.AddForce(new Vector2(direction, 0));

            //if velocity upwards, disable collide
            if (body.velocity.y > 0)
            {
                collide.enabled = false;
            }
            else
            {
                collide.enabled = true;
            }
        }
        #endregion
        public void GameOver()
        {
            GameManager.instance.GameOver(currentHeightAchieved);
        }
        #endregion
    }
}