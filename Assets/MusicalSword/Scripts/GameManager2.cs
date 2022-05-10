using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [Header("Tiempo antes de que cargue la escena")]
    [SerializeField] float tiempoReinicio;

    [Header("GameObject del canvas de derrota")]
    [SerializeField] GameObject canvasDerrotaGo;

    [Header("Animator del jugador")]
    [SerializeField] Animator jugadorAnimator;

    [Header("Animator texto de derrota")]
    [SerializeField] Animator textDerrotaAnim;


    [Header("Animator del menu de pausa ")]
    [SerializeField] Animator panelAnim;
    [SerializeField] Animator botonRetryPausa;
    [SerializeField] Animator botonOptionsPausa;
    [SerializeField] Animator botonMenuPausa;

    [Header("Animator del menu de derrota ")]
    [SerializeField] Animator panelAnimDerrota;
    [SerializeField] Animator botonRetryPausaDerrota;
    [SerializeField] Animator botonOptionsPausaDerrota;
    [SerializeField] Animator botonMenuPausaDerrota;

    [Header("GameObject del canvas de menu Pausa")]
    [SerializeField] GameObject canvasMenuPausa;

    [Header("Animator del menu de opciones ")]
    [SerializeField] Animator menuOpcionesAnim;

    [Header("Animator del texto Nivel 1 ")]
    [SerializeField] Animator nivel1TextoAnim;

    [Header("Animator del panel desaparecer")]
    [SerializeField] Animator panelDesaparecerAnim;

    [Header("Animator del fondo de nivel")]
    [SerializeField] Animator fondoNivelAnim;

    [Header("Animator de la Ui")]
    [SerializeField] Animator indicadorEspadaRojaAnim;
    [SerializeField] Animator indicadorEspadaAzulAnim;
    [SerializeField] Animator indicadorEspadaAmarillaAnim;
    [SerializeField] Animator indicadorEspadaVerdeAnim;

    [Space]
    [SerializeField] Animator teclaRojaAnim;
    [SerializeField] Animator teclaAzulAnim;
    [SerializeField] Animator teclaAmarillaAnim;
    [SerializeField] Animator teclaVerdeAnim;

    [Space]
    [SerializeField] Animator barraDeVidaJugadorAnim;
    [Space]
    [SerializeField] Animator indicadorNotaRojaAnim;
    [SerializeField] Animator indicadorNotaAzulAnim;
    [SerializeField] Animator indicadorNotaAmarillaAnim;
    [SerializeField] Animator indicadorNotaVerdeAnim;

    [Space]
    [SerializeField] Animator panelCargarMenu;

    [Space]
    [SerializeField] Animator botonMenuVictoria;
    [SerializeField] GameObject botonMenuVictoriaGo;

    [Space]
    [SerializeField] Animator textoDerrotaAnim;


    private JugadorController jugadorController;

    private AudioController audioController;

    private bool juegoActivo;
    private bool juegoPausado;
    private bool menuOpcionesAbierto;

    private int tipoBotonOpciones;

    public bool JuegoPausado { get => juegoPausado;}

    private void Awake()
    {
        jugadorController = FindObjectOfType<JugadorController>();

        audioController = FindObjectOfType<AudioController>();

        tipoBotonOpciones = 1;
    }


    public bool JuegoActivo { get => juegoActivo; set => juegoActivo = value; }

    private void Start()
    {
        //IniciarJuego();

        IniciarDesaparecerPantalla();
    }

    private void Update()
    {
        AbrirYCerrarInterfaz();
        CerrarMenuOpcionesTecla();
        PerderJuego();
    }
    public void ActivarBotonMenuEnVictoria()
    {
        if(!botonMenuVictoriaGo.activeInHierarchy)
        {
            botonMenuVictoriaGo.SetActive(true);
            botonMenuVictoria.SetBool("aparecerBoton", true);
            //activar anim de boton de menu
        }
    }
    public void CargarEscenaMenu() //Debe estar al final de la anim del panel cargar menu
    {
        SceneManager.LoadScene("Menu");
    }
    public void ActivarPanelCargarMenu() //Se debe iniciar cuando se apreta el boton menu
    {
        panelCargarMenu.SetBool("activarPanel", true);
    }
    public void ActivarPanelCargarMenu(Animator panel) //Se debe iniciar cuando se apreta el boton menu
    {
        panel.SetBool("activarPanel", true);
    }
    public void DesactivarUIVictoria()
    {
        indicadorEspadaRojaAnim.SetBool("desactivarEspada", true);
        indicadorEspadaAzulAnim.SetBool("desactivarEspada", true);
        indicadorEspadaAmarillaAnim.SetBool("desactivarEspada", true);
        indicadorEspadaVerdeAnim.SetBool("desactivarEspada", true);

        teclaRojaAnim.SetBool("desativarTecla", true);
        teclaAzulAnim.SetBool("desativarTecla", true);
        teclaAmarillaAnim.SetBool("desativarTecla", true);
        teclaVerdeAnim.SetBool("desativarTecla", true);

        barraDeVidaJugadorAnim.SetBool("desactivarBarra", true);

        indicadorNotaRojaAnim.SetBool("desactivarNota", true);
        indicadorNotaAzulAnim.SetBool("desactivarNota", true);
        indicadorNotaAmarillaAnim.SetBool("desactivarNota", true);
        indicadorNotaVerdeAnim.SetBool("desactivarNota", true);
    }
    public void ActivarVictoriaAnim()
    {
        fondoNivelAnim.SetBool("activarVictoria", true);
    }

    public void ActivarFondoJefeFinal()
    {
        fondoNivelAnim.SetBool("fondoFinal", true);
    }

    void PerderJuego()
    {
        if (!jugadorController.JugadorVivo)
        {
            //iniciar anim de derrota, y mostrar menu de reintento
            //El monstruo debe dejar de atacar
            
            //El monstruo sigue atacando, desactivar ataque?, poner un bool de que si el jugador este muerto, se detiene el ataque
            canvasDerrotaGo.SetActive(true);
            jugadorAnimator.SetBool("activarMuerte", true);
        }
    }

    public void ActivarMenuOpciones()
    {
        menuOpcionesAnim.SetBool("activarMenu", true);

        botonRetryPausa.SetBool("desactivarBoton", true);
        botonOptionsPausa.SetBool("desactivarBoton", true);
        botonMenuPausa.SetBool("desactivarBoton", true);

        botonRetryPausa.SetBool("aparecerBoton", false);
        botonOptionsPausa.SetBool("aparecerBoton", false);
        botonMenuPausa.SetBool("aparecerBoton", false);
        menuOpcionesAbierto = true;

        //Desactivar el texto que parpadea

        //textoDerrotaAnim.SetBool("desactivarTexto", true);
    }

    public void ActivarMenuOpcionesDerrota() //Se activa cuando se apreta el boton opciones
    {
        textoDerrotaAnim.SetBool("desactivarTexto", true);
        textoDerrotaAnim.SetBool("activarTexto", false);
        menuOpcionesAnim.SetBool("activarMenu", true);

        botonRetryPausaDerrota.SetBool("desactivarBoton", true);
        botonOptionsPausaDerrota.SetBool("desactivarBoton", true);
        botonMenuPausaDerrota.SetBool("desactivarBoton", true);

        botonRetryPausaDerrota.SetBool("aparecerBoton", false);
        botonOptionsPausaDerrota.SetBool("aparecerBoton", false);
        botonMenuPausaDerrota.SetBool("aparecerBoton", false);
        menuOpcionesAbierto = true;
    }

    public void IdentificarBoton(int i) //1 para el menu de pausa, 2 para el menu derrota
    {
        tipoBotonOpciones = i;
    }

    public void ActivarBotonesMenuBack() //Se ejecuta cuando se oculta el menu de opciones de volumen
    {
        switch(tipoBotonOpciones)
        {
            case 1:
                ActivarBotonesMenuPausa();
                break;

            case 2: //Menu derrota
                botonRetryPausaDerrota.SetBool("desactivarBoton", false);
                botonOptionsPausaDerrota.SetBool("desactivarBoton", false);
                botonMenuPausaDerrota.SetBool("desactivarBoton", false);


                botonRetryPausaDerrota.SetBool("aparecerBoton", true);
                botonOptionsPausaDerrota.SetBool("aparecerBoton", true);
                botonMenuPausaDerrota.SetBool("aparecerBoton", true);

                menuOpcionesAbierto = false;

                textoDerrotaAnim.SetBool("desactivarTexto", false);
                textoDerrotaAnim.SetBool("activarTexto", true);
                //Activar texto
                break;
        }

    }

    public void ActivarBotonesMenuPausa() //Se ejecuta cuando se oculta el menu de opciones de volumen
    {

        botonRetryPausa.SetBool("desactivarBoton", false);
        botonOptionsPausa.SetBool("desactivarBoton", false);
        botonMenuPausa.SetBool("desactivarBoton", false);


        botonRetryPausa.SetBool("aparecerBoton", true);
        botonOptionsPausa.SetBool("aparecerBoton", true);
        botonMenuPausa.SetBool("aparecerBoton", true);

        menuOpcionesAbierto = false;
    }

    public void DesactivarMenuOpciones() //Requiere una version para el menu de derrota
    {
        menuOpcionesAnim.SetBool("activarMenu", false);
    }
    public void ActivarTextoDerrota() //Es activado cuando termina la animacion de muerte
    {
        textDerrotaAnim.SetBool("activarTexto", true);
    }

    public void ActivarBotonDerrota(Animator anim) //Es activado cuando aparece el texto y cuando aparece otro boton
    {
        anim.SetBool("aparecerBoton", true);
    }

    public void CargarEscena(string scn)
    {
        SceneManager.LoadScene(scn);
    }


    public void IniciarCorrutinaRecargarEscena()
    {
        StartCoroutine(RecargarLaEscena());
    }

    IEnumerator RecargarLaEscena()
    {
        yield return new WaitForSeconds(tiempoReinicio);
        ReiniciarElJuego();
    }

    //Requiere poner el nombre de la escena en el inspector del boton de menu para que funcione
    public void IrAlMenu(string scn)
    {
        SceneManager.LoadScene(scn);
    }

    void ReiniciarElJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void IniciarDesaparecerPantalla()
    {
        panelDesaparecerAnim.SetBool("iniciarDesaparecer", true);
    }

    public void IniciarAnimAparecerTexto()
    {
        nivel1TextoAnim.SetBool("aparecerTexto", true);
    }
    public void IniciarJuego() //inicia cuando desaparece el texto nivel 1 
    {
        juegoActivo = true;
    }




    public void AbrirYCerrarInterfaz()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !juegoPausado)
        {
            canvasMenuPausa.SetActive(true);
            panelAnim.SetBool("aparecerPanel", true);

            botonRetryPausa.SetBool("desactivarBoton", false);
            botonOptionsPausa.SetBool("desactivarBoton", false);
            botonMenuPausa.SetBool("desactivarBoton", false);


            botonRetryPausa.SetBool("aparecerBoton", true);
            botonOptionsPausa.SetBool("aparecerBoton", true);
            botonMenuPausa.SetBool("aparecerBoton", true);
            //Iniciar anim de panel
            //principalSource.Pause();
            //menuSource.PlayOneShot(menuOpciones, 0.5f);
            //Cursor.visible = true;
            //pauseMenu.SetActive(true);

            audioController.PausarMusica();
            Time.timeScale = 0.0f;
            juegoPausado = true;
        }



        else if (Input.GetKeyDown(KeyCode.Escape) && juegoPausado && !menuOpcionesAbierto)
        {
            audioController.ReanudarMusica();
            ReanudarJuego();
            //botonRetryPausa.SetBool("desactivarBoton", true);
            //botonOptionsPausa.SetBool("desactivarBoton", true);
            //botonMenuPausa.SetBool("desactivarBoton", true);

            //botonRetryPausa.SetBool("aparecerBoton", false);
            //botonOptionsPausa.SetBool("aparecerBoton", false);
            //botonMenuPausa.SetBool("aparecerBoton", false);

            //panelAnim.SetBool("aparecerPanel", false);
            //canvasMenuPausa.SetActive(false);
            //Time.timeScale = 1f;
            //juegoPausado = false;

        }

    }

    public void CerrarMenuOpcionesTecla()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && juegoPausado && menuOpcionesAbierto)
        {
            
            DesactivarMenuOpciones();
        }
    }

    public void ReanudarJuego()
    {
        botonRetryPausa.SetBool("desactivarBoton", true);
        botonOptionsPausa.SetBool("desactivarBoton", true);
        botonMenuPausa.SetBool("desactivarBoton", true);

        botonRetryPausa.SetBool("aparecerBoton", false);
        botonOptionsPausa.SetBool("aparecerBoton", false);
        botonMenuPausa.SetBool("aparecerBoton", false);

        panelAnim.SetBool("aparecerPanel", false);
        canvasMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }
}