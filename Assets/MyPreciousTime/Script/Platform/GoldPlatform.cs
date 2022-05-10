using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatform : MonoBehaviour
{

    private bool terminoAnim;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void ActivarAnimVictoria()
    {
        gameManager.PasarASiguienteNivel();
    }
}