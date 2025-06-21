using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimacionConPuntaje
{
    public string nombreAnimacion;
    public int puntos;
}
public class HeroeFotografiable : MonoBehaviour
{
    [Header("Puntos que da al ser fotografiado")]
    public int puntosPorFoto = 1;

    [HideInInspector]
    public bool fueFotografiado = false;

    [Header("Sistema avanzado de animaciones con puntaje")]
    public bool usarPuntajePorAnimacion = false;
    public Animator animador;
    public List<AnimacionConPuntaje> animacionesConPuntaje = new List<AnimacionConPuntaje>();

    public int ObtenerPuntajeFinal()
    {
        if (!usarPuntajePorAnimacion || animador == null || animacionesConPuntaje.Count == 0)
            return puntosPorFoto;

        AnimatorStateInfo estadoActual = animador.GetCurrentAnimatorStateInfo(0);
        foreach (var animacion in animacionesConPuntaje)
        {
            if (estadoActual.IsName(animacion.nombreAnimacion))
            {
                return animacion.puntos;
            }
        }

        // Si no coincide ninguna animación, usar puntos base
        return puntosPorFoto;
    }
}

