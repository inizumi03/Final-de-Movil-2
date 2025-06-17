using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinDeNivel : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject menuFinal;
    public Text textoPuntos;
    public Text textoFotos;

    public Fotografo fotografo;

    void Start()
    {
        if (menuFinal != null)
            menuFinal.SetActive(false);
    }

    public void MostrarMenuFinal()
    {
        if (menuFinal != null)
            menuFinal.SetActive(true);

        // Mostrar puntos
        if (textoPuntos != null && fotografo != null)
            textoPuntos.text = "Puntos Totales: " + fotografo.ObtenerPuntaje();

        // Contar héroes fotografiados
        int cantidadFotos = 0;
        ComportamientoHeroe[] heroes = FindObjectsOfType<ComportamientoHeroe>();
        foreach (ComportamientoHeroe heroe in heroes)
        {
            if (heroe.fueFotografiado)
                cantidadFotos++;
        }

        if (textoFotos != null)
            textoFotos.text = "Fotos tomadas: " + cantidadFotos;
    }
}
