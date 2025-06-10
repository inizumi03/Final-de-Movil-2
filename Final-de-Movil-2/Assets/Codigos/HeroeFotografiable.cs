using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeFotografiable : MonoBehaviour
{
    [Header("Puntos que da al ser fotografiado")]
    public int puntosPorFoto = 10;

    [HideInInspector]
    public bool fueFotografiado = false;
}

