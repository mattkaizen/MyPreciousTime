using UnityEngine;
using UnityEngine.UI;

public class JugadorController : MonoBehaviour
{
    /*Este script se encarga de:
    * 1- Registrar inputs del jugador
    * 2- Comunicar al script EspadaController que boton se apreto
    */
    [Header("Vida del jugador")]
    [SerializeField] float vidaMin;
    [SerializeField] float vidaMax;
    [SerializeField] float vidaActual;

    [SerializeField] Slider vidaSlider;
    [SerializeField] Gradient vidaGrandient;
    [SerializeField] Image vidaRelleno;

    [Header("Dmg Que Recibe el jugador")]
    [SerializeField] float dmgRecibido;

    [Header("Curacion que recibe por bonus el jugador")]
    [SerializeField] float curacionBonus;

    private GameManager2 gameManager;
    private EspadaController espadaController;

    private KeyCode botonAtaque1;
    private KeyCode botonAtaque2;
    private KeyCode botonAtaque3;
    private KeyCode botonAtaque4;

    private int tipoDeEspada;

    private bool jugadorVivo;
 


    public bool JugadorVivo { get => jugadorVivo; }
    public int TipoDeEspada { get => tipoDeEspada; }
    public float DmgRecibido { get => dmgRecibido; set => dmgRecibido = value; }
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager2>();
        espadaController = FindObjectOfType<EspadaController>();

        botonAtaque1 = KeyCode.A;
        botonAtaque2 = KeyCode.S;
        botonAtaque3 = KeyCode.F; //verde
        botonAtaque4 = KeyCode.D; //amarillo

        //dmgRecibido = 25.00f;

        vidaActual = vidaMax;
        jugadorVivo = true;

        PonerVidaMaximaSlider();
    }


    private void Update()
    {
        RegistrarInput();
        RegistrarVidaJugador();
        PonerVidaSlider();

        //Debug.Log("Vida jugador: " + vidaActual);
    }

    void PonerVidaMaximaSlider()
    {
        vidaSlider.maxValue = vidaMax;
        vidaSlider.value = vidaMax;

        vidaRelleno.color = vidaGrandient.Evaluate(1f);
    }

    public void PonerVidaSlider()
    {
        vidaSlider.value = vidaActual;

        vidaRelleno.color = vidaGrandient.Evaluate(vidaSlider.normalizedValue);
    }

    void RegistrarInput() //Se puede spamear los botones, si se quiere controlar se puede hacer con un timer o corrutina y un bool
    {
        if (gameManager.JuegoActivo && !gameManager.JuegoPausado)
        {
            if (Input.GetKeyDown(GuardarVariables.inst.BotonAtaque1/*botonAtaque1*/)) //A roja
            {
                tipoDeEspada = 1;
                espadaController.ElegirAtaque(tipoDeEspada);
            }
            if (Input.GetKeyDown(GuardarVariables.inst.BotonAtaque2/*botonAtaque2*/)) //S Azul
            {
                tipoDeEspada = 2;
                espadaController.ElegirAtaque(tipoDeEspada);
            }
            if (Input.GetKeyDown(GuardarVariables.inst.BotonAtaque3/*botonAtaque3*/)) //D Verde
            {
                tipoDeEspada = 3;
                espadaController.ElegirAtaque(tipoDeEspada);
            }
            if (Input.GetKeyDown(GuardarVariables.inst.BotonAtaque4/*botonAtaque4*/)) //W Amarilla
            {
                tipoDeEspada = 4;
                espadaController.ElegirAtaque(tipoDeEspada);
            }
        }
    }

    void RegistrarVidaJugador()
    {
        if(vidaActual <= vidaMin && gameManager.JuegoActivo && jugadorVivo)
        {
            jugadorVivo = false; //iniciar una animacion de derrota, y un menu de reintentar
            
        }
    }

    public void CurarVidaJugador()
    {
        vidaActual += curacionBonus;
    }

    public void CambiarDmgRecibido(float dmg)
    {
        dmgRecibido = dmg;
    }

    public void RestarVidaJugador(float vid)
    {
        vidaActual -= vid;
    }
    public void RestarVidaJugador()
    {
        vidaActual -= dmgRecibido;
        espadaController.BonusAcierto = 0;
    }
}