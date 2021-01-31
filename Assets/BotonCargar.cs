using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BotonCargar : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Scenes/Gameplay");
    }
}
