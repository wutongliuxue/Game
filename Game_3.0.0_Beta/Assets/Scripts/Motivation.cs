using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motivation : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float jumpSpeed;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer; 
    private Rigidbody2D body;
    private Animator animator; 
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    // private bool grounded;

    private void Awake()
    {
        // Grab references for rigibody and animator object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
       
       horizontalInput = Input.GetAxis("Horizontal");
       body.velocity = new Vector2(Input.GetAxis("Horizontal")* speed, body.velocity.y);
       // Flip player when moving left/right.
       if(Input.GetAxis("Horizontal") > 0.01f)
           transform.localScale = Vector3.one;
           else if(Input.GetAxis("Horizontal") < -0.01f)
              transform.localScale = new Vector3(-1,1,1);
              // Ground is for jumping only when character is on the ground
       
     
       
      if(Input.GetKey(KeyCode.W) && isGrounded())
      {
          Jump();
      
      }
     
       animator.SetBool("Run", horizontalInput != 0);
      animator.SetBool("Grounded", isGrounded());
         
    }


    private void Jump()
    {
        
        body.velocity = new Vector2(body.velocity.x,jumpSpeed);
        animator.SetTrigger("Jump");
        
    }
    // Ground is to check if the character is on the ground or not

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null ;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null ;
    }
    public bool canAttack()
    {
         return true;//horizontalInput == 0 && isGrounded() && !onWall();
    }
}
