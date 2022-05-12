using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    private GameManager gameManager;

    private bool activarVictoriaFase;
    public bool ActivarVictoriaFase { get => activarVictoriaFase; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TocoGoldPlatform() //Activar sonido de victoria
    {
        activarVictoriaFase = true;
        //audioController.ReproducirSonidoVictoria();
        //gameManager.ActivarPanelCargarNivel();
    }

    public void TocoUltimaGoldPlatform()
    {
        gameManager.ActivarVictoriaJuego = true;
    }
}