using System.Collections;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    public Animator animator;
    EnemyController enemigo;


    void OTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            animator.SetBool("walk",false);
            animator.SetBool("run",false);
            animator.SetBool("attack",true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled=false;
        }
    }



    void Start()
    {

   
       
    }

    // Update is called once per frame
    void Update()
    {
      
           
    }

}