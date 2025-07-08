using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeslizarParaDesactivarCanvas : MonoBehaviour
{
    [Header("Canvases para desactivar")]
    public List<GameObject> canvasesImagenes = new List<GameObject>();  // Lista de canvases con las imágenes a desactivar

    private Vector2 touchStartPos;  // Posición inicial del toque
    private Vector2 touchEndPos;    // Posición final del toque

    void Start()
    {
        // Asegurarse de que todos los canvases estén desactivados al principio
        foreach (GameObject canvas in canvasesImagenes)
        {
            canvas.SetActive(false);
        }
    }

    void Update()
    {
        // Comprobar si hay un toque en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Obtener el primer toque

            if (touch.phase == TouchPhase.Began)
            {
                // Registrar la posición del inicio del toque
                touchStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Registrar la posición final del toque
                touchEndPos = touch.position;

                // Comprobar si el toque fue de izquierda a derecha
                if (touchEndPos.x > touchStartPos.x)
                {
                    // Si el toque fue de izquierda a derecha, desactivar los canvases
                    DesactivarCanvases();
                }
            }
        }
    }

    // Método para activar los canvases
    public void ActivarCanvases()
    {
        foreach (GameObject canvas in canvasesImagenes)
        {
            canvas.SetActive(true);  // Activar el canvas
        }
    }

    // Método para desactivar los canvases
    void DesactivarCanvases()
    {
        foreach (GameObject canvas in canvasesImagenes)
        {
            canvas.SetActive(false);  // Desactivar el canvas
        }
    }

}
