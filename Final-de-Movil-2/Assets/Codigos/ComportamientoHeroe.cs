using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimacionConPuntos
{
    public string nombreAnimacion;
    public int puntaje;
}
public class ComportamientoHeroe : MonoBehaviour
{
    [Header("Animaciones y puntos")]
    public List<string> nombresAnimaciones;    // Idle 1, Idle 2, Animacion movimiento, Animacion al salir
    public List<int> puntosAnimaciones;        // Puntos asociados a cada animacion (mismo orden)

    [Header("Movimiento")]
    public bool activarMovimiento = true;
    public Transform[] puntosMovimiento;
    public float velocidad = 3f;
    public Color colorGizmo = Color.cyan;
    public float radioZona = 5f;               // Radio de la zona donde el jugador activa movimiento

    private int indiceActual = 0;
    private bool moviendose = false;
    private Animator animator;

    // Referencia al jugador para medir distancia (o usar trigger colider)
    public Transform jugador;

    // Sistema tiempo para cambio de animaciones idle
    private int indiceIdle = 0;
    public float tiempoCambioIdle = 3f;
    private float contadorTiempoIdle = 0f;

    private bool jugadorEnZona = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        contadorTiempoIdle = tiempoCambioIdle;
    }

    void Update()
    {
        if (activarMovimiento && jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);
            bool entroZona = distancia <= radioZona;

            if (entroZona && !jugadorEnZona)
            {
                jugadorEnZona = true;
                indiceActual = 0;
                moviendose = true;
                animator.Play(nombresAnimaciones.Count > 2 ? nombresAnimaciones[2] : "Move");
            }
            else if (!entroZona && jugadorEnZona)
            {
                jugadorEnZona = false;
                moviendose = false;
                animator.Play(nombresAnimaciones.Count > 3 ? nombresAnimaciones[3] : nombresAnimaciones[0]);
            }
        }

        if (moviendose)
        {
            MoverHeroe();
        }
        else
        {
            // Cambio de animaciones idle según tiempo
            contadorTiempoIdle -= Time.deltaTime;
            if (contadorTiempoIdle <= 0f)
            {
                indiceIdle = (indiceIdle + 1) % 2; // Cambia entre 0 y 1 para idle 1 y idle 2
                if (animator != null && nombresAnimaciones.Count > indiceIdle)
                    animator.Play(nombresAnimaciones[indiceIdle]);
                contadorTiempoIdle = tiempoCambioIdle;
            }
        }
    }

    void MoverHeroe()
    {
        if (puntosMovimiento.Length == 0) return;

        Transform destino = puntosMovimiento[indiceActual];
        Vector3 direccion = destino.position - transform.position;
        direccion.y = 0;

        if (direccion.magnitude < 0.1f)
        {
            indiceActual++;
            if (indiceActual >= puntosMovimiento.Length)
            {
                // Llegó al final, detener movimiento y poner animacion al salir
                moviendose = false;
                indiceActual = 0;
                if (animator != null && nombresAnimaciones.Count > 3)
                    animator.Play(nombresAnimaciones[3]);
                else if (animator != null)
                    animator.Play(nombresAnimaciones[0]); // idle 1
            }
        }
        else
        {
            transform.position += direccion.normalized * velocidad * Time.deltaTime;
            if (animator != null && nombresAnimaciones.Count > 2)
                animator.Play(nombresAnimaciones[2]);
        }
    }

    public int ObtenerPuntosAnimacionActual()
    {
        if (moviendose && puntosAnimaciones.Count > 2)
            return puntosAnimaciones[2];
        else if (!moviendose && puntosAnimaciones.Count > 0)
            return puntosAnimaciones[indiceIdle];
        return 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = colorGizmo;
        Gizmos.DrawWireSphere(transform.position, radioZona);
    }
}
