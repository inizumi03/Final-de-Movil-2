using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AnimacionConPuntos
{
    public string nombreAnimacion;
    public int puntos;
    public float duracion;
}
public class ComportamientoHeroe : MonoBehaviour
{
    [Header("Animaciones configurables")]
    public List<AnimacionConPuntos> animaciones = new List<AnimacionConPuntos>();

    [Header("Movimiento")]
    public bool moverHeroe = false;
    public List<Transform> puntosDestino;
    public float velocidadMovimiento = 2f;
    public string animacionMovimiento = "Caminar";
    public int puntosAnimacionMovimiento = 5;

    [Header("Zona de activación (Gizmos)")]
    public Vector3 centroZona = Vector3.zero;
    public float radioZona = 5f;
    public Color colorZona = Color.green;

    private Transform jugador;
    private Animator animator;
    private int indiceDestino = 0;
    private bool movimientoActivo = false;
    private bool animacionPorFrecuenciaActiva = false;

    private bool heroeFotografiado = false;
    public int GetPuntosActuales() => heroeFotografiado ? 0 : puntosActuales;
    private int puntosActuales = 0;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        if (!moverHeroe)
            StartCoroutine(ReproducirAnimacionesPorFrecuencia());
    }

    void Update()
    {
        if (moverHeroe && !movimientoActivo && EstaJugadorEnZona())
        {
            movimientoActivo = true;
            StopAllCoroutines(); // Detiene animaciones por frecuencia si estaban activas
            animator.Play(animacionMovimiento);
            puntosActuales += puntosAnimacionMovimiento;
        }

        if (movimientoActivo && indiceDestino < puntosDestino.Count)
        {
            Vector3 destino = puntosDestino[indiceDestino].position;
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidadMovimiento * Time.deltaTime);

            if (Vector3.Distance(transform.position, destino) < 0.1f)
            {
                indiceDestino++;
                if (indiceDestino >= puntosDestino.Count)
                {
                    animator.Play("Idle");
                }
            }
        }
    }

    bool EstaJugadorEnZona()
    {
        if (jugador == null) return false;
        return Vector3.Distance(jugador.position, transform.position + centroZona) <= radioZona;
    }

    IEnumerator ReproducirAnimacionesPorFrecuencia()
    {
        animacionPorFrecuenciaActiva = true;
        while (animacionPorFrecuenciaActiva && animaciones.Count > 0)
        {
            int indice = Random.Range(0, animaciones.Count);
            AnimacionConPuntos anim = animaciones[indice];

            animator.Play(anim.nombreAnimacion);
            puntosActuales += anim.puntos;

            yield return new WaitForSeconds(anim.duracion);
        }
    }

    public void MarcarComoFotografiado()
    {
        heroeFotografiado = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = colorZona;
        Gizmos.DrawWireSphere(transform.position + centroZona, radioZona);
    }
}
