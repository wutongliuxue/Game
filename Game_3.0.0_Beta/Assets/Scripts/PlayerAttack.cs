using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    // This is where the bullet will spwan.
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullets;
    private Animator animator;
    private Motivation playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<Motivation>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.J) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        Attack();

        cooldownTimer += Time.deltaTime;

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        cooldownTimer = 0;

        // pool the fireball and set the bullet position where it will show up.
        bullets[FindBullet()].transform.position = firePoint.position;
        // Set the facing direction of bullet to the same direction of player facing. 
        bullets[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    // pool the fireball and set the bullet position where it will show up.
    private int FindBullet()
    {
        for(int i=0; i< bullets.Length; i++)
        {
            if(!bullets[i].activeInHierarchy)
            return i;
        }
        return 0;
    }
}
