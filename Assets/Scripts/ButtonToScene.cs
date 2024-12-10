using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToScene : MonoBehaviour
{
    // Nombre de la escena del juego
    [SerializeField] private string juegoSceneName = "Juego";

    // Método que será llamado al presionar el botón
    public void LoadGameScene()
    {
        SceneManager.LoadScene(juegoSceneName);
    }
}

