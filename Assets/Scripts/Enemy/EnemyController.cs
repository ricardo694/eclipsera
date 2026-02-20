using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


  
    //ataque
    public bool atacando;    
    //vigilancia
    public int rutina;
    public float cronometro;
    public int direccion;

    public float speedWalk;
    public float speedRun;
    public GameObject target;
    private Animator animator;

    public float rangoVision;
    public float rangoAtaque;
    public GameObject rango;
    public GameObject Hit;


    void Start()
    {

        animator = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
           
    }

    public void Comportamiento()
    {
        if ( Mathf.Abs(transform.position.x - target.transform.position.x)> rangoVision && !atacando)
        {
            animator.SetBool("run",false);
            cronometro += 1 *Time.deltaTime;
            if (cronometro >=4)
            {
                rutina = Random.Range(0,2);
                cronometro=0;
            }

            switch (rutina)
            {
                
                case 0:
                    animator.SetBool("walk",false);
                    break;

                case 1:
                    direccion = Random.Range(0,2);
                    rutina++;
                    break;

                case 2:
                    switch (direccion)
                        {
                            case 0:
                                transform.rotation = Quaternion.Euler(0,180,0);
                                transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                                break;
                            case 1:
                                transform.rotation = Quaternion.Euler(0,0,0);
                                transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                                break;
                        }
                    animator.SetBool("walk",true);
                    break;


            }
        }
        else
        {
            if ( Mathf.Abs(transform.position.x - target.transform.position.x) > rangoAtaque && !atacando)
            {
                if(transform.position.x < target.transform.position.x)
                {
                    animator.SetBool("walk",false);
                    animator.SetBool("run",true);
                    transform.Translate(Vector3.right * speedRun *Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    animator.SetBool("attack",false);

                }
                else
                {
                    animator.SetBool("walk",false);
                    animator.SetBool("run",true);
                    transform.Translate(Vector3.right * speedRun *Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    animator.SetBool("attack",false);
                }
            }
            else
            {
                if(!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0,0,0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    animator.SetBool("walk",false);
                    animator.SetBool("run",false);
                }
            }
        }
    }
    public void FinalAni()
    {
        animator.SetBool("attack",false);   
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled=true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled=false;
    }

}
