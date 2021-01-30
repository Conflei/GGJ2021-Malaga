using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeOscar : MonoBehaviour
{
    //Movimiento: A y D      Salto: Espacio      Pausa: P     Ataque: L (?)
    
    public Canvas canvasPausa;
    public int hp = 3;
    public int maxHp = 3;
    public float tamañoX;
    public float tamañoY;
    private BoxCollider2D attackCollider;
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 6;
    public float fuerzaSalto = 250f;
    private bool jump;
    public Transform refPlayer;
    float valorAlfaDeseadoTelaNegra;
    public bool enSuelo;
    public UnityEngine.UI.Image telaNegra;
    public UnityEngine.UI.Image barraVerde;
    bool active; //para la pausa

  /* public bool isClimbing;
    public LayerMask whatIsLadder;
    public float distance;
    private float movY;
  */
  /*
    public bool onLadder = false;
    public float climbSpeed;
    public float exitHop = 3f;
    public bool usingLadder = false;
  */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        attackCollider.enabled = false;
        telaNegra.color = new Color(0, 0, 0); //Color inicial fade.
        valorAlfaDeseadoTelaNegra = 0;
        canvasPausa.enabled = false;
        hp = maxHp;

    }

    
    void Update()
    {
        enSuelo = Physics2D.OverlapCircle(refPlayer.position, 0.3f, 1 << 8); //Definicion suelo
        anim.SetBool("enSuelo", enSuelo); //Animator suelo
       
        float movX;
        
        movX = Input.GetAxis("Horizontal"); //Eje horizontal
        anim.SetFloat("absMovX", Mathf.Abs(movX)); //Animacion movimiento en funcion valor movX
        rb.velocity = new Vector2(movX  * speed, rb.velocity.y); //
      
        if (movX < 0) transform.localScale = new Vector3(-tamañoX, tamañoY, 1); //Cambiar escala a negativo al disminuir movimiento X
        if (movX > 0) transform.localScale = new Vector3(tamañoX, tamañoY, 1);

        if ((Input.GetKeyDown(KeyCode.Space) && enSuelo))
        {
            jump = true;
        }
        Pausa();
        Atacar();

        
    
    }

    private void FixedUpdate()
    {
        if (jump)
        {

            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            jump = false;

        }

       /* RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

            if (isClimbing)
            {
                movY = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(rb.position.x, movY * speed);
                rb.gravityScale = 0;
            }else
            {
                rb.gravityScale = 4;
            }*/
            
        
        

        float valorAlfa = Mathf.Lerp(telaNegra.color.a, valorAlfaDeseadoTelaNegra, .03f); //
        telaNegra.color = new Color(0, 0, 0, valorAlfa);

        if (valorAlfa > 0.99f && valorAlfaDeseadoTelaNegra == 1)
        {
           // SceneManager.LoadScene();
        }

        if (hp <= 0)
        {
            anim.Play("Muerte");
        }
    }

    public void iniciarFadeOut()
    {
        valorAlfaDeseadoTelaNegra = 1; //Se llama desde la animacion de muerte
    }
    public void ActualizarVida()
    {
        barraVerde.fillAmount = (float)hp / maxHp;
    }

    public void Atacar()
    {
       
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (attackCollider.enabled == false)
            {

                transform.position = transform.position + new Vector3(0.001f, 0, 0); //Evita un pequeño fallo que no nos permite atacar al estar quietos
                anim.SetTrigger("Atacar");
                attackCollider.enabled = true;
            }
        }
    }

    public void NOAtacar()
    {
        attackCollider.enabled = false;
    }

    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            active = !active;
            if (active)
            {
                anim.enabled = false; //Para evitar que se reproduzcan animaciones con el menu de pausa
                canvasPausa.enabled = true;
                Time.timeScale = 0;
            }
            else 
            {
                anim.enabled = true; //Activa las animaciones.
                canvasPausa.enabled = false;
                Time.timeScale = 1; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag =="Fantasma")
        {
            anim.SetTrigger("Dañado");
        }
    }

   /* private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == ("Escalera"))
        {
            if (Input.GetAxisRaw("Vertical") !=0)
            {
               
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                enSuelo = false;
                usingLadder = onLadder;
                
            }else if(Input.GetAxisRaw("Vertical") ==0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("Escalera") &&onLadder)
        {
            rb.gravityScale = 4;
            onLadder = false;
            usingLadder = onLadder;
            if (!enSuelo)
            {
                rb.velocity = new Vector2(rb.velocity.x, exitHop);
            }
        }
    }*/
}
