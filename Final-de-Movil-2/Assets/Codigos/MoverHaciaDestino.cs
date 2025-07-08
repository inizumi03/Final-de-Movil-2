using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHaciaDestino : MonoBehaviour
{
    [Header("Destino y velocidad")]
    public Transform puntoDestino;
    public float velocidad = 2f;
    public float distanciaMinima = 0.1f;

    [Header("Collider de activación")]
    public Collider zonaDeActivacion; // El collider que activa el movimiento

    private bool haLlegado = false;
    private bool movimientoIniciado = false;

    void Start()
    {
        // Asegurarse de que el movimiento no comience al principio
        zonaDeActivacion.enabled = true;
    }

    void Update()
    {
        if (haLlegado || puntoDestino == null) return;

        // Solo empezar el movimiento si ha sido activado por el trigger
        if (movimientoIniciado)
        {
            // Dirección hacia el destino
            Vector3 direccion = puntoDestino.position - transform.position;

            // Verificar si el objeto ha llegado al destino
            if (direccion.magnitude <= distanciaMinima)
            {
                haLlegado = true;
                return;
            }

            // Mover el objeto hacia el destino
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
        }
    }

    // Método que se llama cuando el jugador entra en el collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            // Desactivamos la zona de activación para que no vuelva a ser activada
            zonaDeActivacion.enabled = false;

            // Iniciar el movimiento inmediatamente
            movimientoIniciado = true;
        }
    }
}
