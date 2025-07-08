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
    public bool bloquearEjeX = false;  // Bloquear rotación en el eje X
    public bool bloquearEjeY = false;  // Bloquear rotación en el eje Y

    [Header("Referencia al jugador")]
    public Transform jugador; // Asigna el jugador desde el Inspector

    [Header("Joystick")]
    public FixedJoystick joystick; // Asignar el joystick desde el Inspector

    private Quaternion rotacionObjetivo;
    private bool centrandoCamara = false;

    private float rotacionX = 0f; // Variable para almacenar la rotación en el eje X
    private float rotacionY = 0f; // Variable para almacenar la rotación en el eje Y

    private float threshold = 0.1f; // Umbral para detectar si el joystick está en la posición neutral
    private bool joystickEnMovimiento = false; // Variable para detectar si el joystick está en movimiento

    void Start()
    {
        rotacionObjetivo = transform.rotation;
    }

    void Update()
    {
        centrandoCamara = false; // Reiniciar cada frame

        // Obtener el valor del joystick
        float ejeX = joystick.Horizontal; // Valor horizontal (izquierda/derecha)
        float ejeY = joystick.Vertical;   // Valor vertical (arriba/abajo)

        // Solo rotar si el joystick se mueve fuera de la posición neutral
        if (Mathf.Abs(ejeX) > threshold || Mathf.Abs(ejeY) > threshold)
        {
            joystickEnMovimiento = true; // El joystick está en movimiento

            // Aplicar la rotación solo en los ejes no bloqueados
            if (!bloquearEjeY)
            {
                rotacionY += ejeX;  // La rotación en Y depende del eje horizontal del joystick
            }

            if (!bloquearEjeX)
            {
                rotacionX -= ejeY; // La rotación en X depende del eje vertical del joystick (invertido)
                rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); // Limitar la rotación para evitar que gire más de 90 grados hacia arriba o abajo
            }

            // Aplicar la rotación final en la cámara
            transform.rotation = Quaternion.Euler(rotacionX, rotacionY, 0f);
        }
        else if (joystickEnMovimiento)  // Si el joystick se ha soltado y estaba en movimiento
        {
            joystickEnMovimiento = false; // El joystick ya no está en movimiento
            rotacionObjetivo = Quaternion.Euler(rotacionX, rotacionY, 0f); // Mantener la rotación en la última posición
        }

        // Aplicar rotación suavizada si se está centrando la cámara
        if (centrandoCamara)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, Time.deltaTime * suavizadoCentrado);
        }
    }
}
