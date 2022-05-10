using System.Collections;
using UnityEngine;

public class EspadaController : MonoBehaviour
{
    [Header("GameObjects de las espadas")]
    [SerializeField] GameObject primeraEspadaGo;
    [SerializeField] GameObject segundaEspadaGo;
    [SerializeField] GameObject terceraEspadaGo;
    [SerializeField] GameObject cuartaEspadaGo;

    [Header("Audioclips de ataques de espadas")]
    [SerializeField] AudioClip primeraEspadaAtaqueAC;
    [SerializeField] AudioClip segundaEspadaAtaqueAC;
    [SerializeField] AudioClip terceraEspadaAtaqueAC;
    [SerializeField] AudioClip cuartaEspadaAtaqueAC;

    [Header("Periodo de registro de ataque del jugador")]

    [SerializeField] float periodoRegistroAtaque;

    [Header("Controlador de animaciones de jugador")]
    [SerializeField] Animator jugadorAnimator;

    private AudioSource primeraEspadaAudioS;
    private AudioSource segundaEspadaAudioS;
    private AudioSource terceraEspadaAudioS;
    private AudioSource cuartaEspadaAudioS;

    private Espada primeraEspada;
    private Espada segundaEspada;
    private Espada terceraEspada;
    private Espada cuartaEspada;

    private MonstruoController monstruoController;
    private JugadorController jugadorController;
    private GameManager gameManager;


    private bool golpeAcertado;
    private bool golpeFallado;
    private bool recibioGolpe;

    private int bonusAcierto; //Si acierto 3 golpes seguidos, me curo 25 puntos de vida y se resetea el bonus, 3 golpes sin fallar.

    private int numGolpes; //Este valor va a valer 0 cuando el monstruo termine de atacar, para evitar recibir 2 hit de un mismo intercambio de daño

    private int numGolpesDados;
    private int numFallos;

    private int numFallosEvento; //Se reinicia cuando termina de atacar el jugador

    public int NumFallosEvento { get => numFallosEvento; set => numFallosEvento = value; }
    public int NumGolpesDados { get => numGolpesDados; set => numGolpesDados = value; }
    public int BonusAcierto { get => bonusAcierto; set => bonusAcierto = value; }
    public int NumFallos { get => numFallos; set => numFallos = value; }
    public int NumGolpes { get => numGolpes; set => numGolpes = value; }
    public bool GolpeFallado { get => golpeFallado; set => golpeFallado = value; }
    public bool GolpeAcertado { get => golpeAcertado; set => golpeAcertado = value; }
    public bool RecibioGolpe { get => recibioGolpe; set => recibioGolpe = value; }



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        primeraEspada = primeraEspadaGo.GetComponent<Espada>();
        segundaEspada = segundaEspadaGo.GetComponent<Espada>();
        terceraEspada = terceraEspadaGo.GetComponent<Espada>();
        cuartaEspada = cuartaEspadaGo.GetComponent<Espada>();

        primeraEspadaAudioS = primeraEspadaGo.GetComponent<AudioSource>();
        segundaEspadaAudioS = segundaEspadaGo.GetComponent<AudioSource>();
        terceraEspadaAudioS = terceraEspadaGo.GetComponent<AudioSource>();
        cuartaEspadaAudioS = cuartaEspadaGo.GetComponent<AudioSource>();

        monstruoController = FindObjectOfType<MonstruoController>();

        jugadorController = FindObjectOfType<JugadorController>();

        golpeAcertado = false;

        numGolpes = 0;
    }

    private void Update()
    {
        CompararAtaquesApretados3();
        CompararAtaqueEventoFinal();
        BonusPorAcertar();
    }
    public void ElegirAtaque(int atk)
    {
        switch (atk)
        {
            case 1: //iniciar una corrutina y tomar la velocidad de ataque del jugador
                IniciarCorrutinaAtaque(primeraEspada, primeraEspadaAudioS, ref primeraEspadaAtaqueAC);
                break;
            case 2:
                IniciarCorrutinaAtaque(segundaEspada, segundaEspadaAudioS, ref segundaEspadaAtaqueAC);
                break;
            case 3:
                IniciarCorrutinaAtaque(terceraEspada, terceraEspadaAudioS, ref terceraEspadaAtaqueAC);
                break;
            case 4:
                IniciarCorrutinaAtaque(cuartaEspada, cuartaEspadaAudioS, ref cuartaEspadaAtaqueAC);
                break;
        }
    }

    void BonusPorAcertar()
    {
        //Debug.Log("bonus acierto " + bonusAcierto);
        if (bonusAcierto >= 5)
        {
            //Falta sonido de curacion, imagen visual de que me curo o algo
            
            jugadorController.CurarVidaJugador();
            bonusAcierto = 0;
        }
    }

    //Crear un switch con el tipo de espada y que este todo el tiempo comparando
    void CompararAtaquesApretados()
    {
        switch (jugadorController.TipoDeEspada) // Si aprieta la A, va al caso 1
        {
            case 1: //podria ser numero de golpes, al terminar el periodo de ataque del monstruo el num de golpes se resetea 0
                if (primeraEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() == 1 && numGolpes == 0) // falta golpe acertado
                { //
                    

                    //Activar animacion de daño, dentro monstruo, si tiene 25 de vida, no se activa, si es mayor si
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion(jugadorController.TipoDeEspada);

                    bonusAcierto += 1;
                    numGolpes += 1;
                    golpeAcertado = true; //Se reinicia luego de que termine el periodo de ataque del jugador

                }
                //Ataco en 0,1                         por 0,5 s esta devolviendo 0                          
                else if (primeraEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() != 1 /*&& monstruoController.PasarAtaqueMonstruoActivo() != 0*/  && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    //Cuando termina la animacion de fallar el golpe, se puede volver a atacar. Sonido de dolor y pantalla roja con temblor
                    bonusAcierto = 0;
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo()); ;
                    jugadorAnimator.SetBool("activarGolpe", true); // En medio de la anim se resta la vida al jugador
                    golpeFallado = true;
                    recibioGolpe = true;
                }

                //EstaAtacando dura unos segundos activo, AtaqueActivoMonstruo dura unos segundos, golpe fallado se reinicia al final de la anim, golpe acertado = falso
                //Se pasa en un periodo de tiempo de 0,5 se pasa el valor 1, luego se pasa el valor 0.

                break;

            case 2:
                if (segundaEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() == 2 && numGolpes == 0) // falta golpe acertado
                {
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(segundaEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion(jugadorController.TipoDeEspada);
                    bonusAcierto += 1;
                    numGolpes += 1;
                    golpeAcertado = true;
                }
                else if (segundaEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() != 2 && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
            case 3:
                if (terceraEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() == 3 && numGolpes == 0) // falta golpe acertado
                {
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(terceraEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion(jugadorController.TipoDeEspada);
                    bonusAcierto += 1;
                    numGolpes += 1;
                }
                else if (terceraEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() != 3 && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
            case 4:
                if (cuartaEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() == 4 && numGolpes == 0) // falta golpe acertado
                {
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(cuartaEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion(jugadorController.TipoDeEspada);
                    numGolpes += 1;
                    bonusAcierto += 1;
                }
                else if (cuartaEspada.EstaAtacando && monstruoController.PasarTipoAtaqueMonstruoActivo() != 4 && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
        }
    }

    void CompararAtaquesApretados2()
    {
        switch (jugadorController.TipoDeEspada) // Si aprieta la A, va al caso 1
        {
            case 1: //Se ejecuta 2 veces despues de los golpes
                if (primeraEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                    && numGolpes == 0 && numFallos == 0 && !golpeAcertado && !golpeFallado && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla) // Comprobascion de nota elegida (cuando sse elige la nota de color)ss
                { //
                     //Se activo 2 veces
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion2(1);

                    //numGolpesDados = numGolpesDados + 1;
                    bonusAcierto += 1;
                    numGolpes += 1; //Se debe reiniciar el numero de golpes cuando termine la ansim de explosion o desaparezca la nota
                    golpeAcertado = true; //Se reinicia luego de que termine el periodo de ataque del jugador

                }
                else if (primeraEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja && !golpeFallado && !golpeAcertado && numFallos == 0)
                { //falta derrota
                    
                    bonusAcierto = 0;
                    if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                    {
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimDesaparicionFallo();
                    }
                    numFallos += 1; //Se reinicia cuando el monstruo deja de atacar
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo()); ;
                    jugadorAnimator.SetBool("activarGolpe", true); // En medio de la anim se resta la vida al jugador
                    golpeFallado = true; //se debe resetear al final de la explosion de la nota o la de desaparicion o del error
                    recibioGolpe = true;
                }//Poner golpe fallado en la condicion de acierto


                break;

            case 2:
                if (segundaEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado) // falta golpe acertado
                {
                    
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(segundaEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion2(2);
                    bonusAcierto += 1;
                    numGolpes += 1;
                    golpeAcertado = true;
                }
                else if (segundaEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                    {
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimDesaparicionFallo();
                    }
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
            case 3: //Verde
                if (terceraEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado) // falta golpe acertado
                {
                    ;
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(terceraEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion2(3);
                    bonusAcierto += 1;
                    numGolpes += 1;
                    golpeAcertado = true;
                }
                else if (terceraEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                    {
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimDesaparicionFallo();
                    }
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
            case 4:
                if (cuartaEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                    && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja && !golpeFallado) // falta golpe acertado
                {
                    
                    monstruoController.ActivarAnimDmgMonstruoActivo();
                    monstruoController.RestarVidaMonstruoActual(cuartaEspada.DañoActual);
                    monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosion2(4);
                    numGolpes += 1;
                    bonusAcierto += 1;
                    golpeAcertado = true;
                }
                else if (cuartaEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                {
                    bonusAcierto = 0;
                    if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                    {
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimDesaparicionFallo();
                    }
                    numFallos += 1;
                    jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                    jugadorAnimator.SetBool("activarGolpe", true);
                    golpeFallado = true;
                    recibioGolpe = true;
                }
                break;
        }
    }

    void CompararAtaqueEventoFinal()
    {
        if (monstruoController.PasarMonstruoActivo().EventoActivo)
        {
            switch (jugadorController.TipoDeEspada)
            {
                case 1:
                    if (primeraEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {                        //Activar anim de muerte de jefe
                        //
                        monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                        monstruoController.PasarMonstruoActivo().DesactivarEvento();

                        
                    }
                    else if (primeraEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().EventoActivo && !monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);

                        numFallosEvento += 1;

                        
                    }
                    break;
                case 2:
                    if (segundaEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                        monstruoController.PasarMonstruoActivo().DesactivarEvento();

                        
                    }
                    else if (segundaEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().EventoActivo && !monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);

                        numFallosEvento += 1;

                        
                    }
                    break;
                case 3:
                    if (terceraEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                        monstruoController.PasarMonstruoActivo().DesactivarEvento();

                        
                    }
                    else if (terceraEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().EventoActivo && !monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);

                        numFallosEvento += 1;

                        
                    }
                    break;
                case 4:
                    if (cuartaEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                        monstruoController.PasarMonstruoActivo().DesactivarEvento();

                        
                    }
                    else if (cuartaEspada.EstaAtacando && monstruoController.PasarMonstruoActivo().EventoActivo && !monstruoController.PasarMonstruoActivo().MarcaEnMedio && numFallosEvento == 0)
                    {
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);

                        numFallosEvento += 1;

                        
                    }
                    break;
            }// Si aprieta la A, va al caso 1
             //if (monstruoController.PasarMonstruoActivo().EventoActivo && monstruoController.PasarMonstruoActivo().MarcaEnMedio)
             //{

            //}
        }
    }
    void CompararAtaquesApretados3()
    {
        if (!monstruoController.PasarMonstruoActivo().EventoActivo && !gameManager.JuegoPausado)
        {
            switch (jugadorController.TipoDeEspada) // Si aprieta la A, va al caso 1
            {
                case 1: //Se ejecuta 2 veces despues de los golpes
                    if (primeraEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                        && numGolpes == 0 && numFallos == 0 && !golpeAcertado && !golpeFallado && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && monstruoController.PasarNotaMusicalActiva().NotaRojaSeleccionada) // Comprobascion de nota elegida (cuando sse elige la nota de color)ss
                    { //
                         //Se activo 2 veces
                        monstruoController.ActivarAnimDmgMonstruoActivo();
                        monstruoController.RestarVidaMonstruoActual(primeraEspada.DañoActual);
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosionRoja();
                        monstruoController.ActivarSonidoGolpeJefe();

                        //numGolpesDados = numGolpesDados + 1;
                        bonusAcierto += 1;
                        numGolpes += 1; //Se debe reiniciar el numero de golpes cuando termine la ansim de explosion o desaparezca la nota
                        golpeAcertado = true; //Se reinicia luego de que termine el periodo de ataque del jugador

                    }
                    else if (primeraEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja && !golpeFallado && !golpeAcertado && numFallos == 0)
                    { //falta derrota
                        
                        bonusAcierto = 0;
                        if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                        {
                            if (monstruoController.PasarNotaMusicalActiva().NotaRojaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloRojo();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAzulSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAzul();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAmarillaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAmarilla();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaVerdeSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloVerde();
                            }
                        }
                        numFallos += 1; //Se reinicia cuando el monstruo deja de atacar
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true); // En medio de la anim se resta la vida al jugador
                        golpeFallado = true; //se debe resetear al final de la explosion de la nota o la de desaparicion o del error
                        recibioGolpe = true;
                    }//Poner golpe fallado en la condicion de acierto


                    break;

                case 2: //Azul
                    if (segundaEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado && monstruoController.PasarNotaMusicalActiva().NotaAzulSeleccionada) // falta golpe acertado
                    {
                        
                        monstruoController.ActivarAnimDmgMonstruoActivo();
                        monstruoController.RestarVidaMonstruoActual(segundaEspada.DañoActual);
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosionAzul();
                        monstruoController.ActivarSonidoGolpeJefe();
                        bonusAcierto += 1;
                        numGolpes += 1;
                        golpeAcertado = true;
                    }
                    else if (segundaEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                    {
                        bonusAcierto = 0;
                        if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                        {
                            //monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAzul();

                            if (monstruoController.PasarNotaMusicalActiva().NotaAzulSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAzul();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaRojaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloRojo();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAmarillaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAmarilla();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaVerdeSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloVerde();
                            }
                        }
                        numFallos += 1;
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);
                        golpeFallado = true;
                        recibioGolpe = true;
                    }
                    break;
                case 3: //Verde Falta
                    if (terceraEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado && monstruoController.PasarNotaMusicalActiva().NotaVerdeSeleccionada) // falta golpe acertado
                    {
                        
                        monstruoController.ActivarAnimDmgMonstruoActivo();
                        monstruoController.RestarVidaMonstruoActual(terceraEspada.DañoActual);
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosionVerde();
                        monstruoController.ActivarSonidoGolpeJefe();
                        bonusAcierto += 1;
                        numGolpes += 1;
                        golpeAcertado = true;
                    }
                    else if (terceraEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                    {
                        bonusAcierto = 0;
                        if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                        {
                            //monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloVerde();

                            if (monstruoController.PasarNotaMusicalActiva().NotaVerdeSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloVerde();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAzulSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAzul();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAmarillaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAmarilla();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaRojaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloRojo();
                            }
                        }
                        numFallos += 1;
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);
                        golpeFallado = true;
                        recibioGolpe = true;
                    }
                    break;
                case 4:
                    if (cuartaEspada.EstaAtacando && monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && numGolpes == 0 && numFallos == 0 && !golpeAcertado
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAzul
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaVerde
                        && !monstruoController.PasarNotaMusicalActiva().NotaEncimaRoja && !golpeFallado && monstruoController.PasarNotaMusicalActiva().NotaAmarillaSeleccionada) // falta golpe acertado
                    {
                        
                        monstruoController.ActivarAnimDmgMonstruoActivo();
                        monstruoController.RestarVidaMonstruoActual(cuartaEspada.DañoActual);
                        monstruoController.PasarNotaMusicalActiva().ActivarAnimExplosionAmarilla();
                        monstruoController.ActivarSonidoGolpeJefe();
                        numGolpes += 1;
                        bonusAcierto += 1;
                        golpeAcertado = true;
                    }
                    else if (cuartaEspada.EstaAtacando && !monstruoController.PasarNotaMusicalActiva().NotaEncimaAmarilla && !golpeFallado && !golpeAcertado && numFallos == 0) //falta bool 
                    {
                        bonusAcierto = 0;
                        if (numFallos == 0) //Evito que se buge la animacion si spameo el boton 
                        {
                            //monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAmarilla();

                            if (monstruoController.PasarNotaMusicalActiva().NotaAmarillaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAmarilla();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaAzulSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloAzul();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaRojaSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloRojo();
                            }
                            else if (monstruoController.PasarNotaMusicalActiva().NotaVerdeSeleccionada)
                            {
                                monstruoController.PasarNotaMusicalActiva().ActivarAnimFalloVerde();
                            }
                        }
                        numFallos += 1;
                        jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
                        jugadorAnimator.SetBool("activarGolpe", true);
                        golpeFallado = true;
                        recibioGolpe = true;
                    }
                    break;
            }
        }
    }


    public void InfligirDmgPorNoApretar() //Si el jugador esta vivo, recibe daño 
    {
        if (jugadorController.JugadorVivo && !recibioGolpe && !primeraEspada.EstaAtacando && !segundaEspada.EstaAtacando && !terceraEspada.EstaAtacando && !cuartaEspada.EstaAtacando && numFallos == 0 && numGolpes == 0)
        {
            jugadorController.CambiarDmgRecibido(monstruoController.PasarDmgMonstruoActivo());
            jugadorAnimator.SetBool("activarGolpe", true);
        }
        recibioGolpe = false;
    }

    public void ReiniciarBool() //Se ejecuta al final de la animacion de recibir un golpe
    {
        jugadorAnimator.SetBool("activarGolpe", false);
        //golpeFallado = false;

    }

    public void ReiniciarGolpeAcertado() //se pone al final de la anim de explosion, para volver a pegar
    {
        golpeAcertado = false;
    }

    public void ReiniciarGolpes() //Se ejecuta cuando termina de explotar
    {
        numGolpes = 0;

       
    }




    void IniciarCorrutinaAtaque(Espada esp, AudioSource espAS, ref AudioClip atkAC)
    {
        if (!esp.EstaAtacando)
        {
            StartCoroutine(esp.RegistrarAtaque(periodoRegistroAtaque)); //Si no hay una corrutina acti3va, la activo
            ReproducirSonidoAtaque(ref espAS, ref atkAC);
        }
    }
    private void ReproducirSonidoAtaque(ref AudioSource espAS, ref AudioClip atkAC)
    {
        espAS.PlayOneShot(atkAC);
    }
    //Si el jugador tiene muchos fallos, se actualiza el daño de la espada correspondiente
}