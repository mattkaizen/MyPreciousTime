using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour
{
    [Header("Animator del panel")]
    [SerializeField] Animator panelAnimator;

    [Header("Animator del Fondo")]
    [SerializeField] Animator fondoAnim;

    [Header("Apagar musica")]
    [SerializeField] float velCorrutina;
    [SerializeField] float velocidadDisminuidor;
    [SerializeField] float valorDisminuidor;
    [SerializeField] AudioSource musicAS;
    [SerializeField] AudioClip musicaMenuAudioClip;

    private bool iniciarMusica;

    private void Awake()
    {
        //velocidadDisminuidor = 30.00f;
        iniciarMusica = false;
    }

    private void Update()
    {
        ReproducirMusicaMenuPrincipal();

        CambiarFondoMenu();
    }

    public void CambiarFondoMenu()
    {
        //if(SaveVariables.inst.MostrarPantallaDerrota)
        //{
        //    //Poner pantalla de derrota
        //    fondoAnim.SetBool("Derrota", true);
        //}

        //else if(SaveVariables.inst.GanoElJuego)
        //{
        //    //Poner pantalla de victoria
        //    fondoAnim.SetBool("Victoria", true);
        //}
    }
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void JugarBorrar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void DesactivarPanelInicio()
    {
        panelAnimator.SetBool("desactivarPanel", true);
    }

    void ReproducirMusicaMenuPrincipal()
    {
        if(!iniciarMusica && !musicAS.isPlaying)
        {
            iniciarMusica = true;
            musicAS.PlayOneShot(musicaMenuAudioClip, 0.85f);
        }
        else if(!musicAS.isPlaying && iniciarMusica)
        {
            musicAS.PlayOneShot(musicaMenuAudioClip, 0.85f);
        }
    }

    public void ResetearTimeScale()
    {
        Time.timeScale = 1.0f;
    }
    public void IniciarMusicaMenuPrincipal() //debe ir cuando desaparezca la pantalla
    {
        iniciarMusica = true;
    }

    public void IniciarAnimDesaparicionPantalla()
    {
        panelAnimator.SetBool("activarPanel", true);
    }

    public void CargarEscenaDiapositivas() //ACa debe cargar el nivel 1
    {
        SceneManager.LoadScene(1);
    }
    public void Salir()
    {
        
        Application.Quit();
    } 

    public void IniciarCorrutina() //Al comienzo de desaparecer
    {
        StartCoroutine(ApagarMusica());
    }

    IEnumerator ApagarMusica()
    {   while (musicAS.volume > 0.01f)
        {
            musicAS.volume -= velocidadDisminuidor;
            
            yield return new WaitForSeconds(velCorrutina);
        } 
    }
}