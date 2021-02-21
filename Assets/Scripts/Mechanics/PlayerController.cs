using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using DG.Tweening;
namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public GameObject followObject;
        public Vector3 initpos;
        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/
        public Collider2D collider2d;
        /*internal new*/
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public static bool flipBool;
        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
  

        public Bounds Bounds => collider2d.bounds;


        int stickyBabyCount = 0, fireBabyCount = 0, waterBabyCount = 0;
        int maxBabyCount = 3;
        [SerializeField] GameObject stickyBlobPrefab, fireBlobPrefab, waterBlobPrefab;
        [SerializeField] GameObject poof, magicPoof;
        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            initpos = followObject.transform.localPosition;
        }
        protected override void OnEnable()
        {

            base.OnEnable();
            TrainMove.ExtendTrain(followObject.GetComponent<FollowedBy>());


        }
        protected override void Start()
        {
            base.Start();
            GameObject gameobj = GameObject.Find("Barrier");
            if (gameobj)
            {
                gameobj.transform.position = new Vector3(transform.position.x - 7, transform.position.y - 7, transform.position.z);
                GameObject.Find("Barrier").transform.DOMoveY(transform.position.y + 10, 3);
            }
        }

       
        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
            {
                spriteRenderer.flipX = false;
                if (flipBool != false)
                    TrainMove.flip();
                flipBool = false;
                followObject.transform.localPosition = new Vector3(-1 * initpos.x, initpos.y, initpos.z);


            }
            else if (move.x < -0.01f)
            {
                spriteRenderer.flipX = true;
                if (flipBool != true)
                    TrainMove.flip();
                flipBool = true;
                followObject.transform.localPosition = 1 * initpos;

            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            /*if (collision.gameObject.tag == "Blob")
            {
                Physics2D.IgnoreCollision(collision.collider, collider2d);
                Debug.Log("collision happenibg");
            }*/

        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            /*if (collision.gameObject.tag == "Blob")
            {
                Physics2D.IgnoreCollision(collision.collider, collider2d);
                Debug.Log("collision happenibg stay");
            }*/

        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "BabyBlob")
            {

                int temp = other.GetComponent<BabyBlobHandler>().id;
                switch (temp)
                {
                    case 0:
                        stickyBabyCount++;
                        if (stickyBabyCount >= maxBabyCount)
                        {
                            stickyBabyCount = 0;
                            GameObject go = Instantiate(stickyBlobPrefab, other.transform.position + Vector3.forward * -0.2f, Quaternion.identity);
                            go.GetComponent<BlobHandler>().mergePos = transform.GetChild(0);
                            go.GetComponent<SpringJoint2D>().connectedBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
                            TrainMove.ExtendTrain(go.GetComponent<FollowedBy>());
                            Instantiate(magicPoof, other.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(poof, other.transform.position, Quaternion.identity);
                        }
                        break;
                    case 1:
                        fireBabyCount++;
                        if (fireBabyCount >= maxBabyCount)
                        {
                            fireBabyCount = 0;
                            GameObject go = Instantiate(fireBlobPrefab, other.transform.position + Vector3.forward * -0.2f, Quaternion.identity);
                            go.GetComponent<BlobHandler>().mergePos = transform.GetChild(0);
                            go.GetComponent<SpringJoint2D>().connectedBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
                            TrainMove.ExtendTrain(go.GetComponent<FollowedBy>());
                            Instantiate(magicPoof, other.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(poof, other.transform.position, Quaternion.identity);
                        }
                        break;
                    default:
                        waterBabyCount++;
                        if (waterBabyCount >= maxBabyCount)
                        {
                            waterBabyCount = 0;
                            GameObject go = Instantiate(waterBlobPrefab, other.transform.position + Vector3.forward * -0.2f, Quaternion.identity);
                            go.GetComponent<BlobHandler>().mergePos = transform.GetChild(0);
                            go.GetComponent<SpringJoint2D>().connectedBody = transform.GetChild(0).GetComponent<Rigidbody2D>();
                            TrainMove.ExtendTrain(go.GetComponent<FollowedBy>());
                            Instantiate(magicPoof, other.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(poof, other.transform.position, Quaternion.identity);
                        }
                        break;
                }

                Destroy(other.gameObject);
            }
        }

    }
}