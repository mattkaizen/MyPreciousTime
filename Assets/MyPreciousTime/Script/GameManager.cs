using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerCollision playerCollision;
    private PlayerAnimator playerAnim;

    private bool juegoActivo;
    private bool seActivoMuerte;

    private int numeroDeNivel;
    private int numeroDeFase;
    private int numeroDeEscena;

    public bool JuegoActivo { get => juegoActivo; }

    private void Awake()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        playerAnim = FindObjectOfType<PlayerAnimator>();
    }
    private void Start()
    {
        juegoActivo = true;
    }

    private void Update()
    {
        ConsultarNivelYFase();
        RastrearMuerteJugador();
    }

    public void PasarASiguienteNivel()
    {
        switch (numeroDeNivel)
        {
            case 1: //Si estoy en el nivel 1 etapa 1, carga la etapa 2
                if(numeroDeFase == 1) //Carga el nivel 1 fase 2
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                else if (numeroDeFase == 2) //Carga el nivel 1 fase 3
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                else if (numeroDeFase == 3) //Carga el nivel 2
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                break;
            case 2: //Si estoy en el nivel 1 etapa 1, carga la etapa 2
                if (numeroDeFase == 1) //Carga el nivel 1 fase 2
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                else if (numeroDeFase == 2) //Carga el nivel 1 fase 3
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                else if (numeroDeFase == 3) //Carga el nivel 2
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                break;
        }
    }
    private void ConsultarNivelYFase()
    {
        switch (SceneManager.GetActiveScene().buildIndex) // El 0 es el menu principal, El 1 es el nivel 1
        {
            // Escena de indice 1 2, 3, es Nivel 1
            case 1:
                numeroDeNivel = 1;
                numeroDeFase = 1;
                numeroDeEscena = 1;
                break;
            case 2:
                numeroDeNivel = 1;
                numeroDeFase = 2;
                numeroDeEscena = 1;
                break;
            case 3:
                numeroDeNivel = 1;
                numeroDeFase = 3;
                numeroDeEscena = 1;
                break;
            // Escena de indice 4, 5, 6, es Nivel 2
            case 4:
                numeroDeNivel = 2;
                numeroDeFase = 1;
                numeroDeEscena = 4;
                break;
            case 5:
                numeroDeNivel = 2;
                numeroDeFase = 2;
                numeroDeEscena = 4;
                break;
            case 6:
                numeroDeNivel = 2;
                numeroDeFase = 3;
                numeroDeEscena = 4;
                break;
        }
    }

    public void VolverAPrimeraFaseDelNivel()
    {
        if (numeroDeNivel == 1)
        {
            SceneManager.LoadScene(numeroDeEscena); //Carga el nivel 1, fase 1
        }
        else if (numeroDeNivel == 2)
        {
            SceneManager.LoadScene(numeroDeEscena); //Carga el nivel 2, fase 1
        }
    }

    private void RastrearMuerteJugador()
    {
        if (!playerCollision.EstaVivoJugador && !seActivoMuerte)
        {
            seActivoMuerte = true;

            playerAnim.StartDeathAnim();
            Debug.Log("activarAnmMuerte");
            //activar animacion de muerte
        }
    }
}