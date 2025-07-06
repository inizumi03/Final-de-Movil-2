using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino : MonoBehaviour
{
    public List<Transform> caminoA;       // Lista de puntos para el camino A
    public List<Transform> caminoB;       // Lista de puntos para el camino B
    public List<Transform> caminoC;       // Lista de puntos para el camino C
    public GameObject menuDecisiones;     // El menú de decisiones
    public Transform puntoFinal;          // El punto final común para todos los caminos
    public Transform jugador;             // El jugador a mover
    public MenuDeDecision menuDeDecision; // Referencia al script MenuDeDecision
    public MenuFinal menuFinal;
    private List<Transform> caminoActual; // Lista que determina el camino por el que va el jugador
    private int indiceActual = 0;         // Índice para recorrer el camino

    private bool estaEnDesviacion = false; // Verifica si el jugador está en una desviación
    private bool cambiarCamino = false;   // Verifica si el jugador decide cambiar el camino
    private bool estaQuieto = false;      // Verifica si el jugador está quieto esperando una decisión

    private Rigidbody rb;                 // Referencia al Rigidbody del jugador para detenerlo

    void Start()
    {
        caminoActual = caminoA;  // El camino por defecto es el A
        menuDecisiones.SetActive(false); // Desactiva el menú de decisiones al inicio
        rb = jugador.GetComponent<Rigidbody>(); // Obtenemos la referencia al Rigidbody
        rb.isKinematic = false; // Aseguramos que el Rigidbody esté activo al principio
    }

    void Update()
    {
        // Si el jugador no está quieto, lo movemos
        if (!estaQuieto)
        {
            MoverJugador();
        }
    }

    // Función para mover al jugador a lo largo del camino actual
    void MoverJugador()
    {
        if (indiceActual < caminoActual.Count)
        {
            // Mueve al jugador hacia el siguiente punto del camino
            jugador.position = Vector3.MoveTowards(jugador.position, caminoActual[indiceActual].position, 5f * Time.deltaTime);

            // Si el jugador llega al punto actual, mueve al siguiente
            if (Vector3.Distance(jugador.position, caminoActual[indiceActual].position) < 0.1f)
            {
                indiceActual++;
            }
        }
        else
        {
            // Una vez que llega al final del camino, activa el menú final
            if (menuFinal != null)
                menuFinal.MostrarMenuFinal(); // Llama al método para mostrar el menú final
        }
    }

    // Esta función se llama cuando el jugador atraviesa un "lugar de desviación"
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador ha llegado a un punto de desviación
        if (other.CompareTag("Desviacion"))
        {
            LugarDeDesviacion(); // Activa el menú de decisiones cuando el jugador llega a un punto de desviación
        }
    }

    // Función para activar el menú de decisiones cuando el jugador llega a un "lugar de desviación"
    public void LugarDeDesviacion()
    {
        if (!estaEnDesviacion)
        {
            estaEnDesviacion = true;
            estaQuieto = true;  // Detenemos al jugador
            rb.velocity = Vector3.zero; // Detenemos el movimiento del jugador
            menuDeDecision.MostrarMenuDecisiones();  // Activa el menú de decisiones
        }
    }

    // El jugador decide seguir el camino actual
    public void SeguirCamino()
    {
        menuDecisiones.SetActive(false);
        estaEnDesviacion = false;
        estaQuieto = false;  // El jugador puede seguir moviéndose
    }

    // El jugador decide cambiar de camino al más cercano
    public void CambiarCamino()
    {
        // Lógica para determinar el camino más cercano
        float distanciaA = Vector3.Distance(jugador.position, caminoA[0].position);
        float distanciaB = Vector3.Distance(jugador.position, caminoB[0].position);
        float distanciaC = Vector3.Distance(jugador.position, caminoC[0].position);

        // Compara las distancias y asigna el camino más cercano
        if (distanciaA < distanciaB && distanciaA < distanciaC)
        {
            caminoActual = caminoA;  // El camino A es el más cercano
        }
        else if (distanciaB < distanciaA && distanciaB < distanciaC)
        {
            caminoActual = caminoB;  // El camino B es el más cercano
        }
        else
        {
            caminoActual = caminoC;  // El camino C es el más cercano
        }

        indiceActual = 0;  // Reinicia el índice para empezar desde el principio del nuevo camino
        menuDecisiones.SetActive(false);  // Desactiva el menú de decisiones
        estaEnDesviacion = false;  // Desactiva el estado de desviación
        estaQuieto = false;  // El jugador puede seguir moviéndose
    }
}
