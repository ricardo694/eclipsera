
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movimiento
    public float velocidad = 5f;
    //salto
    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;
    public Animator animator;
    private Rigidbody2D rb;
    private bool enSuelo;
    //dano
    private bool recibiendoDano;
    public float fuerzaRebote = 10f;
    //ataque
    private bool atacando;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!atacando)
        {
            Movimiento();
            /*salto*/
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
            enSuelo = hit.collider != null;

            if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDano)
            {
                rb.AddForce(new Vector2(0f, fuerzaSalto),ForceMode2D.Impulse);
            }
        }


        Animaciones();


        if (Input.GetKeyDown(KeyCode.J) && !atacando && enSuelo)
        {
            Atacando();
        }


    }
    public void RecibeDano(Vector2 direccion, int cantDano)
    {
        if(!recibiendoDano)
        {
            recibiendoDano = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote*fuerzaRebote, ForceMode2D.Impulse);  
            Invoke(nameof(DesactivarDano), 0.3f);
        }

    }
    public void Movimiento()
    {
                /*movivmiento*/
        float velocidadX = Input.GetAxis("Horizontal")*Time.deltaTime*velocidad;

        animator.SetFloat("movement", velocidadX*velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 posicion = transform.position;

        if(!recibiendoDano)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y,posicion.z);
    }
    
    public void Animaciones()
    {
        animator.SetBool("ensuelo",enSuelo);
        animator.SetBool("atacando",atacando); 
        // animator.SetBool("RecibeDano",recibiendoDano);
    }
    public void DesactivarDano()
    {
        recibiendoDano = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando=true;
    }

    public void DesactivaAtaque()
    {
        atacando=false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);        
    }
}
