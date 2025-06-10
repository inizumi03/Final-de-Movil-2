using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoHeroe : MonoBehaviour
{
    [Header("Animaciones")]
    public Animator animador;
    public string animacionIdle = "Idle";
    public string animacionPrimera = "Reaccion";
    public string animacionSegunda = "Especial";

    [Header("Puntos por animación")]
    public int puntosPrimeraAnimacion = 10;
    public int puntosSegundaAnimacion = 20;

    [Header("Distancias de activación")]
    public Transform jugador;
    public float distanciaReaccion = 10f;
    public float distanciaEspecial = 4f;

    [Header("Movimiento entre puntos")]
    public bool usarMovimiento = true;
    public Transform puntoA;
    public Transform puntoB;
    public float velocidadMovimiento = 2f;

    [HideInInspector] public bool fueFotografiado = false;
    [HideInInspector] public int puntosPorFoto = 0;

    private bool reaccionActiva = false;
    private bool especialActiva = false;
    private bool estabaEnZonaEspecial = false;
    private bool movimientoActivo = false;
    private Vector3 destinoActual;

    void Start()
    {
        if (animador != null)
            animador.Play(animacionIdle);

        if (puntoA != null)
            destinoActual = puntoA.position;
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        // Activar animación de reacción y (opcionalmente) movimiento
        if (distancia <= distanciaReaccion && !reaccionActiva)
        {
            ActivarPrimeraAnimacion();
        }

        // Detectar salida de la zona especial para activar la segunda animación
        if (distancia <= distanciaEspecial)
        {
            estabaEnZonaEspecial = true;
        }
        else
        {
            if (estabaEnZonaEspecial && !especialActiva)
            {
                ActivarSegundaAnimacion();
                estabaEnZonaEspecial = false;
            }
        }

        if (movimientoActivo && usarMovimiento && puntoA != null && puntoB != null)
        {
            MoverEntrePuntos();
        }
    }

    void ActivarPrimeraAnimacion()
    {
        reaccionActiva = true;

        if (usarMovimiento)
            movimientoActivo = true;

        if (animador != null)
            animador.Play(animacionPrimera);

        puntosPorFoto = puntosPrimeraAnimacion;
    }

    void ActivarSegundaAnimacion()
    {
        especialActiva = true;
        movimientoActivo = false;

        if (animador != null)
            animador.Play(animacionSegunda);

        puntosPorFoto = puntosSegundaAnimacion;
    }

    void MoverEntrePuntos()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadMovimiento * Time.deltaTime);

        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = (destinoActual == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaReaccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaEspecial);
    }
}
