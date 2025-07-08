using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoEspecial : MonoBehaviour
{
    [Header("Configuración de penalización")]
    public int puntosARestar = 5;  // Los puntos que se restarán al jugador
    public float rangoDeteccion = 10f;  // Rango de detección alrededor del objeto

    private Fotografo fotografo;  // Referencia al script del fotógrafo
    private bool yaFotografiado = false; // Variable para asegurar que el objeto solo resta puntos una vez

    void Start()
    {
        // Obtener referencia al script Fotografo en el jugador (o en el objeto correspondiente)
        fotografo = FindObjectOfType<Fotografo>();
    }

    void Update()
    {
        // Solo permitir que el objeto reste puntos una vez
        if (!yaFotografiado)
        {
            // Detectar si el jugador está cerca del objeto especial
            if (fotografo != null && Vector3.Distance(transform.position, fotografo.transform.position) < rangoDeteccion)
            {
                // Si está cerca y el objeto aún no ha sido fotografiado, restar puntos
                fotografo.RestarPuntos(puntosARestar);
                yaFotografiado = true; // Marcar que el objeto ha sido fotografiado
                Debug.Log("Jugador cerca de un objeto especial, puntos restados.");
            }
        }
    }

    // Gizmo para visualizar el rango de detección en la escena
    void OnDrawGizmosSelected()
    {
        // Establecer color para el gizmo
        Gizmos.color = Color.red;
        // Dibujar una esfera que representa el rango de detección
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
