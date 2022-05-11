using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Animator del menu de pausa ")]
    [SerializeField] Animator panelAnim;
    [SerializeField] Animator botonRetryPausa;
    [SerializeField] Animator botonOptionsPausa;
    [SerializeField] Animator botonMenuPausa;
    [Space]
    [SerializeField] Animator panelCargarMenu;

    [Header("GameObject del canvas de menu Pausa")]
    [SerializeField] GameObject canvasMenuPausa;

    [Header("Animator del menu de opciones ")]
    [SerializeField] Animator menuOpcionesAnim;

    [Header("Animator del panel inicio nivel ")]
    [SerializeField] Animator panelInicioNivelAnim;

    [Header("Animator del panel cargar proxNivel ")]
    [SerializeField] Animator panelCargarEscenaAnim;

    [Header("Animator de la Camara ")]
    [SerializeField] Animator cameraAnim;

    private PlayerCollision playerCollision;
    private PlayerAnimator playerAnim;
    private AudioController audioController;

    private int numeroDeNivel;
    private int numeroDeFase;
    private int numeroDeEscena;

    private int tipoBotonOpciones;

    private bool juegoActivo;
    private bool seActivoMuerte;
    private bool juegoPausado;
    private bool menuOpcionesAbierto;


    public bool JuegoActivo { get => juegoActivo; }

    private void Awake()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        playerAnim = FindObjectOfType<PlayerAnimator>();
        audioController = FindObjectOfType<AudioController>();
    }
    private void Start()
    {
        ActivarPanelDesaparecer();
    }

    private void Update()
    {
        AbrirYCerrarInterfaz();
        CerrarMenuOpcionesTecla();
        ConsultarNivelYFase();
        RastrearMuerteJugador();
        IrSiguienteEscena();
    }

    public void PasarASiguienteNivel() //Se reproduce al final de panel cargar escena
    {
        switch (numeroDeNivel)
        {
            case 1: //Si estoy en el nivel 1 etapa 1, carga la etapa 2
                if(numeroDeFase == 1) //Carga el nivel 1 fase 2
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                else if (numeroDeFase == 2) //Carga el nivel 1 fase 3
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                else if (numeroDeFase == 3) //Carga el nivel 2
                {
                    SceneManager.LoadScene(numeroDeFase + 1);
                }
                break;
            case 2: //Si estoy en el nivel 1 etapa 1, carga la etapa 2
                if (numeroDeFase == 1) //Carga el nivel 1 fase 2
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                else if (numeroDeFase == 2) //Carga el nivel 1 fase 3
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                else if (numeroDeFase == 3) //Carga el nivel 2
                {
                    SceneManager.LoadScene(numeroDeFase + 3);
                }
                break;
        }
    }
    private void ConsultarNivelYFase()
    {
        switch (SceneManager.GetActiveScene().buildIndex) // El 0 es el menu principal, El 1 es el nivel 1
        {
            // Escena de indice 1 2, 3, es Nivel 1
            case 1:
                numeroDeNivel = 1;
                numeroDeFase = 1;
                numeroDeEscena = 1;
                break;
            case 2:
                numeroDeNivel = 1;
                numeroDeFase = 2;
                numeroDeEscena = 1;
                break;
            case 3:
                numeroDeNivel = 1;
                numeroDeFase = 3;
                numeroDeEscena = 1;
                break;
            // Escena de indice 4, 5, 6, es Nivel 2
            case 4:
                numeroDeNivel = 2;
                numeroDeFase = 1;
                numeroDeEscena = 4;
                break;
            case 5:
                numeroDeNivel = 2;
                numeroDeFase = 2;
                numeroDeEscena = 4;
                break;
            case 6:
                numeroDeNivel = 2;
                numeroDeFase = 3;
                numeroDeEscena = 4;
                break;

            // Escena de indice 7, 8, 9, es Nivel 3
            case 7:
                numeroDeNivel = 3;
                numeroDeFase = 1;
                numeroDeEscena = 7;
                break;
            case 8:
                numeroDeNivel = 3;
                numeroDeFase = 2;
                numeroDeEscena = 7;
                break;
            case 9:
                numeroDeNivel = 3;
                numeroDeFase = 3;
                numeroDeEscena = 7;
                break;
        }
    }

    public void VolverAPrimeraFaseDelNivel() //Se ejecuta al final de la animacion de muerte (evento)
    {
        if (numeroDeNivel == 1)
        {
            SceneManager.LoadScene(numeroDeEscena); //Carga el nivel 1, fase 1
        }
        else if (numeroDeNivel == 2)
        {
            SceneManager.LoadScene(numeroDeEscena); //Carga el nivel 2, fase 1
        }
    }

    public void ActivarTemblorCamara() //Se activa al comienzo de la animacion de muerte
    {
        //Activar animacion de temblor de camara
        cameraAnim.SetBool("Temblor", true);
    }

    public void DesactivarTemblorCamara() //Se activa final de anim de temblor, aca se debe activar panel de perdida
    {
        cameraAnim.SetBool("Temblor", false);
    }

    private void RastrearMuerteJugador()
    {
        if (!playerCollision.EstaVivoJugador && !seActivoMuerte)
        {
            seActivoMuerte = true;

            playerAnim.StartDeathAnim();
            audioController.ReproducirSonidoMuerte();
            //activar animacion de muerte
        }
    }

    //Cargar menu 

    public void CargarNuevaEscena() //Debe estar al final de la anim del panel cargar menu
    {
        SceneManager.LoadScene("Menu");
    }

    public void IniciarJuego() //Se ejecuta casi al final de la animacion del desaparecer panelInicioNivel
    {
        juegoActivo = true;
    }

    public void ActivarPanelDesaparecer() //El panel de inicio de nivel
    {
        panelInicioNivelAnim.SetBool("activarPanel", true);
    }

    public void ActivarPanelCargarNivel() //El panel al cargar una escena
    {
        panelCargarEscenaAnim.SetBool("activarPanel", true);
    }

    public void IrSiguienteEscena() //Se revisa en update
    {
        if (audioController.TerminoSonidos)
        {
            ActivarPanelCargarNivel();
        }
    }

    //MENU PAUSA
    //-----------------------------------------------------------------------------------------------------------------

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

            //audioController.PausarMusica(); // Descomentar esta linea al implementar musica
            Time.timeScale = 0.0f;
            juegoPausado = true;
        }



        else if (Input.GetKeyDown(KeyCode.Escape) && juegoPausado && !menuOpcionesAbierto)
        {
            //audioController.ReanudarMusica();// Descomentar esta linea al implementar musica
            ReanudarJuego();
        }

    }

    public void CerrarMenuOpcionesTecla()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && juegoPausado && menuOpcionesAbierto)
        {

            DesactivarMenuOpciones();
        }
    }
    public void DesactivarMenuOpciones() //Requiere una version para el menu de derrota
    {
        menuOpcionesAnim.SetBool("activarMenu", false);
    }
    public void ActivarBotonesMenuBack() //Se ejecuta cuando se oculta el menu de opciones de volumen
    {
        switch (tipoBotonOpciones)
        {
            case 1:
                ActivarBotonesMenuPausa();
                break;

            //case 2: //Menu derrota
            //    botonRetryPausaDerrota.SetBool("desactivarBoton", false);
            //    botonOptionsPausaDerrota.SetBool("desactivarBoton", false);
            //    botonMenuPausaDerrota.SetBool("desactivarBoton", false);


            //    botonRetryPausaDerrota.SetBool("aparecerBoton", true);
            //    botonOptionsPausaDerrota.SetBool("aparecerBoton", true);
            //    botonMenuPausaDerrota.SetBool("aparecerBoton", true);

            //    menuOpcionesAbierto = false;

            //    textoDerrotaAnim.SetBool("desactivarTexto", false);
            //    textoDerrotaAnim.SetBool("activarTexto", true);
            //    //Activar texto
            //    break;
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
    public void CargarEscenaMenu() //Debe estar al final de la anim del panel cargar menu
    {
        SceneManager.LoadScene("Menu");
    }
    public void ActivarPanelCargarMenu() //Se debe iniciar cuando se apreta el boton menu
    {
        panelCargarMenu.SetBool("activarPanel", true);
    }
    public void IdentificarBoton(int i) //1 para el menu de pausa, 2 para el menu derrota
    {
        tipoBotonOpciones = i;
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

    }
    public void ActivarBotonDerrota(Animator anim) //Es activado cuando aparece el texto y cuando aparece otro boton
    {
        anim.SetBool("aparecerBoton", true);
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