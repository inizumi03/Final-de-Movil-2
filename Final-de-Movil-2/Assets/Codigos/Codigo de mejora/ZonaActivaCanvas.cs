using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaActivaCanvas : MonoBehaviour
{
    [Header("Canvas para activar")]
    public GameObject canvasImagenes;  // El canvas con las imágenes a activar

    void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en la zona, activamos el canvas
        if (other.CompareTag("Player"))  // Asegúrate de que el jugador tenga el tag "Player"
        {
            if (canvasImagenes != null)
            {
                canvasImagenes.SetActive(true);  // Activar el canvas
            }
        }
    }
}
