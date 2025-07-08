using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeslizarParaDesplazar : MonoBehaviour
{
    [Header("Referencias")]
    public ScrollRect scrollRect;  // Referencia al ScrollRect que contiene el GridLayoutGroup
    public float velocidadDeslizar = 1f;  // Velocidad con la que se desplaza el contenido

    private Vector2 touchStartPos;  // Posición inicial del toque
    private Vector2 touchEndPos;    // Posición final del toque
    private bool deslizando = false; // Indica si se está deslizando

    void Update()
    {
        // Comprobar si hay un toque en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Obtener el primer toque

            if (touch.phase == TouchPhase.Began)
            {
                // Registrar la posición inicial del toque
                touchStartPos = touch.position;
                deslizando = true;
            }

            if (touch.phase == TouchPhase.Moved && deslizando)
            {
                // Registrar la posición final del toque mientras se mueve
                touchEndPos = touch.position;

                // Calcular la dirección del deslizamiento (arriba o abajo)
                float distanciaDeslizada = touchStartPos.y - touchEndPos.y;

                // Si se desliza hacia arriba, desplazamos el contenido
                if (distanciaDeslizada > 0)
                {
                    DesplazarContenido(distanciaDeslizada * velocidadDeslizar);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Finalizar el deslizamiento cuando se levanta el dedo
                deslizando = false;
            }
        }
    }

    // Método para desplazar el contenido en el ScrollRect
    private void DesplazarContenido(float distancia)
    {
        // Asegurarnos de que el desplazamiento sea solo en el eje Y (vertical)
        Vector2 nuevaPosicion = scrollRect.content.anchoredPosition;
        nuevaPosicion.y += distancia;

        // Aplicar el nuevo valor de la posición
        scrollRect.content.anchoredPosition = nuevaPosicion;
    }
}
