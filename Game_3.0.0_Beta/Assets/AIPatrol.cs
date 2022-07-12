using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;
    public float range;
    public float timeBetweenShots;
    public float shootSpeed;
    private float distanceToPlayer;


    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;
    private bool canShoot;
    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, ShootPos;

    public GameObject fireBall;


    void Start()
    {   
        mustPatrol = true;
        canShoot = true;


    }

   
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }

        distanceToPlayer = Vector2.Distance(transform.position, player.position); //automatically calculate the distance between player and enemy

        if(distanceToPlayer <= range) {
            if(player.position.x > transform.position.x && transform.localScale.x > 0 || player.position.x < transform.position.x && transform.localScale.x < 0) 
            {
               FlipCharactor();
            }
            mustPatrol = false;
            rb.velocity = Vector2.zero; //stops the velocity

            if(canShoot) {
                
                StartCoroutine(Attack());
            }
            

        }
        else {
            mustPatrol = true;
        }

    }

    private void FixedUpdate() 
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
        }

    }
    
    void Patrol() 
    {
        if(mustFlip || bodyCollider.IsTouchingLayers(groundLayer))
        {
            FlipCharactor();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void FlipCharactor() 
    {
        mustPatrol = false;

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
       
        walkSpeed *= -1;

        mustPatrol = true;

    }

    IEnumerator Attack()  //returns the amount of time
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        GameObject newBall = Instantiate(fireBall, ShootPos.position, Quaternion.identity);

        newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

        canShoot = true;

    }

}


