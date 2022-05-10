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