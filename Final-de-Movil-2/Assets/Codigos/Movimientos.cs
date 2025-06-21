using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientos : MonoBehaviour
{
    public Transform[] puntosDestino;
    public float velocidad = 3f;
    public float distanciaMinima = 0.1f;
    public float velocidadRotacion = 5f; // Nueva: velocidad para girar

    private int indiceActual = 0;
    private bool movimientoFinalizado = false;

    [Header("Referencia al Fin de Nivel (opcional)")]
    public MenuFinal menuFinal;

    void Update()
    {
        if (movimientoFinalizado || puntosDestino.Length == 0) return;

        Transform destino = puntosDestino[indiceActual];
        Vector3 posicionActual = transform.position;
        Vector3 posicionObjetivo = destino.position;

        Vector3 direccion = posicionObjetivo - posicionActual;

        // Evita rotación en Y si no hay desnivel
        if (Mathf.Abs(direccion.y) < 0.05f)
            direccion.y = 0;

        if (direccion != Vector3.zero)
        {
            // Gira suavemente hacia la dirección de movimiento
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidadRotacion);
        }

        // Movimiento
        Vector3 movimiento = direccion.normalized * velocidad * Time.deltaTime;
        if (movimiento.magnitude > direccion.magnitude)
        {
            transform.position = posicionObjetivo;
        }
        else
        {
            transform.position += movimiento;
        }

        // Verifica si llegó
        if (Vector3.Distance(transform.position, posicionObjetivo) <= distanciaMinima)
        {
            indiceActual++;
            if (indiceActual >= puntosDestino.Length)
            {
                movimientoFinalizado = true;
                if (menuFinal != null)
                    menuFinal.MostrarMenuFinal();
            }
        }
    }
}
