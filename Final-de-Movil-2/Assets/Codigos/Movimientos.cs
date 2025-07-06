using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movimientos : MonoBehaviour
{
    // Lista pública de puntos del camino que el jugador sigue (cada camino es un conjunto de puntos)
    public List<Transform> caminoActual;
    private int indiceActual = 0;              // Índice del punto actual dentro del camino seleccionado
    private bool enPuntoDeDecision = false;    // Bandera para saber si estamos en un punto de decisión

    [Header("Referencia al Menú de Decisión")]
    public MenuDeDecision menuDeDecision;      // Referencia al menú de decisiones
    [Header("Referencia al Menú Final")]
    public MenuFinal menuFinal;                // Menú final que se muestra al completar el recorrido

    private NavMeshAgent agente;               // Referencia al NavMeshAgent para mover al jugador

    // Lista de puntos de decisión (Transform)
    [Header("Puntos de Decisión")]
    public List<Transform> puntosDeDecision;   // Lista de puntos donde el jugador puede cambiar de camino

    void Start()
    {
        // Obtener el NavMeshAgent
        agente = GetComponent<NavMeshAgent>();

        // Empieza con el primer camino
        if (caminoActual.Count > 0)
            agente.SetDestination(caminoActual[0].position);  // Establecer destino inicial
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

        // Si llegamos a un punto de destino
        if (Vector3.Distance(transform.position, posicionObjetivo) <= 0.1f)
        {
            indiceActual++;

            // Si llegamos a un punto de decisión
            if (puntosDeDecision.Contains(destino))  // Aquí se verifica si el destino es un punto de decisión
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
            else
            {
                // Continuar hacia el siguiente punto
                if (agente != null)
                    agente.SetDestination(caminoActual[indiceActual].position);  // Continuar movimiento
            }
        }
    }

    // Método para tomar una decisión cuando el jugador elige seguir o cambiar el camino
    public void TomarDecision(bool seguirCamino)
    {
        if (seguirCamino)
        {
            enPuntoDeDecision = false; // Continuar el camino actual
            if (agente != null)
                agente.SetDestination(caminoActual[indiceActual].position); // Continuar el movimiento
        }
        else
        {
            // Cambiar a otro camino (por ejemplo, a otro conjunto de puntos en el Mesh)
            indiceActual = 0; // Reiniciar el camino actual
            // Cambiar al siguiente camino
            // En este caso, podemos cambiar el camino a otro conjunto de puntos.
            caminoActual = new List<Transform>();  // Crear una nueva lista de puntos de destino
            // Aquí puedes agregar el nuevo camino, por ejemplo, con puntos de decisión diferentes
            enPuntoDeDecision = false; // Continuar con el nuevo camino
            if (agente != null)
                agente.SetDestination(caminoActual[indiceActual].position); // Reiniciar movimiento
        }
    }
}
