using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        // Detectar si se presionó la tecla 'Q'
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Cambiar a la escena llamada "Fin"
            SceneManager.LoadScene("Fin");
        }
    }
}
