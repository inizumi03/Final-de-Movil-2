using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    // Lista de caminos, donde cada camino es una lista de puntos (Transform)
    public List<List<Transform>> caminos;
    private int indiceActual = 0;              // Índice del punto actual dentro del camino seleccionado
    private bool enPuntoDeDecision = false;    // Bandera para saber si estamos en un punto de decisión

    [Header("Referencia al Menú de Decisión")]
    public MenuDeDecision menuDeDecision;      // Referencia al menú de decisiones
    [Header("Referencia al Menú Final")]
    public MenuFinal menuFinal;                // Menú final que se muestra al completar el recorrido

    public List<Transform> caminoActual;      // El camino actual que está siguiendo el jugador

    // Lista de puntos de decisión (Transform)
    [Header("Puntos de Decisión")]
    public List<Transform> puntosDeDecision;   // Lista de puntos donde el jugador puede cambiar de camino

    void Start()
    {
        // Empieza con el primer camino
        if (caminos.Count > 0)
            caminoActual = caminos[0];  // El primer camino es el que se sigue inicialmente
    }

    void Update()
    {
        if (caminoActual.Count == 0) return;

        // Si estamos en un punto de decisión, se abre el menú de elección de camino
        if (enPuntoDeDecision)
        {
            if (menuDeDecision != null)
                menuDeDecision.MostrarMenuDecisiones();

            return; // Detenemos el movimiento mientras el menú está activo
        }

        // Movimiento entre los puntos del camino
        Transform destino = caminoActual[indiceActual];
        Vector3 posicionActual = transform.position;
        Vector3 posicionObjetivo = destino.position;

        Vector3 direccion = posicionObjetivo - posicionActual;

        if (Mathf.Abs(direccion.y) < 0.05f) direccion.y = 0;

        if (direccion != Vector3.zero)
        {
            // Gira suavemente hacia la dirección de movimiento
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }

        // Movimiento
        Vector3 movimiento = direccion.normalized * 3f * Time.deltaTime;
        if (movimiento.magnitude > direccion.magnitude)
        {
            transform.position = posicionObjetivo;
        }
        else
        {
            transform.position += movimiento;
        }

        // Verifica si llegó
        if (Vector3.Distance(transform.position, posicionObjetivo) <= 0.1f)
        {
            indiceActual++;

            // Si llegamos a un punto de decisión
            if (puntosDeDecision.Contains(destino))
            {
                enPuntoDeDecision = true; // Detenemos el movimiento y mostramos el menú
                return; // Esperamos que el jugador decida
            }

            // Si llegamos al final del camino
            if (indiceActual >= caminoActual.Count)
            {
                if (menuFinal != null)
                    menuFinal.MostrarMenuFinal(); // Mostrar el menú final
            }
        }
    }

    // Método para tomar una decisión cuando el jugador elige seguir o cambiar el camino
    public void TomarDecision(bool seguirCamino)
    {
        if (seguirCamino)
        {
            enPuntoDeDecision = false; // Continuar el camino actual
        }
        else
        {
            // Cambiar a otro camino (aquí puedes definir la lógica para cambiar el camino)
            indiceActual = 0; // Reiniciar el camino actual
            caminoActual = caminos[1]; // Cambiar al segundo camino de la lista
            enPuntoDeDecision = false; // Continuar con el nuevo camino
        }
    }
}
