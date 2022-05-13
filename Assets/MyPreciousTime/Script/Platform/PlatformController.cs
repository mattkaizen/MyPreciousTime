using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Controlador de plataforma")]
    [SerializeField] float timeToActivePlatform;
    [SerializeField] int estadoPlataforma;

    private Animator platformAnim;
    private GameManager gameManager;

    private bool plataformaActiva;
    private bool platafomaSecuencia;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        platformAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!plataformaActiva)
        {
            StartCoroutine(ActivarPlataforma());
        }
    }

    public void ActivarCorrutinaPlataforma()
    {
        if (!plataformaActiva)
        {
            StartCoroutine(ActivarPlataforma());
        }
    }
    void ElegirEstado()
    {
        switch (estadoPlataforma)
        {
            case 0:
                DesactivarAnimacion();
                estadoPlataforma = 1;
                Debug.Log("Ejecutado0");
                break;
            case 1:
                ActivarAnimacion();
                estadoPlataforma = 0;
                Debug.Log("Ejecutado1");
                break;
        }
    }
    void ActivarAnimacion()
    {
        platformAnim.SetBool("Desactivar", false);
        platformAnim.SetBool("Activar", true);
    }
    void DesactivarAnimacion()
    {
        platformAnim.SetBool("Activar", false);
        platformAnim.SetBool("Desactivar", true);
    }
    IEnumerator ActivarPlataforma()
    {
        while (gameManager.JuegoActivo)
        {
            plataformaActiva = true;
            yield return new WaitForSeconds(timeToActivePlatform);
            ElegirEstado();
        }
    }
}