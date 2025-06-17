using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importante para TextMeshPro

public class MenuFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelMenuFinal;
    public TMP_Text textoPuntajeFinal;
    public Transform contenedorFotos; // Contenedor para las imágenes
    public GameObject prefabImagen;   // Prefab con RawImage para mostrar fotos

    [Header("Referencia a Fotografo")]
    public Fotografo fotografo;

    public void MostrarMenuFinal()
    {
        panelMenuFinal.SetActive(true);

        // Mostrar puntaje total
        textoPuntajeFinal.text = "Puntaje total: " + fotografo.ObtenerPuntaje();

        // Limpiar fotos previas (si las hubiera)
        foreach (Transform hijo in contenedorFotos)
        {
            Destroy(hijo.gameObject);
        }

        // Instanciar fotos tomadas
        foreach (Texture2D foto in fotografo.fotosCapturadas)
        {
            GameObject nuevaFoto = Instantiate(prefabImagen, contenedorFotos);
            RawImage img = nuevaFoto.GetComponent<RawImage>();
            if (img != null)
            {
                img.texture = foto;
            }
        }
    }
}

