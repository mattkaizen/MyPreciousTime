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

    private Animator platformAnim;
    private GameManager gameManager;

    private bool plataformaInicialActivada;

    private void Awake()
    {
        platformAnim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
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

            StartCoroutine(ActivarPlatafGO(platformAnim));
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
        yield return new WaitForSeconds(timeToActivePlatform);
        if (platActivadora)
            siguientePlataforma.SetActive(true);
        nextPlatform.SetBool("Activar", true);
        thisPlatform.SetBool("Desactivar", true);
        Debug.Log("PlataformaIniciada");
    }

    IEnumerator ActivarPlatafGO(Animator thisPlatform)
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);
        yield return new WaitForSeconds(timeToActivePlatform);
        if (platActivadora)
            siguientePlataforma.SetActive(true);

        thisPlatform.SetBool("Desactivar", true);
    }
}