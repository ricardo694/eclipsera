
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed =2.0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool recibiendoDano;
    public float fuerzaRebote = 10f;

    private bool enMovimiento;

    private Vector3 escalaInicial;


    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        escalaInicial = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized; 

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(escalaInicial.x, escalaInicial.y, escalaInicial.z);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-escalaInicial.x, escalaInicial.y, escalaInicial.z);
            }

        movement =new Vector2(direction.x, 0);
        enMovimiento = true;

        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }
        if (!recibiendoDano)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

            animator.SetBool("enMovimiento",enMovimiento);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDano = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().RecibeDano(direccionDano,1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Puño"))
        {
            Vector2 direccionDano = new Vector2(collision.gameObject.transform.position.x, 0);

            RecibeDano(direccionDano,1);
        }
    }
    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if(!recibiendoDano)
        {
            recibiendoDano = true;
            float dir = transform.position.x - direccion.x;
        Vector2 rebote = new Vector2(dir, 2f).normalized; // más fuerza hacia arriba

            rb.AddForce(rebote*fuerzaRebote, ForceMode2D.Impulse);  
            StartCoroutine(DesactivarDano());
        }

    }
    IEnumerator DesactivarDano()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDano=false;
        rb.linearVelocity = Vector2.zero;
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (transform.position, detectionRadius);        
    }
}
