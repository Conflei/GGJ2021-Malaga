using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeOscar : MonoBehaviour
{
    //Movimiento: A y D      Salto: Espacio      Pausa: P     Ataque: L (?)

    public bool tengoHacha = false;
    public bool tengoPalanca = false;
    public GameObject Hacha;
    public GameObject Palanca;

    public GameObject LamparaLuz;
    public GameObject LinternaLuz;
    public bool TengoLampara = false;
    public bool TengoLinterna = false;

    public GameObject AbalorioSalto;

    public bool PuedesAtacar;// bool para intentar q no se repita la animacion de ataque al presionar repetidamente la tecla.

    private int congelar = 1; //Congela el movimiento desde el animation poniendo su valor a 0 en rb.velocity
    public CanvasController canvasController;
    public int hp = 3;
    public int maxHp = 3;
    public float tamañoX;
    public float tamañoY;
    private BoxCollider2D attackColliderHacha;
    private BoxCollider2D attackColliderPalanca;
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 6;
    public float fuerzaSalto = 250f;
    private bool jump;
    public Transform refPlayer;
    public bool enSuelo;
    bool active; //para la pausa

    /* public bool isClimbing;
      public LayerMask whatIsLadder;
      public float distance;
      private float movY;
    */
    // Cosas escalera  desde linea 34 hasta la 39 y en el ontriggerstay/triggerexit.
    private BoxCollider2D escaleraCollider;
    public bool onLadder = false;
    public float climbSpeed = 3;
    public float exitHop = 10f;
    public bool usingLadder = false;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackColliderHacha = transform.GetChild(0).GetComponent<BoxCollider2D>();
        Hacha.SetActive(false);
        Palanca.SetActive(false);
        attackColliderHacha.enabled = false;
        attackColliderPalanca = transform.GetChild(3).GetComponent<BoxCollider2D>();
        LamparaLuz.SetActive(false);
        LinternaLuz.SetActive(false);
        AbalorioSalto.SetActive(false);
        

        hp = maxHp;

    }

    
    void Update()
    {
        if (hp >= 1)
        {
            enSuelo = Physics2D.OverlapCircle(refPlayer.position, 0.3f, 1 << 8); //Definicion suelo
            anim.SetBool("enSuelo", enSuelo); //Animator suelo

            float movX;

            movX = Input.GetAxis("Horizontal"); //Eje horizontal
            anim.SetFloat("absMovX", Mathf.Abs(movX)); //Animacion movimiento en funcion valor movX
            rb.velocity = new Vector2(movX * congelar * speed, rb.velocity.y); //

            if (movX < 0) transform.localScale = new Vector3(-tamañoX, tamañoY, 1); //Cambiar escala a negativo al disminuir movimiento X
            if (movX > 0) transform.localScale = new Vector3(tamañoX, tamañoY, 1);

            if ((Input.GetKeyDown(KeyCode.Space) && enSuelo))
            {

                jump = true;

            }
            Pausa();
            Atacar();
        }
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            tengoHacha = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            tengoPalanca = true;
        }*/



    }

    private void FixedUpdate()
    {
        if (jump)
        {

            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            jump = false;

        }

        if (tengoHacha)
        {
            Hacha.SetActive(true);
        }
        if (tengoPalanca)
        {
            Palanca.SetActive(true);
        }




        canvasController.barraVerde.fillAmount = (float)hp / maxHp;

        if (hp <= 0)
        {
            canvasController.iniciarFadeOut();
        }
    }
    public void CogerHacha()
    {
        tengoHacha = true;
        Destroy(Palanca);
    }
    public void CogerPalanca()
    {
        tengoPalanca = true;
        Destroy(Hacha);
    }
    
    public void ActualizarVida()
    {
        canvasController.barraVerde.fillAmount = (float)hp / maxHp;
    }

    public void Atacar()
    {
       
        if (Input.GetKeyDown(KeyCode.L))
        {

            if (tengoHacha)
            {
                if (PuedesAtacar)
                {
                   // attackColliderHacha.enabled = true;
                    transform.position = transform.position + new Vector3(0.009f, 0, 0); //Evita un pequeño fallo que no nos permite atacar al estar quietos
                    anim.SetTrigger("Atacar");
                }
            }

            if (tengoPalanca)
            {
                if (PuedesAtacar)
                {
                   // attackColliderPalanca.enabled = true;
                    transform.position = transform.position + new Vector3(0.009f, 0, 0); //Evita un pequeño fallo que no nos permite atacar al estar quietos
                    anim.SetTrigger("Atacar");
                }
            }




           
        }
    }

    public void AtacarArma()
    {
        attackColliderHacha.enabled = true;
        attackColliderPalanca.enabled = true;
    }
    public void NOAtacarArma()
    {
        attackColliderHacha.enabled = false;
        attackColliderPalanca.enabled = false;
    }
    public void PuedeAtacar()
    {
        PuedesAtacar = true;
    }
    public void NOAtacar()
    {

        PuedesAtacar = false;
    }

    public void CongelaMovimiento()
    {
        congelar = 0;
    }

    public void NOCongelarMovimiento()
    {
        congelar = 1;
    }
    
    public void Abalorio()
    {
        AbalorioSalto.SetActive(true);
        maxHp += 2;
    }
    public void Droga()
    {
        fuerzaSalto += 50;
    }
   
    public void Linterna()
    {
        LinternaLuz.SetActive(true);
        Destroy(LamparaLuz);
    }

    public void Lampara()
    {
        LamparaLuz.SetActive(true);
        Destroy(LinternaLuz);
    }

    private void Pausa()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            active = !active;
            if (active)
            {
                anim.enabled = false; //Para evitar que se reproduzcan animaciones con el menu de pausa
                canvasController.canvasPausa.enabled = true;
                Time.timeScale = 0;
            }
            else 
            {
                anim.enabled = true; //Activa las animaciones.
                canvasController.canvasPausa.enabled = false;
                Time.timeScale = 1; 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag =="Enemy")
        {
            anim.SetTrigger("Dañado");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == ("Escalera"))
        {
            if (Input.GetAxisRaw("Vertical") !=0)
            {
                escaleraCollider = col.GetComponent<BoxCollider2D>();

                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                //enSuelo = false;
                usingLadder = onLadder;
                escaleraCollider.enabled = false;
                
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
            escaleraCollider = collision.GetComponent<BoxCollider2D>();
            rb.gravityScale = 4;
            onLadder = false;
            usingLadder = onLadder;
            escaleraCollider.enabled = true;
            if (!enSuelo)
            {
               // rb.velocity = new Vector2(rb.velocity.x, exitHop);
            }
        }
    }
}


/*
  
 
 
 */
