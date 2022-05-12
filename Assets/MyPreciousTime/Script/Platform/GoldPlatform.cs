using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    [SerializeField] int numeroPlataforma;

    private GameManager gameManager;

    private bool activarVictoriaFase;
    public bool ActivarVictoriaFase { get => activarVictoriaFase; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void TocoGoldPlatform() //Activar sonido de victoria
    {
        TocoGoldPlatformNro();
        activarVictoriaFase = true;

        Debug.Log("dorada");
        //audioController.ReproducirSonidoVictoria();
        //gameManager.ActivarPanelCargarNivel();
    }

    private void TocoGoldPlatformNro()
    {
        if(!activarVictoriaFase)
        {
            gameManager.PlatfDoradasTocadas++;

            Debug.Log("Toco plataforma dorada");
        }
    }

    public void TocoUltimaGoldPlatform()
    {
        gameManager.ActivarVictoriaJuego = true;
    }
}