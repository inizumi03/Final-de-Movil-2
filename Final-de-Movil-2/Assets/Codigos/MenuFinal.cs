using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelMenuFinal;
    public Text textoPuntajeFinal;
    public Transform contenedorFotos; // Un layout horizontal/vertical
    public GameObject prefabImagen;   // Prefab con componente RawImage

    [Header("Referencias al sistema de fotografía")]
    public Fotografo fotografo;

    public void MostrarMenuFinal()
    {
        panelMenuFinal.SetActive(true);

        // Mostrar el puntaje total
        textoPuntajeFinal.text = "Puntaje total: " + fotografo.ObtenerPuntaje();

        // Cargar las fotos como RawImages
        foreach (Texture2D foto in fotografo.fotosCapturadas)
        {
            GameObject nuevaFoto = Instantiate(prefabImagen, contenedorFotos);
            RawImage img = nuevaFoto.GetComponent<RawImage>();
            if (img != null)
                img.texture = foto;
        }
    }
}
