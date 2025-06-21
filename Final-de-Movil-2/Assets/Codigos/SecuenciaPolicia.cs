using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaPolicia : MonoBehaviour
{
    public Animator animador;

    [Header("Nombres de las animaciones")]
    public string animacionIdle = "Idle";
    public string animacionCaminar = "Caminar";
    public string animacionEspecial = "Especial";

    [Header("Tiempos en segundos")]
    public float tiempoEnIdle = 2f;
    public float tiempoCaminando = 3f;
    public float tiempoEspecial = 2f;

    [Header("Puntaje por animación")]
    public int puntosIdle = 10;
    public int puntosCaminar = 20;
    public int puntosEspecial = 30;

    private HeroeFotografiable heroeDatos;
    private bool yaInicio = false;

    void Start()
    {
        animador.Play(animacionIdle);
        heroeDatos = GetComponent<HeroeFotografiable>();

        if (heroeDatos != null)
            heroeDatos.puntosPorFoto = puntosIdle;

        StartCoroutine(Secuencia());
    }

    IEnumerator Secuencia()
    {
        if (yaInicio) yield break;
        yaInicio = true;

        // Espera en Idle
        yield return new WaitForSeconds(tiempoEnIdle);

        animador.Play(animacionCaminar);
        if (heroeDatos != null)
            heroeDatos.puntosPorFoto = puntosCaminar;

        yield return new WaitForSeconds(tiempoCaminando);

        animador.Play(animacionEspecial);
        if (heroeDatos != null)
            heroeDatos.puntosPorFoto = puntosEspecial;

        yield return new WaitForSeconds(tiempoEspecial);

        animador.Play(animacionIdle);
        if (heroeDatos != null)
            heroeDatos.puntosPorFoto = puntosIdle;
    }
}
