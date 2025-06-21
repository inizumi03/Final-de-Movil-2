using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GirarCamara : MonoBehaviour
{
    [Header("Velocidad de giro")]
    public float velocidadRotacion = 50f;
    public float suavizadoCentrado = 5f;

    [Header("Bloquear ejes de rotación")]
    public bool bloquearEjeX = false;
    public bool bloquearEjeY = false;

    [Header("Porcentaje de pantalla para detectar toques en los bordes")]
    [Range(0f, 0.5f)] public float anchoBorde = 0.2f; // 20 % a cada lado
    [Range(0f, 0.5f)] public float altoBorde = 0.2f;  // 20 % arriba y abajo

    [Header("Referencia al jugador")]
    public Transform jugador; // Asigna el jugador desde el Inspector

    private Quaternion rotacionObjetivo;
    private bool centrandoCamara = false;

    void Start()
    {
        rotacionObjetivo = transform.rotation;
    }

    void Update()
    {
        centrandoCamara = false; // Reiniciar cada frame

        foreach (Touch toque in Input.touches)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(toque.fingerId))
                continue;

            Vector2 posicion = toque.position;
            float ancho = Screen.width;
            float alto = Screen.height;

            bool enBordeDerecho = posicion.x > ancho * (1f - anchoBorde);
            bool enBordeIzquierdo = posicion.x < ancho * anchoBorde;
            bool enBordeSuperior = posicion.y > alto * (1f - altoBorde);
            bool enBordeInferior = posicion.y < alto * altoBorde;

            bool estaEnBordeHorizontal = enBordeDerecho || enBordeIzquierdo;
            bool estaEnBordeVertical = enBordeSuperior || enBordeInferior;
            bool enCentro = !estaEnBordeHorizontal && !estaEnBordeVertical;

            if ((toque.phase == TouchPhase.Stationary || toque.phase == TouchPhase.Moved) && !enCentro)
            {
                Vector3 rotacion = Vector3.zero;

                if (!bloquearEjeY)
                {
                    if (enBordeDerecho) rotacion.y = 1f;
                    if (enBordeIzquierdo) rotacion.y = -1f;
                }

                if (!bloquearEjeX)
                {
                    if (enBordeSuperior) rotacion.x = -1f;
                    if (enBordeInferior) rotacion.x = 1f;
                }

                transform.Rotate(rotacion * velocidadRotacion * Time.deltaTime, Space.Self);
                rotacionObjetivo = transform.rotation;
            }

            // Centrado con toque mantenido en el centro
            if ((toque.phase == TouchPhase.Stationary) && enCentro && jugador != null)
            {
                rotacionObjetivo = Quaternion.Euler(0f, jugador.eulerAngles.y, 0f);
                centrandoCamara = true;
            }
        }

        // Aplicar rotación suavizada
        if (centrandoCamara)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, Time.deltaTime * suavizadoCentrado);
        }
    }
}
