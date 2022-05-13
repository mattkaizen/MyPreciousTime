using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlatform : MonoBehaviour
{
    [Header("Lista de plataformas doradas")]
    [SerializeField] List<Animator> goldPlatformsAnim;
    [SerializeField] List<GoldPlatform> goldPlatforms;

    [Header("Tiempo entre plataformas")]
    [SerializeField] float tiempoActivarSgtPlatf; // 5
    [SerializeField] float tiempoGoldActiva; // 1

    [Header("Desactiva plataforma")]
    [SerializeField] bool desactivaPlataforma; //
    [SerializeField] GameObject prePlataform; //

    private GameManager gameManager;

    private int plataformaActual;

    private bool tocoPlatformaInicial;
    private bool inicioTimer;
    private bool desaparecioGold;


    private void Awake()
    {
        plataformaActual = 0;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        ActivarTimerInicial();
    }


    public void IniciarPlatformTime()
    {
        if (!tocoPlatformaInicial)
        {
            tocoPlatformaInicial = true;
        }
    }
    private void ActivarTimerInicial()
    {
        if (tocoPlatformaInicial && !inicioTimer)
        {
            if(desactivaPlataforma)
            {
                prePlataform.SetActive(false);
            }
            inicioTimer = true;
            //iniciarCorrutina
            StartCoroutine(Esperar5Segundos());
        }
    }

    IEnumerator Esperar5Segundos()
    {
        yield return new WaitForSeconds(tiempoActivarSgtPlatf);
        StartCoroutine(ActivarPlataformaGold());
    }

    IEnumerator ActivarPlataformaGold() //Si inicia al aparecer la plataforma
    {
        //Actiivar plataforma dorada
        if (plataformaActual > goldPlatformsAnim.Count - 1)
        {
            plataformaActual = 0;
        }
        Debug.Log("Plataforma actal" + plataformaActual);
        goldPlatformsAnim[plataformaActual].SetBool("Desactivar", false);
        goldPlatformsAnim[plataformaActual].SetBool("Activar", true);
        yield return new WaitForSeconds(tiempoGoldActiva);

        if (gameManager.PlatfDoradasTocadas != gameManager.PlataformasDoradasXNivel)
        {
            goldPlatformsAnim[plataformaActual].SetBool("Activar", false);
            goldPlatformsAnim[plataformaActual].SetBool("Desactivar", true);

            plataformaActual++;

            inicioTimer = false;
        }

        //Desactivar plataforma dorada
        //iniciar corrutina a esperar 5 segundos
    }
}