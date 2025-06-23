using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaDePeleas : MonoBehaviour
{
    [System.Serializable]
    public class AnimacionConPuntos
    {
        public string nombreAnimacion; // Nombre exacto del estado en Animator
        public int puntos;
    }

    public List<AnimacionConPuntos> animaciones = new List<AnimacionConPuntos>();
    public Animator animator;
    public HeroeFotografiable heroeFotografiable;

    public float tiempoEntreAnimaciones = 0.5f;
    public int puntosIdle = 10;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (heroeFotografiable == null) heroeFotografiable = GetComponent<HeroeFotografiable>();
        StartCoroutine(CicloAnimaciones());
    }

    IEnumerator CicloAnimaciones()
    {
        while (true)
        {
            // Idle
            animator.SetInteger("Estado", 0);
            yield return EsperarAnimacion("Idle");
            heroeFotografiable.puntosPorFoto = puntosIdle;
            yield return new WaitForSeconds(tiempoEntreAnimaciones);

            // Defensa (asumimos animaciones[0])
            animator.SetInteger("Estado", 1);
            yield return EsperarAnimacion(animaciones[0].nombreAnimacion);
            heroeFotografiable.puntosPorFoto = animaciones[0].puntos;
            yield return new WaitForSeconds(tiempoEntreAnimaciones);

            // Golpe (asumimos animaciones[1])
            animator.SetInteger("Estado", 2);
            yield return EsperarAnimacion(animaciones[1].nombreAnimacion);
            heroeFotografiable.puntosPorFoto = animaciones[1].puntos;
            yield return new WaitForSeconds(tiempoEntreAnimaciones);
        }
    }

    IEnumerator EsperarAnimacion(string nombreAnimacion)
    {
        // Espera a que la animación actual sea la que queremos y que termine (normalizedTime >=1)
        // Además chequea que no esté en transición para evitar saltos

        while (true)
        {
            AnimatorStateInfo estado = animator.GetCurrentAnimatorStateInfo(0);

            if (estado.IsName(nombreAnimacion) && estado.normalizedTime >= 1f && !animator.IsInTransition(0))
                break;

            yield return null;
        }
    }
}
