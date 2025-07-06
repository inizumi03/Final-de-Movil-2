using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarCamara : MonoBehaviour
{
    public Camera camPrincipal;  // Cámara principal
    public Camera camSecundaria; // Cámara secundaria

    private bool enModoCompleto = false; // Controla si la cámara secundaria está en modo completo o no

    void Start()
    {
        // Asegúrate de que la cámara principal esté siempre activada
        camPrincipal.enabled = true;

        // Configura la cámara secundaria en tamaño pequeño inicialmente
        camSecundaria.rect = new Rect(0.75f, 0f, 0.25f, 0.25f); // Cámara pequeña en la esquina inferior derecha

        // Iniciar el parpadeo de la cámara secundaria
        StartCoroutine(ParpadeoCamaraSecundaria());
    }

    void Update()
    {
        // Detectar si el jugador toca la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = touch.position;

                // Verificar si el toque está dentro de la zona de la cámara secundaria
                if (EsToqueEnZona(touchPos))
                {
                    CambiarTamañoCamaraSecundaria();
                }
            }
        }
    }

    bool EsToqueEnZona(Vector2 touchPos)
    {
        // Verifica si el toque está dentro del área de la cámara secundaria.
        // Las coordenadas de la zona de la cámara secundaria son el 25% de la pantalla en la esquina inferior derecha
        Rect zonaCamaraSecundaria = new Rect(Screen.width * 0.75f, 0, Screen.width * 0.25f, Screen.height * 0.25f);
        return zonaCamaraSecundaria.Contains(touchPos);
    }

    void CambiarTamañoCamaraSecundaria()
    {
        // Si está en tamaño pequeño, cambiar a tamaño completo, si está en tamaño completo, vuelve a tamaño pequeño
        if (enModoCompleto)
        {
            // Regresar a tamaño pequeño
            camSecundaria.rect = new Rect(0.75f, 0f, 0.25f, 0.25f); // Cámara pequeña
            camPrincipal.enabled = true; // Activar la cámara principal
        }
        else
        {
            // Cambiar a tamaño completo
            camSecundaria.rect = new Rect(0f, 0f, 1f, 1f); // Cámara completa
            camPrincipal.enabled = false; // Desactivar la cámara principal
        }

        // Alternar el modo
        enModoCompleto = !enModoCompleto;
    }

    // Corutina para crear el efecto de parpadeo de la cámara secundaria
    IEnumerator ParpadeoCamaraSecundaria()
    {
        // Realizar el parpadeo varias veces al inicio
        for (int i = 0; i < 5; i++)  // Puedes ajustar el número de parpadeos (5 es solo un ejemplo)
        {
            camSecundaria.enabled = true;  // Activar la cámara secundaria
            yield return new WaitForSeconds(0.1f);  // Esperar 0.1 segundos (ajusta según sea necesario)

            camSecundaria.enabled = false; // Desactivar la cámara secundaria
            yield return new WaitForSeconds(0.1f);  // Esperar 0.1 segundos antes de activarla nuevamente
        }

        // Después del parpadeo, dejar la cámara secundaria activada
        camSecundaria.enabled = true;  // Mantener la cámara secundaria activada después del parpadeo
    }
}
