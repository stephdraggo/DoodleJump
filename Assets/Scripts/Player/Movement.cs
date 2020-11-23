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
        private GameManager game;
        private Scores score;
        private Animator animate;
        private SpriteRenderer render;
        private Collider2D collide;
        private Rigidbody2D body;


        private float direction;
        private int currentHeightAchieved, currentHeight, highestHeight;
        [SerializeField] private bool grounded;


        [SerializeField] private Text currentHeightAchievedText, currentHeightText, highestHeightText;
        [SerializeField] private float jumpSpeed, heightMultiplyer;

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip jump, land;
        #endregion
        #region Start
        void Start()
        {
            game = FindObjectOfType<GameManager>();
            score = FindObjectOfType<Scores>();

            //connect components
            animate = GetComponent<Animator>();
            render = GetComponent<SpriteRenderer>();
            collide = GetComponent<CircleCollider2D>();
            body = GetComponent<Rigidbody2D>();

            //get highest score
            if (score.HighScores.Length>2)
            {
                highestHeight = score.HighScores[0].score;
                highestHeightText.text = ("Beat " + score.HighScores[0].name + "'s " + highestHeight.ToString() + " metres");
            }
            else
            {
                highestHeight = 0;
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
            grounded = true;
            sfxSource.clip = land;
            sfxSource.Play();
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
            currentHeight = (int)(gameObject.transform.position.y * heightMultiplyer);
            currentHeightText.text = "Current height: " + currentHeight.ToString();
            if (currentHeight > currentHeightAchieved)
            {
                currentHeightAchieved = currentHeight;
                currentHeightAchievedText.text = "Highest this round: " + currentHeightAchieved.ToString();
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
        public void GameOver()
        {
            game.GameOver(currentHeightAchieved);
        }
        #endregion
    }
}