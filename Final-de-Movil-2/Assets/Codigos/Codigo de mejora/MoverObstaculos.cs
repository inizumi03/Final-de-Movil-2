using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObstaculos : MonoBehaviour
{
    [Header("Objetivos")]
    public Transform puntoInicio;   // El punto de inicio (posicion inicial)
    public Transform puntoDestino;  // El punto de destino al que el objeto se moverá

    [Header("Velocidad de Movimiento")]
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del objeto

    private bool enDestino = false;  // Indica si el objeto ha llegado al destino

    void Start()
    {
        // Aseguramos que el objeto comience en la posición inicial
        transform.position = puntoInicio.position;
    }

    void Update()
    {
        if (!enDestino)
        {
            MoverHaciaDestino();
        }
        else
        {
            // Si el objeto llegó al destino, resetea la posición a puntoInicio
            ReseteaPosicion();
        }
    }

    // Método para mover el objeto hacia el destino
    void MoverHaciaDestino()
    {
        // Mover el objeto hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino.position, velocidadMovimiento * Time.deltaTime);

        // Si el objeto ha llegado al destino, cambiar estado
        if (transform.position == puntoDestino.position)
        {
            enDestino = true;
        }
    }

    // Método para resetear la posición del objeto a puntoInicio
    void ReseteaPosicion()
    {
        // Espera un momento antes de reseteo (opcional)
        // Puedes ajustar el tiempo que esperará antes de volver al inicio
        Invoke("VolverAlInicio", 1f);
    }

    // Resetea la posición al punto de inicio
    void VolverAlInicio()
    {
        transform.position = puntoInicio.position;
        enDestino = false; // Permitir que el objeto vuelva a moverse
    }
}
