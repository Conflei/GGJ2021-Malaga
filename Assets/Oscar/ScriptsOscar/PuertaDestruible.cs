using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDestruible : MonoBehaviour
{
    public AudioSource sonidoGolpearPuerta;
    public int hp = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        if ((collision.tag == "Hacha") || (collision.tag =="Palanca"))
        {
            sonidoGolpearPuerta.Play();
        }

        if (collision.tag == "Hacha")
        {
            hp -= 1;
        }

        if (collision.tag =="Palanca")
        {
            hp -= 4;
        }
    }
}
