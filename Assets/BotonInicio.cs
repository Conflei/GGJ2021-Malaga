using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonInicio : MonoBehaviour
{
    public void CargarTexto()
    {
        SceneManager.LoadScene("Scenes/TextoInicio");
    }
}
