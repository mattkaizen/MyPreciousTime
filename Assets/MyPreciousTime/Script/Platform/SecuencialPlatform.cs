using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuencialPlatform : MonoBehaviour
{
    [Header("Tipo de plataforma")]
    [SerializeField] bool platInicial;
   
    [SerializeField] float timeToActivePlatform;
    [Header("Activa Un GO")]
    [SerializeField] bool platActivadora;
    [SerializeField] GameObject siguientePlataforma;
    [Space]
    [Header("Si es la plataforma inicial agregar. .")]
    [SerializeField] Animator nextPlatformAnim;

    [Header("Cambio de colores ")]
    [SerializeField] Color colorInicial;
    [SerializeField] Color colorFinal;
    [SerializeField] float smoothness;

    [Header("Inicia activado?")]
    [SerializeField] bool iniciaActivado;
    private float duration;

    private SpriteRenderer plataformaSpriteR;
    private Animator platformAnim;
    private GameManager gameManager;

    private bool plataformaInicialActivada;

    private void Awake()
    {
        platformAnim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        plataformaSpriteR = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if(!iniciaActivado)
        {
            platformAnim.SetBool("Desactivar", true);
        }
    }


    public void ActivarCorrPlataformaInicial()
    {
        if (!plataformaInicialActivada && platInicial && !platActivadora)
        {
            plataformaInicialActivada = true;
            ActivarSigPlataforma(nextPlatformAnim);
        }
        if(!plataformaInicialActivada && platInicial && platActivadora)
        {
            plataformaInicialActivada = true;

            ActivarSigPlataforma(nextPlatformAnim);
            //StartCoroutine(ActivarPlatafGO(platformAnim));
        }
    }

    public void ActivarPlataformaPermanente(Animator nextPlatform)
    {
        StartCoroutine(ActivarPlataformaSecuencialPermanente(nextPlatform));

    }

    public void ActivarSigPlataforma(Animator nextPlatform)
    {
        StartCoroutine(ActivarPlataformaSecuencial(platformAnim, nextPlatform));
    }
    public void DesactivarOtraPlatforma(Animator otherPlatform)
    {
        otherPlatform.SetBool("Desactivar", true);
    }

    IEnumerator ActivarPlataformaSecuencialPermanente(Animator nextPlatform) //Si inicia al aparecer la plataforma
    {
        yield return new WaitForSeconds(timeToActivePlatform);
        nextPlatform.SetBool("Activar", true);
    }
    IEnumerator ActivarPlataformaSecuencial(Animator thisPlatform, Animator nextPlatform) //Si inicia al aparecer la plataforma
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);
        StartCoroutine(LerpColor());
        yield return new WaitForSeconds(timeToActivePlatform);
        if (platActivadora)
            siguientePlataforma.SetActive(true);
        nextPlatform.SetBool("Desactivar", false);
        nextPlatform.SetBool("Activar", true);
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", true);
        Debug.Log("PlataformaIniciada");
    }

    IEnumerator ActivarPlatafGO(Animator thisPlatform)
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);

        StartCoroutine(LerpColor());
        yield return new WaitForSeconds(timeToActivePlatform);
        if (platActivadora)
            siguientePlataforma.SetActive(true);

        thisPlatform.SetBool("Desactivar", true);
    }

    IEnumerator LerpColor()
    {
        plataformaSpriteR.color = colorInicial;
        duration = timeToActivePlatform;
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            plataformaSpriteR.color = Color.Lerp(plataformaSpriteR.color, colorFinal, progress);
            //bloom.color.Interp(Color.red, Color.magenta, progress);
            progress += increment;
            Debug.Log("Lerp");
            yield return new WaitForSeconds(smoothness);
        }
    }
}