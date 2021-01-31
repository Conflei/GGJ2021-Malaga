using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aEnemigo : MonoBehaviour
{
    public AudioSource atacar;
    BoxCollider2D box;
    public float tamañoX = 1.25f;
    public float tamañoY = 1.20f;

    public int maxHp = 4;

    public int hp = 4;
    Rigidbody2D rb;
    //public float velX = 10f;
    Animator anim;

    float limiteCaminataIzq;
    float limiteCaminataDer;

    public float velCaminata = 3f;
    int dirección = 1;

    private bool movement = true;

    private bool knockBack = false;
    private Vector2 knockBackDirection;
    private float knockBackTime;
    public float knockBackForce = 150f;
    public float knockBackHeith = 200f;

    enum tipoComportamientoZombie
    {
        pasivo, persecución, ataque
    }

    tipoComportamientoZombie comportamiento = tipoComportamientoZombie.pasivo;
    public float entradaZonaActiva = 8f;
    public float salidaZonaActiva = 2f;
   // public float distanciaAtaque = 0.3f;

    float distanciaConPlayer;
    public Transform Player;
    

    



    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        hp = maxHp;
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        limiteCaminataIzq = transform.position.x - GetComponent<CircleCollider2D>().radius;
        limiteCaminataDer = transform.position.x + GetComponent<CircleCollider2D>().radius;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 1)
        {
            distanciaConPlayer = Mathf.Abs(Player.position.x - transform.position.x);
            switch (comportamiento)
            {
                case tipoComportamientoZombie.pasivo:


                    if (!knockBack)
                    {
                        rb.velocity = new Vector2(velCaminata * dirección, rb.velocity.y); //si no queremos que salte (40, 0)
                        if (transform.position.x < limiteCaminataIzq) dirección = 1;
                        if (transform.position.x > limiteCaminataDer) dirección = -1;

                        //entrar zona persecucion
                        if (distanciaConPlayer < entradaZonaActiva) comportamiento = tipoComportamientoZombie.persecución;
                    }

                    break;

                case tipoComportamientoZombie.persecución:

                    if (!knockBack)
                    {
                        rb.velocity = new Vector2(velCaminata * dirección, rb.velocity.y); //si no queremos que salte (40, 0)
                        if (Player.position.x > transform.position.x) dirección = 1;
                        if (Player.position.x < transform.position.x) dirección = -1;

                        //vovler zona parsiva
                        if (distanciaConPlayer > entradaZonaActiva) comportamiento = tipoComportamientoZombie.pasivo;

                    }
                    transform.localScale = new Vector3(tamañoX * dirección, tamañoY, 1f);
                    break;


            }
        }
        transform.localScale = new Vector3(tamañoX* dirección, tamañoY, 1f);


        

        if (Time.time > knockBackTime && knockBackTime != 0f)
        {
            movement = true;
            knockBack = false;
        }
        
    }

    void EnableMovement()
    {
        movement = true;
    }

    void FixedUpdate()
    {

        if (hp <= 0)
        {

            SceneManager.LoadScene("Scenes/Ganar");

        }


      

        
    }
    

    private void KnockBack(Vector3 position)
    {
        if (hp >= 1)
        {
            if (knockBack) return;



            //enemigo a la derecha
            if (position.x - this.transform.position.x <= 0)
            {
                knockBackDirection = new Vector2(1, 1);
            }
            //enemigo a la izquierda
            else
            {
                knockBackDirection = new Vector2(-1, 1);
            }

            //Aplica fuerza y señala durante cuanto tiempo
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(knockBackDirection.x * knockBackForce, knockBackDirection.y * knockBackHeith), ForceMode2D.Impulse);
            knockBackTime = Time.time + 0.5f;


            //desabilita el movimiento
            movement = false;
            knockBack = true;
        }
    }

    //detecta la colisión con el enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.collider.tag =="Player")
        {
            if (hp >=1)
            {
                anim.SetTrigger("Atacar");
                atacar.Play();
            }
            
        }

    }
    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hacha")
        {
            
            hp -= 2;
            if (hp >= 1)
            {
                KnockBack(collision.transform.position);
                
            }
           
            if (hp == 0)
            {

                SceneManager.LoadScene("Scenes/Ganar");

            }



        }
        if (collision.tag == "Palanca")
        {

            hp -= 1;
            if (hp >= 1)
            {
                KnockBack(collision.transform.position);

            }

            if (hp == 0)
            {

                SceneManager.LoadScene("Scenes/Ganar");

            }



        }
    }
   
    
}
