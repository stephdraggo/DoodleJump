using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Player
{
    [AddComponentMenu("Doodle Jump/Player/Movement")]
    //needed components for player
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        #region Variables
        //game references
        private GameManager game;
        private ScoresNew score;

        //component references
        private Animator animate;
        private SpriteRenderer render;
        private Collider2D collide;
        private Rigidbody2D body;

        //gameplay variables
        private float direction;
        private int currentHeightAchieved, currentHeight, highestHeight;
        private bool grounded;

        [Header("Make sure these are connected.")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip jump, land;

        [SerializeField] private Text currentHeightAchievedText, currentHeightText, highestHeightText;
        [SerializeField] private float jumpSpeed, heightMultiplyer;
        #endregion
        #region Awake
        /// <summary>
        /// Connect important objects.
        /// </summary>
        private void Awake()
        {
            //connect game
            game = FindObjectOfType<GameManager>();
            score = FindObjectOfType<ScoresNew>();

            //connect components
            animate = GetComponent<Animator>();
            render = GetComponent<SpriteRenderer>();
            collide = GetComponent<CircleCollider2D>();
            body = GetComponent<Rigidbody2D>();
        }
        #endregion
        #region Start
        /// <summary>
        /// Display highest score.
        /// </summary>
        void Start()
        {
            //get highest score for display
            if (score.HighScores!=null) //if there are high scores
            {
                highestHeight = score.HighScores[0].score; //get highest one
                highestHeightText.text = ("Beat " + score.HighScores[0].name + "'s " + highestHeight.ToString() + " metres"); //display highest score with text
            }
            else
            {
                highestHeight = 0; //default is 0
                highestHeightText.text = ("Beat Sam's 0 metres");
            }
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
            //if collided
            grounded = true; //player is grounded
            sfxSource.clip = land; //assign landing sound
            sfxSource.Play(); //play landing sound
            animate.SetInteger("StateIndex", 1); //run
        }
        #endregion
        #region Functions
        #region update height done
        /// <summary>
        /// sets current height according to y position, checks and updates achieved height, updates ui
        /// </summary>
        private void UpdateHeight()
        {
            currentHeight = (int)(gameObject.transform.position.y * heightMultiplyer); //display height is current y coordinate multiplied by height mod as an int
            currentHeightText.text = "Current height: " + currentHeight.ToString(); //display current height as text
            if (currentHeight > currentHeightAchieved) //if current height is highest point so far
            {
                currentHeightAchieved = currentHeight; //update highest to height to current height
                currentHeightAchievedText.text = "Highest this round: " + currentHeightAchieved.ToString(); //display highest point as text
            }
        }
        #endregion
        #region update move
        /// <summary>
        /// all the movement and animation stuff
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
                //run or idle
                if (direction > 0.1 || direction < -0.1)
                {
                    animate.SetInteger("StateIndex", 1); //run
                }
                else
                {
                    animate.SetInteger("StateIndex", 0); //idle
                }

                //override with jump if applicable
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    body.AddForce(new Vector2(0, jumpSpeed));
                    grounded = false;
                    sfxSource.clip = jump;
                    sfxSource.Play();
                    animate.SetInteger("StateIndex", 3); //jump
                }
            }

            //side direction
            direction = (Input.GetAxis("Horizontal"));
            body.AddForce(new Vector2(direction, 0));

            //if velocity upwards, disable collide
            if (body.velocity.y > 0 || Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.S))
                {
                    animate.SetInteger("StateIndex", 2); //crouch
                }
                collide.enabled = false;
            }
            else
            {
                collide.enabled = true;
            }
        }
        #endregion
        #region game over
        /// <summary>
        /// Send achieved height to game manager for calculating.
        /// </summary>
        public void GameOver()
        {
            game.GameOver(currentHeightAchieved);
        }
        #endregion
        #endregion
    }
}