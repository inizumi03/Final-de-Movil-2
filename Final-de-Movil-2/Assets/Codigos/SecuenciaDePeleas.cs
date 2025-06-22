using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaDePeleas : MonoBehaviour
{
    [System.Serializable]
    public class AnimacionConPuntos
    {
        public string nombreAnimacion;
        public int puntos;
    }

    public List<AnimacionConPuntos> animaciones = new List<AnimacionConPuntos>();
    public Animator animator;
    public HeroeFotografiable heroeFotografiable;

    public float tiempoEntreAnimaciones = 0.5f;
    private bool secuenciaIniciada = false;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (heroeFotografiable == null) heroeFotografiable = GetComponent<HeroeFotografiable>();
        StartCoroutine(IniciarSecuenciaUnaVez());
    }

    IEnumerator IniciarSecuenciaUnaVez()
    {
        if (secuenciaIniciada || animaciones.Count == 0) yield break;
        secuenciaIniciada = true;

        for (int i = 0; i < animaciones.Count; i++)
        {
            // Cambiar el parámetro "Estado" del Animator
            animator.SetInteger("Estado", i + 1); // Asumimos Estado == 1 es primera animación, etc.

            // Esperar hasta que termine la animación actual
            yield return new WaitUntil(() =>
            {
                AnimatorStateInfo estado = animator.GetCurrentAnimatorStateInfo(0);
                return estado.normalizedTime >= 1f && !animator.IsInTransition(0);
            });

            // Sumar los puntos de esta animación
            heroeFotografiable.puntosPorFoto += animaciones[i].puntos;

            yield return new WaitForSeconds(tiempoEntreAnimaciones);
        }

        // Volver a Idle (Estado == 0)
        animator.SetInteger("Estado", 0);
    }
}
