using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {
    
    // Variables.
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    // Lifecycle.
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        directionX = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(directionX * moveSpeed, rigidBody.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    // Methods.
    private void UpdateAnimationState() {
        MovementState state;

        if(directionX > 0f) {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if(directionX < 0f) {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else {
            state = MovementState.idle;
        }

        if(rigidBody.velocity.y > .1f) {
            state = MovementState.jumping;
        }
        else if(rigidBody.velocity.y < -.1f) {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}