using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool estaVivoJugador;

    private GameManager gameManager;

    public bool EstaVivoJugador { get => estaVivoJugador; }
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        estaVivoJugador = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Derrota"))
        {
            //Activar Anim derrota
            Debug.Log("Murio");
            estaVivoJugador = false;
        }
        else if (collision.CompareTag("Meta"))
        {
            collision.gameObject.GetComponentInParent<GoldPlatform>().TocoGoldPlatform();
        }
        else if (collision.CompareTag("Secuencial"))
        {
            Debug.Log("Secuencia");
            collision.gameObject.GetComponentInParent<SecuencialPlatform>().ActivarCorrPlataformaInicial();
        }
        else if (collision.CompareTag("Victoria"))
        {
            collision.gameObject.GetComponentInParent<GoldPlatform>().TocoUltimaGoldPlatform();
        }
        else if (collision.CompareTag("SecuencialGold"))
        {
            collision.gameObject.GetComponentInParent<DynamicPlatform>().TocoPlataforma();
        }
        else if (collision.CompareTag("SecuencialDynamic"))
        {
            collision.gameObject.GetComponentInParent<SencuencialDynamic>().TocoPlataforma();
        }
        else if (collision.CompareTag("Activadora"))
        {
            collision.gameObject.GetComponentInParent<ActivadoraPlatform>().ActivarPlatformaGO();
        }
        else if (collision.CompareTag("Timer"))
        {
            collision.gameObject.GetComponentInParent<TimePlatform>().IniciarPlatformTime();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Derrota"))
        {
            Debug.Log("MurioStay");
            estaVivoJugador = false;
        }

        else if (collision.CompareTag("Meta"))
        {
            collision.gameObject.GetComponentInParent<GoldPlatform>().TocoGoldPlatform();
        }
        else if (collision.CompareTag("Secuencial"))
        {
            Debug.Log("Secuencia");
            collision.gameObject.GetComponentInParent<SecuencialPlatform>().ActivarCorrPlataformaInicial();
        }
        else if (collision.CompareTag("Victoria"))
        {
            collision.gameObject.GetComponentInParent<GoldPlatform>().TocoUltimaGoldPlatform();
        }
        else if (collision.CompareTag("SecuencialGold"))
        {
            collision.gameObject.GetComponentInParent<DynamicPlatform>().TocoPlataforma();
        }
        else if (collision.CompareTag("SecuencialDynamic"))
        {
            collision.gameObject.GetComponentInParent<SencuencialDynamic>().TocoPlataforma();
        }
        else if (collision.CompareTag("Activadora"))
        {
            collision.gameObject.GetComponentInParent<ActivadoraPlatform>().ActivarPlatformaGO();
        }
        else if (collision.CompareTag("Timer"))
        {
            collision.gameObject.GetComponentInParent<TimePlatform>().IniciarPlatformTime();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inicial"))
        {
            Debug.Log("Salio");
            collision.gameObject.GetComponentInParent<InicialPlatform>().DesactivarPlataforma();
            //O iniciar animacion de desaparecer plataforma
        }
    }
}