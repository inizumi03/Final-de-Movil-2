using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Fotografo : MonoBehaviour
{
    [Header("Puntaje")]
    public int puntosPorFoto = 10;
    private int puntajeTotal = 0;

    [Header("Referencias UI")]
    public Text textoPuntaje;                // Texto para mostrar puntaje en juego
    public GameObject uiCanvas;              // Canvas que contiene la UI para ocultar al sacar foto

    [Header("Tag de los objetos fotografiables")]
    public string tagHeroe = "Heroe";

    [Header("Audio")]
    public AudioSource audioFoto;

    // Fotos capturadas (Texturas)
    public List<Texture2D> fotosCapturadas = new List<Texture2D>();

    // Lista de héroes fotografiados para no contar puntos varias veces
    private HashSet<HeroeFotografiable> heroesFotografiados = new HashSet<HeroeFotografiable>();

    public void IntentarTomarFoto()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit impacto, 100f))
        {
            if (impacto.collider.CompareTag(tagHeroe))
            {
                HeroeFotografiable heroe = impacto.collider.GetComponent<HeroeFotografiable>();
                if (heroe != null && !heroesFotografiados.Contains(heroe))
                {
                    StartCoroutine(TomarFotoCoroutine(heroe));
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

    IEnumerator TomarFotoCoroutine(HeroeFotografiable heroe)
    {
        // Ocultar UI para la captura limpia
        if (uiCanvas != null) uiCanvas.SetActive(false);

        yield return new WaitForEndOfFrame();

        // Captura la pantalla en una textura
        Texture2D foto = ScreenCapture.CaptureScreenshotAsTexture();

        if (foto != null)
        {
            fotosCapturadas.Add(foto);
            heroesFotografiados.Add(heroe);
            heroe.fueFotografiado = true;

            // Sumar puntos y actualizar texto
            puntajeTotal += puntosPorFoto;
            ActualizarTextoPuntaje();

            if (audioFoto != null) audioFoto.Play();

            Debug.Log("Foto tomada a " + heroe.name);
        }
        else
        {
            Debug.LogError("La captura salió vacía.");
        }

        yield return new WaitForSeconds(2f);

        // Reactivar UI
        if (uiCanvas != null) uiCanvas.SetActive(true);
    }

    void ActualizarTextoPuntaje()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeTotal;
        }
    }

    public int ObtenerPuntaje()
    {
        return puntajeTotal;
    }
}

