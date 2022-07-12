using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float dieTime, dmg;
    public GameObject diePEFFECT;


    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Die();
    }

  
    void Update()
    {
        
    }

    IEnumerator CountDownTimer() 
    {
        yield return new WaitForSeconds(dieTime);

        Die();

    }

    void Die() {
        Destroy(gameObject);
    }
}
