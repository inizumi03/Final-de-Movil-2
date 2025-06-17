using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Fotografo : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaMaxima = 100f;
    public int puntosPorFoto = 10;
    public string tagHeroe = "Heroe";

    [Header("Audio")]
    public AudioSource audioFoto;

    [Header("Galería")]
    public List<Texture2D> fotosCapturadas = new List<Texture2D>();

    [Header("UI para ocultar temporalmente")]
    public GameObject uiCanvas;

    [Header("Texto puntaje TMP")]
    public TextMeshProUGUI textoPuntaje;

    private int puntajeTotal = 0;

    void Start()
    {
        ActualizarTextoPuntaje();
    }

    public void IntentarTomarFoto()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit impacto;

        if (Physics.Raycast(rayo, out impacto, distanciaMaxima))
        {
            if (impacto.collider.CompareTag(tagHeroe))
            {
                HeroeFotografiable heroe = impacto.collider.GetComponent<HeroeFotografiable>();
                if (heroe != null && !heroe.fueFotografiado)
                {
                    heroe.fueFotografiado = true;
                    puntajeTotal += puntosPorFoto;

                    if (audioFoto != null) audioFoto.Play();

                    ActualizarTextoPuntaje();
                    StartCoroutine(CapturarFoto());

                    Debug.Log("¡Foto tomada a: " + impacto.collider.name + "!");
                }
                else
                {
                    Debug.Log("Este héroe ya fue fotografiado.");
                }
            }
            else
            {
                Debug.Log("No estás mirando a un héroe.");
            }
        }
        else
        {
            Debug.Log("No hay nada al frente.");
        }
    }

    private IEnumerator CapturarFoto()
    {
        if (uiCanvas != null) uiCanvas.SetActive(false);
        yield return new WaitForEndOfFrame();

        Texture2D captura = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        captura.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        captura.Apply();

        fotosCapturadas.Add(captura);

        if (uiCanvas != null) uiCanvas.SetActive(true);
    }

    public int ObtenerPuntaje()
    {
        return puntajeTotal;
    }

    void ActualizarTextoPuntaje()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntos: " + puntajeTotal;
    }
}

