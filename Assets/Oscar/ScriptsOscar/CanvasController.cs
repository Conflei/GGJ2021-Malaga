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
    public Image barraRoja;
    public Image barraNegra;

    float valorAlfaDeseadoTelaNegra;
    public GameObject CanvasArma;
    public GameObject CanvasDroga;
    public GameObject CanvasLuz;

    public GameObject fullBarReference;

    public PersonajeOscar personaje { set; get; }

    public GameObject uiKey;

    public Text bottomText;

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
        float valorAlfa = Mathf.Lerp(telaNegra.color.a, valorAlfaDeseadoTelaNegra, .08f); //
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

    public void IncreaseLife()
    {
        barraVerde.transform.localScale = barraNegra.transform.localScale = barraRoja.transform.localScale = fullBarReference.transform.localScale;
        barraVerde.transform.position = barraNegra.transform.position = barraRoja.transform.position = fullBarReference.transform.position;
        
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
                uiKey.SetActive(true);
                personaje.hasKey = true;
                break;
            case ObjectType.LockedDoor:
                if (!personaje.hasKey)
                {
                    StartCoroutine(ShowTextWorker("Encuentra la llave"));
                }
                break;
        }
    }

    public IEnumerator ShowTextWorker(string text)
    {
        bottomText.text = text;
        var cg = bottomText.GetComponent<CanvasGroup>();
        cg.alpha = 0f;
        while (cg.alpha < 1f)
        {
            cg.alpha += 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (cg.alpha > 0f)
        {
            cg.alpha -= 0.1f;
            yield return null;
        }
    }
}
