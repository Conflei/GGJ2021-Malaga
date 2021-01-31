using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDestruible : MonoBehaviour
{
    public CanvasController canvas;
    public AudioSource sonidoGolpearPuerta { set; get; }
    public int hp = 4;

    public Sprite[] sprites;
    private int currentState;

    private bool alive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        sonidoGolpearPuerta = GetComponent<AudioSource>();
        canvas = GameObject.FindGameObjectWithTag("Player").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (hp<=0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision door with " + collision.gameObject.tag);
        if ((collision.gameObject.tag == "Hacha") || (collision.gameObject.tag =="Palanca") || collision.gameObject.tag == "Player")
        {
            sonidoGolpearPuerta.Play();
            StartCoroutine(canvas.ShowTextWorker("Presiona \"L\" para atacar"));
        }

        if (collision.gameObject.tag == "Hacha")
        {
            hp -= 1;
            CheckAndAnimate();
        }

        if (collision.gameObject.tag =="Palanca")
        {
            hp -= 4;
            alive = false;
            CheckAndAnimate();
        }
    }

    public void CheckAndAnimate()
    {
        
        GetComponent<SpriteRenderer>().sprite = sprites[3];
        if (hp == 2) GetComponent<SpriteRenderer>().sprite = sprites[2];
        if (hp == 1) GetComponent<SpriteRenderer>().sprite = sprites[1];
        if (hp <= 0) GetComponent<SpriteRenderer>().sprite = sprites[0];


        if (hp <= 0)
        {
            alive = false;
            StartCoroutine(WaitAndHide());
        }
    }

    public IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
