using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuencialPlatform : MonoBehaviour
{
    [Header("Tipo de plataforma")]
    [SerializeField] bool platInicial;
    [SerializeField] float timeToActivePlatform;
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
        if (!plataformaInicialActivada && platInicial)
        {
            plataformaInicialActivada = true;
            ActivarSigPlataforma(nextPlatformAnim);
        }
    }

    public void ActivarSigPlataforma(Animator nextPlatform)
    {
        StartCoroutine(ActivarPlataformaSecuencial(platformAnim, nextPlatform));
    }

    //public void IniciarCorrutinaActivarPlataforma()
    //{
    //    StartCoroutine(ActivarPlataformaSecuencial());
    //}

    //void ElegirEstado()
    //{
    //    switch (estadoPlataforma)
    //    {
    //        case 0:
    //            DesactivarAnimacion();
    //            estadoPlataforma = 1;
    //            Debug.Log("Ejecutado0");
    //            break;
    //        case 1:
    //            ActivarAnimacion();
    //            estadoPlataforma = 0;
    //            Debug.Log("Ejecutado1");
    //            break;
    //    }
    //}

    IEnumerator ActivarPlataformaSecuencial(Animator thisPlatform, Animator nextPlatform) //Si inicia al aparecer la plataforma
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);
        yield return new WaitForSeconds(timeToActivePlatform);
        nextPlatform.SetBool("Activar", true);
        thisPlatform.SetBool("Desactivar", true);
        Debug.Log("PlataformaIniciada");
    }

    IEnumerator ActivarPlataformaInicial()
    {
        yield return new WaitForSeconds(timeToActivePlatform);
    }
    //IEnumerator ActivarPlataformaSecuencial()
    //{
    //    plataformaActiva = true;
    //    yield return new WaitForSeconds(timeToActivePlatform);
    //    //ElegirEstado();
    //}
    //void ActivarAnimacion()
    //{
    //    platformAnim.SetBool("Desactivar", false);
    //    platformAnim.SetBool("Activar", true);
    //}
    //void DesactivarAnimacion()
    //{
    //    platformAnim.SetBool("Activar", false);
    //    platformAnim.SetBool("Desactivar", true);
    //}
}