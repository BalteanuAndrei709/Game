using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private float directionX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14;
    
    private enum MovementState { idle, running, jumping, falling }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
        
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (directionX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false; 
        }
        else if (directionX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if( rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);
    }
}