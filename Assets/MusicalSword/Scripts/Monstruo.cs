using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Monstruo : MonoBehaviour
{
    /*Este script se encarga de:
     * 1-Asignar la vida maxima y minima del monstruo
     * 2-Actualizar la vida actual del monstruo
     * 3-Cambiar el estado del monstruo: Vivo o muerto
     */

    [Header("Vida del Monstruo")]
    [SerializeField] float vidaMax;
    [SerializeField] float vidaActual;
    [SerializeField] float vidaMin;

    [Header("Ataque del Monstruo")]
    [SerializeField] float dmgMonstruo;
    [SerializeField] float velocidadAtaqueActual;
    [SerializeField] float velocidadAtaqueMax;
    [SerializeField] float velocidadAtaqueMin;
    [SerializeField] float periodoDeAtaque;


    [Header("Audioclips de ataques del Monstruo")]
    [SerializeField] AudioClip monstruoAtaqueAC1;
    [SerializeField] AudioClip monstruoAtaqueAC2;
    [SerializeField] AudioClip monstruoAtaqueAC3;
    [SerializeField] AudioClip monstruoAtaqueAC4;

    [Header("Audioclips de Jefe final")]
    [SerializeField] AudioClip gritoJefeFinal;

    [Header("Nota Musical de Ataque")]
    [SerializeField] MoverNotaMusical moverNotaMusical;

    [Header("Matar al monstruo")]
    [SerializeField] bool monstruoActivo;

    [Header("Animaciones del monstruo")]
    [SerializeField] Animator monstruoAnimator;
    [SerializeField] float vidaMinAnim; //Vida minima para activar la animacion de dmg

    [Header("GameObject del monstruo")]
    [SerializeField] GameObject monstruoGo;

    [Header("SpriteRenderer del monstruo")]
    [SerializeField] SpriteRenderer monstruoSpriteR;

    [Header("Jefe Final")]
    [SerializeField] GameObject eventoSliderGo;
    [SerializeField] Slider jefeFinalVidaSlider;
    [SerializeField] Slider eventoSlider;
    [SerializeField] Image rellenoEvento;

    [SerializeField] Animator eventoAnimator;


    private AudioSource monstruoAudioS;



    private bool monstruoVivo; //Necesario para la animacion de muerte, al final de la anim monstruoActivo = false
    private bool inicioMuerte;

    private bool primerAtaque;
    private bool segundoAtaque;
    private bool tercerAtaque;
    private bool cuartoAtaque;

    private bool aparecioJefe;
    private bool gritoBossFinalEmitido;
    private bool eventoActivo;
    private bool marcaEnMedio;

    private int tipoDeAtaque;
    private int tipoDeAtaqueActivo;

    private EspadaController espadaController;


    public AudioSource MonstruoAudioS { get => monstruoAudioS; set => monstruoAudioS = value; }
    public MoverNotaMusical MoverNotaMusical { get => moverNotaMusical; set => moverNotaMusical = value; }
    public Animator MonstruoAnimator { get => monstruoAnimator; set => monstruoAnimator = value; }
    public SpriteRenderer MontruoSpriteR { get => monstruoSpriteR; set => monstruoSpriteR = value; }

    public float VelocidadAtaqueActual { get => velocidadAtaqueActual; }
    public float DmgMonstruo { get => dmgMonstruo; set => dmgMonstruo = value; } //Si es el jefe (bool), se activa un daño especial

    public bool MonstruoVivo { get => monstruoVivo; set => monstruoVivo = value; }
    public bool MonstruoActivo { get => monstruoActivo; set => monstruoActivo = value; }

    public bool PrimerAtaque { get => primerAtaque; }
    public bool SegundoAtaque { get => segundoAtaque; }
    public bool Tercertaque { get => tercerAtaque; }
    public bool CuartoAtaque { get => cuartoAtaque; }

    public bool MarcaEnMedio { get => marcaEnMedio; }
    public bool EventoActivo { get => eventoActivo; }

    private void Awake()
    {
        monstruoAudioS = GetComponent<AudioSource>();
        //monstruoSpriteR = GetComponent<SpriteRenderer>(); //Requiere SerializeField

        espadaController = FindObjectOfType<EspadaController>();

        vidaMin = 0.0f;
        vidaMinAnim = 25.0f;
        velocidadAtaqueActual = 1.0f;

        //monstruoVivo = true;
    }

    private void Update()
    {
        ConsultarEstadoMonstruo();
        AparecerJefeFinal();
        ObtenerTipoAtaqueActivo();
        ActualizarJefeSlider();
        RastrearSliderValue();

    }

    public void RastrearSliderValue()
    {
        if (eventoActivo)
        {
            if (eventoSlider.value < 0.55f && eventoSlider.value > 0.45f)
            {
                marcaEnMedio = true;
            }
            else
                marcaEnMedio = false;
            // Centro 0,35(izquierd), 0,67 derecha
        }
    }

    void ActualizarJefeSlider()
    {
        if (aparecioJefe)
        {
            jefeFinalVidaSlider.value = vidaActual;
        }
    }
    public void PonerVidaMaximaJefeSlider() //Se debe poner al final de la anim aparecer
    {
        jefeFinalVidaSlider.maxValue = vidaMax;
        jefeFinalVidaSlider.value = vidaMax;
    }

    public void AparecerJefeFinal() //Tal vez se deba ejecutar en un update
    {
        if (gritoBossFinalEmitido && !monstruoAudioS.isPlaying && !aparecioJefe) // y bool !aparecio jefe
        {
            aparecioJefe = true;
            PonerVidaMaximaJefeSlider();
            monstruoAnimator.SetBool("activarBoss", true);
            //Activar Barra de vida
        }
    }

    public void ActivarEvento() //Debe activarse cuando la vida sea menor que 50
    {
        if (!eventoActivo)
        {
            //monstruoVivo = false;

            eventoActivo = true; //Cuando se acierta el golpe se desactiva el evento, cuando desaparece la barra tal vez

            eventoAnimator.SetBool("aparecerBarra", true);
        }
    }

    public void DesactivarEvento() //Se desactiva cuando el jugador acierta
    {
        eventoAnimator.SetBool("desactivarBarra", true);
    }


    public void EmitirGritoBossFinal() //Se ejecuta al final de la aparicion de la pantalla negra3
    {
        if (!monstruoAudioS.isPlaying && !gritoBossFinalEmitido)
        {
            EmitirGritoJefe();
            gritoBossFinalEmitido = true;
        }
    }

    /// <summary>
    /// Cambia el estado del monstruo si la vida actual es menor o igual a la vida minima
    /// </summary>
    void ConsultarEstadoMonstruo()
    {
        if (vidaActual <= vidaMin && monstruoVivo)
        {
            
            monstruoVivo = false;
            ActivarAnimMuerte();
            //monstruoActivo = false;
        }
    }

    public void ActivarFaseCombate()
    {
        //Cuando termina la anim de aparicion, empieza a atacar (corrutina de ataque)
        monstruoVivo = true;
        StartCoroutine(IniciarAtaque());
    }
    void ActivarAnimMuerte()
    {
        if (!MonstruoVivo && !inicioMuerte)
        {
            
            inicioMuerte = true;
            monstruoAnimator.SetBool("matarMonstruo", true);
            //monstruoAnimator.SetInteger("tipoMonstruo",  )
        }
    }

    public void EmitirGritoJefe()
    {
        monstruoAudioS.PlayOneShot(gritoJefeFinal);
    }

    public void ActivarAnimDmg()
    {
        if (vidaMax > vidaMinAnim && vidaActual > vidaMinAnim)
        {

            monstruoAnimator.SetBool("recibirAtk", true);
        }
    }

    public void ResetearAnimDmg()
    {
        monstruoAnimator.SetBool("recibirAtk", false);
    }
    public void DesactivarMonstruo() //Se desactiva cuando termina la animacion de desaparecer
    {
        monstruoActivo = false;
        ObjectPooler.SharedInstance.RemoverMonstruoMuertoLista(); //No es necesario
        monstruoGo.SetActive(false); //Si se elimina la linea de arriba, y se deja esta, se rompe la cadena
    }

    public void PonerVidaMaxima(float hth)
    {
        vidaMax = hth;
        vidaActual = vidaMax;
        monstruoAnimator.SetFloat("vidaActual", vidaActual);
    }

    /// <summary>
    /// Se resta la vida actual del monstruo según el daño recibido
    /// </summary>
    /// <param name="dañoRecibido"></param>
    public void RestarVida(float dañoRecibido)
    {
        vidaActual -= dañoRecibido;
        monstruoAnimator.SetFloat("vidaActual", vidaActual);
    }

    public void ReproducirSonidoAtaque(ref AudioSource atkAS, ref AudioClip atkAC)
    {
        atkAS.PlayOneShot(atkAC);
    }

    void ElegirTipoAtaque()
    {
        tipoDeAtaque = Random.Range(1, 5); //Si tengo 4 tipos de ataque
        switch (tipoDeAtaque)
        {
            case 1: // rojo

                espadaController.NumGolpes = 0;
                StartCoroutine(Atacando(() => ActivarBool(out primerAtaque), () => DesactivarBool(out primerAtaque)));
                moverNotaMusical.ElegirNotaRoja();
                ReproducirSonidoAtaque(ref monstruoAudioS, ref monstruoAtaqueAC1);
                break;
            case 2: // azul

                espadaController.NumGolpes = 0;
                StartCoroutine(Atacando(() => ActivarBool(out segundoAtaque), () => DesactivarBool(out segundoAtaque)));
                //moverNotaMusical.ModificarColor(tipoDeAtaque);
                //moverNotaMusical.DecidirIzquierdaODerecha();
                moverNotaMusical.ElegirNotaAzul();
                ReproducirSonidoAtaque(ref monstruoAudioS, ref monstruoAtaqueAC2);
                break;
            case 3: // verde

                espadaController.NumGolpes = 0;
                StartCoroutine(Atacando(() => ActivarBool(out tercerAtaque), () => DesactivarBool(out tercerAtaque)));
                //moverNotaMusical.ModificarColor(tipoDeAtaque);
                //moverNotaMusical.DecidirIzquierdaODerecha();
                moverNotaMusical.ElegirNotaVerde();
                ReproducirSonidoAtaque(ref monstruoAudioS, ref monstruoAtaqueAC3);
                break;
            case 4: // amarillo
                espadaController.NumGolpes = 0;
                StartCoroutine(Atacando(() => ActivarBool(out cuartoAtaque), () => DesactivarBool(out cuartoAtaque)));
                //moverNotaMusical.ModificarColor(tipoDeAtaque);
                //moverNotaMusical.DecidirIzquierdaODerecha();
                moverNotaMusical.ElegirNotaAmarilla();
                ReproducirSonidoAtaque(ref monstruoAudioS, ref monstruoAtaqueAC4);
                break;
        }
    }
    public void Atacar()
    {
        ElegirTipoAtaque();
    }

    private void ActivarBool(out bool atk)
    {
        atk = true;
    }
    private void DesactivarBool(out bool atk)
    {
        atk = false;
    }

    /// <summary>
    /// Activo un bool, espero el tiempo indicado por periodoDeAtaque y desactivo el mismo bool
    /// </summary>
    /// <param name="metodoAct"></param>
    /// <param name="metodoDes"></param>
    /// <returns></returns>
    IEnumerator Atacando(Action metodoAct, Action metodoDes)
    {
        metodoAct();
        yield return new WaitForSeconds(periodoDeAtaque);
        metodoDes();
        espadaController.InfligirDmgPorNoApretar();
        espadaController.NumFallos = 0;
        /*espadaController.GolpeAcertado = false;*/ //se debe reiniciar en la mitad de la anim aparecer
        espadaController.NumGolpes = 0; //El jugador puede meter otro ataque luego de que termine el periodo de ataque del monstruo

        //NumGolpes debe reniciarse cuando termina La anim de desaparecer y la de explosion
    }

    public int ObtenerTipoAtaqueActivo()
    {
        //for(int i = 0; i < 4; i++)
        //{
        if (primerAtaque)
        {
            return tipoDeAtaqueActivo = 1;
        }
        else if (segundoAtaque)
        {
            return tipoDeAtaqueActivo = 2;
        }
        else if (tercerAtaque)
        {
            return tipoDeAtaqueActivo = 3;
        }
        else if (cuartoAtaque)
        {
            return tipoDeAtaqueActivo = 4;
        }
        //}
        return 0;
    }

    public IEnumerator IniciarAtaque()
    {
        while (/*monstruoActivo*/monstruoVivo && !eventoActivo)
        {
            Atacar();
            yield return new WaitForSeconds(velocidadAtaqueActual);
            espadaController.GolpeFallado = false;
        }
    }
}