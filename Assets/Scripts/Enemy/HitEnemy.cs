using System.Collections;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{

    void OTriggerEnter2D(Collider2D coll)
    {
        if(CompareTag("Player"))
        {
            print("Daño");
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
