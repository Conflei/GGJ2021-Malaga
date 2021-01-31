using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasPausa;
    public Image telaNegra;
    public Image barraVerde;
    float valorAlfaDeseadoTelaNegra;
    public GameObject CanvasArma;
    public GameObject CanvasDroga;
    public GameObject CanvasLuz;

    private PersonajeOscar personaje;

    // Start is called before the first frame update
    void Start()
    {
        telaNegra.color = new Color(0, 0, 0); //Color inicial fade.
        valorAlfaDeseadoTelaNegra = 0;
        canvasPausa.SetActive(false);
        CanvasArma.SetActive(false);
        CanvasDroga.SetActive(false);
        CanvasLuz.SetActive(false);
        personaje = this.GetComponent<PersonajeOscar>();
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
        CanvasDroga.SetActive(false);
        Time.timeScale = 1;
    }
    public void CerrarMenuArma()
    {
        CanvasArma.SetActive(false);
        Time.timeScale = 1;
       
    }

    public void CerrarMenuLuz()
    {
        CanvasLuz.SetActive(false);
        Time.timeScale = 1;
    }

    public void ObjectAcquired(ObjectType type)
    {
        print("object acquired " + type);
        switch (type)
        {
            case ObjectType.Light:
                personaje.innerLight.SetActive(false);
                CanvasLuz.SetActive(true);
                break;
            case ObjectType.Weapon:
                CanvasArma.SetActive(true);
                break;
            case ObjectType.Drug:
                CanvasDroga.SetActive(true);
                CanvasDroga.gameObject.SetActive(true);
                break;
            case ObjectType.Key:
                break;
        }
    }
}
