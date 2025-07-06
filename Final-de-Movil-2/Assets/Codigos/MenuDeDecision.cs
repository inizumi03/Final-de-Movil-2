using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDeDecision : MonoBehaviour
{
    public GameObject menuDecisiones;  // El menú de decisiones que contiene los botones
    public Camino camino;              // Referencia al script Camino para manipular el movimiento del jugador

    // Este método se llamará para mostrar el menú de decisiones
    public void MostrarMenuDecisiones()
    {
        menuDecisiones.SetActive(true);  // Activar el menú de decisiones
    }

    // Este método se llama cuando el jugador decide seguir el camino actual
    public void OpcionSeguirCamino()
    {
        camino.SeguirCamino();          // Llama a la función del script Camino para seguir el camino actual
        menuDecisiones.SetActive(false); // Ocultar el menú
    }

    // Este método se llama cuando el jugador decide cambiar el camino
    public void OpcionCambiarCamino()
    {
        camino.CambiarCamino();         // Llama a la función del script Camino para cambiar de camino
        menuDecisiones.SetActive(false); // Ocultar el menú
    }

}
