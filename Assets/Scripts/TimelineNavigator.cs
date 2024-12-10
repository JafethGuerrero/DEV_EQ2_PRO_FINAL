using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimelineNavigator : MonoBehaviour
{
    public GameObject marco;               // Marco que siempre está visible
    public GameObject[] imagenesConTexto;  // Array de imágenes con TextMeshPro
    public float transitionDuration = 0.5f; // Duración de la transición (en segundos)
    private int indiceActual = 0;          // Índice del elemento activo actualmente
    private bool isTransitioning = false;  // Para evitar múltiples transiciones simultáneas

    void Start()
    {
        // Inicializar visibilidad
        foreach (GameObject obj in imagenesConTexto)
        {
            SetCanvasGroupAlpha(obj, 0f); // Ocultar todos los elementos
        }
        SetCanvasGroupAlpha(imagenesConTexto[indiceActual], 1f); // Mostrar el primero
    }

    void Update()
    {
        // Navegar a la derecha (tecla D)
        if (Input.GetKeyDown(KeyCode.D) && !isTransitioning)
        {
            Avanzar();
        }

        // Navegar a la izquierda (tecla A)
        if (Input.GetKeyDown(KeyCode.A) && !isTransitioning)
        {
            Retroceder();
        }
    }

    void Avanzar()
    {
        int siguienteIndice = (indiceActual + 1) % imagenesConTexto.Length;
        StartCoroutine(Transicion(imagenesConTexto[indiceActual], imagenesConTexto[siguienteIndice]));
        indiceActual = siguienteIndice;
    }

    void Retroceder()
    {
        int siguienteIndice = (indiceActual - 1 + imagenesConTexto.Length) % imagenesConTexto.Length;
        StartCoroutine(Transicion(imagenesConTexto[indiceActual], imagenesConTexto[siguienteIndice]));
        indiceActual = siguienteIndice;
    }

    IEnumerator Transicion(GameObject actual, GameObject siguiente)
    {
        isTransitioning = true;

        // Configurar inicio de la transición
        CanvasGroup actualCanvas = GetOrAddCanvasGroup(actual);
        CanvasGroup siguienteCanvas = GetOrAddCanvasGroup(siguiente);
        siguiente.SetActive(true);

        float t = 0f;

        // Desvanecer actual y mostrar siguiente
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / transitionDuration); // Actual se desvanece
            actualCanvas.alpha = alpha;
            siguienteCanvas.alpha = 1f - alpha; // Siguiente aparece
            yield return null;
        }

        // Finalizar transición
        actualCanvas.alpha = 0f;
        actual.SetActive(false);
        siguienteCanvas.alpha = 1f;

        isTransitioning = false;
    }

    CanvasGroup GetOrAddCanvasGroup(GameObject obj)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        if (group == null)
        {
            group = obj.AddComponent<CanvasGroup>();
        }
        return group;
    }

    void SetCanvasGroupAlpha(GameObject obj, float alpha)
    {
        CanvasGroup group = GetOrAddCanvasGroup(obj);
        group.alpha = alpha;
        obj.SetActive(alpha > 0);
    }
}
