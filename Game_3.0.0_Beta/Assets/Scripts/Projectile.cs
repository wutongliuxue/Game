using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private float direction;
    private bool hit;
    private float lifetime; 
    private BoxCollider2D boxCollider;
    private Animator animator;


    private void Awake()
    {

        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(hit) return;
        // Put fly direction here.
        float movementSpeed = speed * Time.deltaTime* direction;
        // This is how the object can move, it cannot move along y or z because of 0.
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if(lifetime > 20) gameObject.SetActive(false);
    }

    // This is the method when bullet hits something.
    private void OnTriggerEnter2D(Collider2D collision)
    {
       hit = true;
       // Close box collider here.
       boxCollider.enabled = false;
       animator.SetTrigger("Explode");
       
    } 
    // We need this to control if the bullet flies to the right or left.
     public void SetDirection(float _direction)
     {
         lifetime =0;
         direction = _direction;
        gameObject.SetActive(true);
        // After shooting.
        hit = false;
        boxCollider.enabled = true;

        // Here we flip it

        float localScaleX = transform.localScale.x; // We only need x axis.
        if(Mathf.Sign(localScaleX) != _direction)
           localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
     }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
