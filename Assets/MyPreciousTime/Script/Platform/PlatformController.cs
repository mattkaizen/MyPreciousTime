using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Controlador de plataforma")]
    [SerializeField] float timeToActivePlatform;
    [SerializeField] int estadoPlataforma;

    [Header("Cambio de colores ")]
    [SerializeField] Color colorInicial;
    [SerializeField] Color colorFinal;
    [SerializeField] float smoothness;
    private float duration;

    private SpriteRenderer PlataformaSpriteR;
    private Animator platformAnim;
    private GameManager gameManager;

    private bool plataformaActiva;
    private bool platafomaSecuencia;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        platformAnim = GetComponent<Animator>();

        PlataformaSpriteR = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!plataformaActiva)
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
                StartCoroutine(LerpColor());
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

    //IEnumerator LerpColor()
    //{
    //    Color.Lerp(colorInicial, colorFinal, timeToActivePlatform)
    //    yield return new WaitForSeconds(timeToActivePlatform);
    //}


    IEnumerator LerpColor()
    {
        PlataformaSpriteR.color = colorInicial;
        duration = timeToActivePlatform;
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            PlataformaSpriteR.color = Color.Lerp(PlataformaSpriteR.color, colorFinal, progress);
            //bloom.color.Interp(Color.red, Color.magenta, progress);
            progress += increment;
            Debug.Log("Lerp");
            yield return new WaitForSeconds(smoothness);
        }
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