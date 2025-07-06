using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDeDecision : MonoBehaviour
{
    public GameObject menuDecisiones;  // El menú que tiene las opciones de seguir o cambiar de camino
    public Movimientos movimientos;    // Referencia al script de Movimientos

    public void MostrarMenuDecisiones()
    {
        menuDecisiones.SetActive(true);  // Mostrar el menú de decisiones
    }

    public void OpcionSeguirCamino()
    {
        movimientos.TomarDecision(true);  // Continuar en el camino actual
        menuDecisiones.SetActive(false);  // Ocultar el menú
    }

    public void OpcionCambiarCamino()
    {
        movimientos.TomarDecision(false);  // Cambiar de camino
        menuDecisiones.SetActive(false);  // Ocultar el menú
    }

}
