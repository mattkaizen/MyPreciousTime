using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool juegoActivo;

    public bool JuegoActivo { get => juegoActivo; }

    private void Start()
    {
        juegoActivo = true;
    }
}