using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mecanicaDeActivar : MonoBehaviour
{
    public Animator animadorHeroe;  // Referencia al Animator del héroe
    public string triggerAnimacionA = "AnimacionA";  // Nombre del Trigger para animación A
    public string triggerAnimacionB = "AnimacionB";  // Nombre del Trigger para animación B
    public string triggerIdle = "Idle";  // Nombre del Trigger para animación Idle

    private bool jugadorDentro = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            Debug.Log("Empieza las animaciones");  // Mensaje en consola
            ActivarAnimacion();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            DetenerAnimacion();
        }
    }

    void ActivarAnimacion()
    {
        if (jugadorDentro && animadorHeroe != null)
        {
            animadorHeroe.SetTrigger(triggerAnimacionA);  // Activa AnimacionA
            // Aquí agregas el código para esperar o transitar a AnimacionB después de AnimacionA.
            Invoke("ActivarAnimacionB", 2f); // Cambiar a AnimacionB después de 2 segundos (ajusta el tiempo si es necesario)
        }
    }


    void ActivarAnimacionB()
    {
        if (jugadorDentro && animadorHeroe != null)
        {
            animadorHeroe.SetTrigger(triggerAnimacionB);  // Activa AnimacionB
        }
    }



    void DetenerAnimacion()
    {
        if (animadorHeroe != null)
        {
            animadorHeroe.SetTrigger(triggerIdle);  // Vuelve a Idle
        }
    }
}
