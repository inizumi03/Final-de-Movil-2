using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Fotografo : MonoBehaviour
{
    public float distanciaMaxima = 100f;
    public string tagHeroe = "Heroe";
    public AudioSource audioFoto;
    public GameObject interfazUI;
    public RawImage pantallaFoto;
    public Camera camaraFoto;
    public RenderTexture texturaFoto;
    public Text textoPuntaje;

    private static int puntajeTotal = 0;

    private void Start()
    {
        ActualizarPuntajeUI();
    }

    public static void SumarPuntos(int puntos)
    {
        puntajeTotal += puntos;
    }

    public void IntentarTomarFoto()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(rayo, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag(tagHeroe))
            {
                ComportamientoHeroe heroe = hit.collider.GetComponent<ComportamientoHeroe>();
                if (heroe != null)
                {
                    if (!FueFotografiado(heroe))
                    {
                        heroe.MarcarFotografiado();
                        ActualizarPuntajeUI();

                        if (audioFoto) audioFoto.Play();
                        StartCoroutine(SacarFoto());
                    }
                    else
                    {
                        Debug.Log("Este héroe ya fue fotografiado.");
                    }
                }
            }
        }
    }

    bool FueFotografiado(ComportamientoHeroe heroe)
    {
        return heroe.GetType().GetField("yaFotografiado", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(heroe) as bool? == true;
    }

    void ActualizarPuntajeUI()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeTotal;
        }
    }

    System.Collections.IEnumerator SacarFoto()
    {
        interfazUI.SetActive(false);
        yield return new WaitForEndOfFrame();

        camaraFoto.targetTexture = texturaFoto;
        camaraFoto.Render();
        RenderTexture.active = texturaFoto;

        Texture2D foto = new Texture2D(texturaFoto.width, texturaFoto.height, TextureFormat.RGB24, false);
        foto.ReadPixels(new Rect(0, 0, texturaFoto.width, texturaFoto.height), 0, 0);
        foto.Apply();

        pantallaFoto.texture = foto;

        camaraFoto.targetTexture = null;
        RenderTexture.active = null;

        interfazUI.SetActive(true);
    }
}

