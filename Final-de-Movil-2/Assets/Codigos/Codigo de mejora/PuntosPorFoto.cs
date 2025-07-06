using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosPorFoto : MonoBehaviour
{
    public Animator animadorHeroe;  // Referencia al Animator del héroe
    public HeroeFotografiable heroe;  // Referencia al script HeroeFotografiable

    [Header("Animaciones y Puntos")]
    public string animacionIdle = "Idle";  // Nombre de la animación Idle
    public string animacionCaminar = "Caminar";  // Nombre de la animación Caminar
    public string animacionEspecial = "Especial";  // Nombre de la animación Especial

    [Header("Puntaje por Animación")]
    public int puntosIdle = 10;
    public int puntosCaminar = 20;
    public int puntosEspecial = 30;

    private bool yaInicio = false;

    void Start()
    {
        // Inicializamos la animación en Idle y asignamos el puntaje de Idle
        animadorHeroe.Play(animacionIdle);
        if (heroe != null)
        {
            heroe.puntosPorFoto = puntosIdle;  // Asignamos puntos iniciales para Idle
        }

        // Empezamos la secuencia de animaciones
        StartCoroutine(Secuencia());
    }

    IEnumerator Secuencia()
    {
        if (yaInicio) yield break;
        yaInicio = true;

        // Animación Idle
        yield return new WaitForSeconds(2f);  // Esperamos el tiempo de la animación Idle
        animadorHeroe.Play(animacionCaminar);  // Activamos la animación de caminar
        if (heroe != null)
        {
            heroe.puntosPorFoto = puntosCaminar;  // Cambiamos los puntos a los de caminar
        }

        // Animación Caminar
        yield return new WaitForSeconds(3f);  // Esperamos el tiempo de la animación caminar
        animadorHeroe.Play(animacionEspecial);  // Activamos la animación especial
        if (heroe != null)
        {
            heroe.puntosPorFoto = puntosEspecial;  // Cambiamos los puntos a los de especial
        }

        // Animación Especial
        yield return new WaitForSeconds(2f);  // Esperamos el tiempo de la animación especial
        animadorHeroe.Play(animacionIdle);  // Regresamos a la animación idle
        if (heroe != null)
        {
            heroe.puntosPorFoto = puntosIdle;  // Volvemos a los puntos de Idle
        }
    }
}
