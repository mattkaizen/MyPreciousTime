using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    private GameManager gameManager;

    private bool activarVictoria;
    public bool ActivarVictoria { get => activarVictoria; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TocoGoldPlatform() //Activar sonido de victoria
    {
        activarVictoria = true;
        //audioController.ReproducirSonidoVictoria();
        //gameManager.ActivarPanelCargarNivel();
    }
}