using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHaciaDestino : MonoBehaviour
{
    [Header("Destino y velocidad")]
    public Transform puntoDestino;
    public float velocidad = 2f;
    public float distanciaMinima = 0.1f;

    [Header("Tiempo de espera antes de moverse")]
    public float tiempoDeEspera = 0f;

    private bool haLlegado = false;
    private float tiempoTranscurrido = 0f;
    private bool movimientoIniciado = false;

    void Update()
    {
        if (haLlegado || puntoDestino == null) return;

        // Esperar antes de comenzar el movimiento
        if (!movimientoIniciado)
        {
            tiempoTranscurrido += Time.deltaTime;
            if (tiempoTranscurrido >= tiempoDeEspera)
            {
                movimientoIniciado = true;
            }
            else
            {
                return;
            }
        }

        // Dirección hacia el destino
        Vector3 direccion = puntoDestino.position - transform.position;

        if (direccion.magnitude <= distanciaMinima)
        {
            haLlegado = true;
            return;
        }

        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }
}
