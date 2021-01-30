using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public Canvas canvasPausa;
    public Image telaNegra;
    public Image barraVerde;
    float valorAlfaDeseadoTelaNegra;
    public Canvas CanvasArma;
    public Canvas CanvasDroga;
    public Canvas CanvasLuz;

    // Start is called before the first frame update
    void Start()
    {
        telaNegra.color = new Color(0, 0, 0); //Color inicial fade.
        valorAlfaDeseadoTelaNegra = 0;
        canvasPausa.enabled = false;
        CanvasArma.enabled = false;
        CanvasDroga.enabled = false;
        CanvasLuz.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float valorAlfa = Mathf.Lerp(telaNegra.color.a, valorAlfaDeseadoTelaNegra, .1f); //
        telaNegra.color = new Color(0, 0, 0, valorAlfa);

        if (valorAlfa > 0.99f && valorAlfaDeseadoTelaNegra == 1)
        {
            SceneManager.LoadScene(escenaActual()); // Para recargar el nivel actual.

        }
    }
    int escenaActual()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }


    public void iniciarFadeOut()
    {
        valorAlfaDeseadoTelaNegra = 1; //Se llama desde la animacion de muerte
    }

    public void CerrarMenuDroga()
    {
        CanvasDroga.enabled = false;
        Time.timeScale = 1;
    }
    public void CerrarMenuArma()
    {
        CanvasArma.enabled = false;
        Time.timeScale = 1;
       
    }

    public void CerrarMenuLuz()
    {
        CanvasLuz.enabled = false;
        Time.timeScale = 1;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag =="escogerArma")
        {
            CanvasArma.enabled = true;
            Destroy(col.gameObject);
            Time.timeScale = 0;
        }

        if (col.collider.tag == "escogeDroga")
        {
            CanvasDroga.enabled = true;
            Destroy(col.gameObject);
            Time.timeScale = 0;
        }

        if (col.collider.tag =="escogeLuz")
        {
            CanvasLuz.enabled = true;
            Destroy(col.gameObject);
            Time.timeScale = 0;
        }
    }
}
