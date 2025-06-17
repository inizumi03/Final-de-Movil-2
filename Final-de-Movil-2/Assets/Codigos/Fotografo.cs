using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fotografo : MonoBehaviour
{
    [Header("Configuración de cámara")]
    public float distanciaMaxima = 100f;

    [Header("Puntaje")]
    public int puntosPorFoto = 10;
    private int puntajeTotal = 0;

    [Header("UI y Captura")]
    public Canvas canvasPrincipal;
    public AudioSource audioFoto;
    public string tagHeroe = "Heroe";

    [Header("Fotos capturadas")]
    public List<Texture2D> fotosCapturadas = new List<Texture2D>();

    private bool tomandoFoto = false;

    public void IntentarTomarFoto()
    {
        if (!tomandoFoto)
            StartCoroutine(Fotografiar());
    }

    private IEnumerator Fotografiar()
    {
        tomandoFoto = true;

        // Lanzar raycast
        Ray rayo = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit impacto, distanciaMaxima))
        {
            if (impacto.collider.CompareTag(tagHeroe))
            {
                HeroeFotografiable heroe = impacto.collider.GetComponent<HeroeFotografiable>();
                if (heroe != null && !heroe.fueFotografiado)
                {
                    heroe.fueFotografiado = true;
                    puntajeTotal += puntosPorFoto;

                    // Ocultar UI y esperar un frame
                    if (canvasPrincipal != null)
                        canvasPrincipal.enabled = false;

                    yield return new WaitForEndOfFrame();

                    // Captura de pantalla
                    Texture2D captura = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
                    captura.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                    captura.Apply();
                    fotosCapturadas.Add(captura);

                    // Reactivar UI
                    if (canvasPrincipal != null)
                        canvasPrincipal.enabled = true;

                    // Sonido y mensaje
                    if (audioFoto != null)
                        audioFoto.Play();

                    Debug.Log("¡Foto tomada al héroe: " + impacto.collider.name + "!");
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

        tomandoFoto = false;
    }
    public int ObtenerPuntaje()
    {
        return puntajeTotal;
    }
}

