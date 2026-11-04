using UnityEngine;
using UnityEngine.InputSystem;

// https://youtu.be/lIZnIFqai2I (colliders)
// https://youtu.be/4vLYoFWV5lk
namespace Player {
    public class Movement : MonoBehaviour {
        public string myName = "EX";
        public float moveSpeed = 10f;
        public float dashPercentage = 2f;

        // Jumping:
        public float jumpForce = 20f;
        public float fallDelay = 0.1f;
        private float jumpTimer = 0f;

        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;
        public LayerMask groundLayer;

        // number of times character can jump in air:
        private int canJump = 3;
        private bool canMove = true;
        private bool facingRight = true;
        private bool isGrounded;

        private Rigidbody2D rb;
        private Animator animate;
        private Vector2 moveInput;
        private InputSystem_Actions controls;

        // Actions:
        InputAction jumpAction;
        InputAction sprintAction;
        InputAction crouchAction;
        InputAction blockAction;
        // InputAction attackAction;

        public void Awake() {
            controls = new InputSystem_Actions();
        }

        public void Start() {
            //controls.Player.Jump.performed += ctx => Jump();
            //controls.Player.Sprint.performed += ctx => Sprint();
            jumpAction = InputSystem.actions.FindAction("Jump");
            sprintAction = InputSystem.actions.FindAction("Sprint");
            rb = GetComponent<Rigidbody2D>();
            animate = GetComponent<Animator>();
            Debug.Log("I am alive and my name is " + myName);
        }

        public void OnEnable() {
            controls.Enable();
        }
 
        public void OnDisable() {
            controls.Disable();
        }

        public void Update() {
            if (canMove) {
                moveInput = controls.Player.Move.ReadValue<Vector2>();
                rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
                animate.SetBool("isRunning", moveInput.x != 0f);
                // Check direction:
                if (moveInput.x > 0 && !facingRight) {
                    Flip();
                } else if (moveInput.x < 0 && facingRight) {
                    Flip();
                }
                // Sprint: 
                if (sprintAction.IsPressed() && moveInput.x != 0f) {
                    animate.speed = 2f;
                } else {
                    animate.speed = 1f;
                }
            }
            // ACTIONS: 
            if (jumpAction.WasPressedThisFrame()) { Jump(); }
            if (jumpAction.WasReleasedThisFrame() && rb.linearVelocity.y > 0) { Shortjump(); }
            if (sprintAction.IsPressed()) { Sprint(); }
            if (rb.linearVelocity.y < -0.01f || jumpTimer > 0f) {
                Fall();
            }
            // var moveDirection = moveAction.ReadValue<Vector2>();
            // position += moveDirection * moveSpeed * Time.deltaTime;
        }
        
        public void Shortjump() {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        public void Jump() {
            if (canJump > 1 && jumpTimer > 0f || jumpTimer == -0.1f) {
                canJump--;
                animate.SetBool("isJumping", false);
                animate.SetBool("isFalling", true);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            } else if (canJump > 1) {
                canJump--;
                jumpTimer = Time.time + fallDelay;
                animate.SetBool("isFalling", false);
                animate.SetBool("isJumping", true);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }

        public void Fall() {
            if (!isGrounded && Time.time >= jumpTimer) {
                animate.SetBool("isJumping", false);
                animate.SetBool("isFalling", true);
                jumpTimer = -0.1f; 
            }
        }

        public void Sprint() {
            //Vector2 position = transform.position;
            //// Time.deltaTime = time in seconds since last frame
            //position.x += speed * Time.deltaTime;
            //transform.position = position;
            if (canMove) {
                moveInput = controls.Player.Move.ReadValue<Vector2>();
                rb.linearVelocity = new Vector2(moveInput.x * moveSpeed *dashPercentage, rb.linearVelocity.y);
            }   
        }

        public void FixedUpdate() {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (isGrounded) {
                canJump = 2;
                jumpTimer = 0f; 
                animate.SetBool("isFalling", false);
                animate.SetBool("isJumping", false); 
            }
        }

        // FLIP SPRITE TUTORIAL: https://youtu.be/Cr-j7EoM8bg
        public void Flip() {
            Vector3 currScale = transform.localScale;
            currScale.x *= -1;
            transform.localScale = currScale;
            facingRight = !facingRight;
        }

    } //Movement
} //namespace