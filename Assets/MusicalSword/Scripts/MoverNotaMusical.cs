using System.Collections.Generic;
using UnityEngine;

public class MoverNotaMusical : MonoBehaviour
{
    //Falta que aparezca una luz blanca alrededor para indicar el acierto de colores


    [Header("Objeto a rotar")]
    [SerializeField] GameObject puntoPivotGo;


    [Header("Objeto a mover")]
    [SerializeField] GameObject notaAMoverGo;

    [Header("SpriteRenderer a cambiar de color")]
    [SerializeField] SpriteRenderer notaSpriteR;

    [Header("Sprites de notas")]
    [SerializeField] List<Sprite> spriteNotas;

    [Header("Controlador de animacion")]
    [SerializeField] Animator notaAnimator;

    [Header("Animator de Indicador de nota")]
    [SerializeField] Animator notaRojaIndicadorAnim;
    [SerializeField] Animator notaAzulIndicadorAnim;
    [SerializeField] Animator notaVerdeIndicadorAnim;
    [SerializeField] Animator notaAmarillaIndicadorAnim;



    [Header("Coordenadas de Objetivo a mirar")]
    [SerializeField] float ejeXIzquierdoObj;
    [SerializeField] float ejeXDerechoObj;

    [SerializeField] float minValorObjY;
    [SerializeField] float maxValorObjY;

    [Header("Coordenadas de punto de aparicion")]

    [SerializeField] float ejeXIzquierdoApa;
    [SerializeField] float ejeXDerechoApa;

    [SerializeField] float minValorAparY;
    [SerializeField] float maxValorAparY;

    [Header("Coordenadas de punto de aparicion")]
    [SerializeField] float spawnNotaRojaX;
    [SerializeField] float spawnNotaRojaY;

    [Space]
    [SerializeField] float spawnNotaAzulX;
    [SerializeField] float spawnNotaAzulY;

    [Space]
    [SerializeField] float spawnNotaAmarillaX;
    [SerializeField] float spawnNotaAmarillaY;

    [Space]
    [SerializeField] float spawnNotaVerdeX;
    [SerializeField] float spawnNotaVerdeY;

    [Header("Coordenadas de punto de destino")]
    [SerializeField] float destinoNotaRojaX;
    [SerializeField] float destinoNotaRojaY;

    [Space]
    [SerializeField] float destinoNotaAzulX;
    [SerializeField] float destinoNotaAzulY;

    [Space]
    [SerializeField] float destinoNotaAmarillaX;
    [SerializeField] float destinoNotaAmarillaY;

    [Space]
    [SerializeField] float destinoNotaVerdeX;
    [SerializeField] float destinoNotaVerdeY;

    [Header("Notas indicadoras GameObject")]
    [SerializeField] GameObject indicadorNotaRoja;

    [Space]
    [SerializeField] GameObject indicadorNotaAzul;

    [Space]
    [SerializeField] GameObject indicadorNotaAmarilla;

    [Space]
    [SerializeField] GameObject indicadorNotaVerde;

    [Header("Distancia minima para acertar")]

    [SerializeField] float distanciaMinAcierto;//0,5f ?

    [Header("Distancia minima para perder")]
    [SerializeField] float disMinAciertoAzulAmarilla;
    [SerializeField] float diMinAciertoVerdeRoja;




    [Header("Velocidad de animacion")]
    [SerializeField] float smoothTime;
    [SerializeField] float velocidadMax;


    private EspadaController espadaController;

    private Vector2 velocidadActual;

    private Vector2 objetivoAMirar;
    private Vector2 puntoAparicion;

    private bool animExplosionTermino;
    private bool animDesaparecerTermino;
    private bool movimientoIniciado;

    private bool notaEncimaRoja;
    private bool notaEncimaAzul;
    private bool notaEncimaAmarilla;
    private bool notaEncimaVerde;

    private bool notaErrada;
    private bool colorElegido;


    private int notaSeleccionada; //Roja , azul etc

    public bool NotaEncimaRoja { get => notaEncimaRoja; }
    public bool NotaEncimaAzul { get => notaEncimaAzul; }
    public bool NotaEncimaAmarilla { get => notaEncimaAmarilla; }
    public bool NotaEncimaVerde { get => notaEncimaVerde; }//Si aprieto en el tiempo de ataque y la nota esta encima.. ataca


    [Header("SpriteRenderer De notas de color")]
    [SerializeField] SpriteRenderer notaRojaSpriteR;
    [SerializeField] SpriteRenderer notaAzulSpriteR;
    [SerializeField] SpriteRenderer notaAmarillaSpriteR;
    [SerializeField] SpriteRenderer notaVerdeSpriteR;

    [Header("Notas a mover")] //Elegir con switch con un int en el inspector33
    [SerializeField] GameObject notaRojaAMoverGo;
    [SerializeField] GameObject notaAzulAMoverGo;
    [SerializeField] GameObject notaAmarillaAMoverGo;
    [SerializeField] GameObject notaVerdeAMoverGo;

    [Header("Notas Animator")]
    [SerializeField] Animator notaRojaAnim;
    [SerializeField] Animator notaAzulAnim;
    [SerializeField] Animator notaAmarillaAnim;
    [SerializeField] Animator notaVerdeAnim;

    private Vector2 puntoAparicionRoja;
    private Vector2 puntoAparicionAzul;
    private Vector2 puntoAparicionAmarilla;
    private Vector2 puntoAparicionVerde;

    private Vector2 objetivoAMirarRoja;
    private Vector2 objetivoAMirarAzul;
    private Vector2 objetivoAMirarAmarilla;
    private Vector2 objetivoAMirarVerde;

    private Vector2 velocidadActualRoja;
    private Vector2 velocidadActualAzul;
    private Vector2 velocidadActualAmarilla;
    private Vector2 velocidadActualVerde;

    private bool notaRojaSeleccionada;
    private bool notaAzulSeleccionada;
    private bool notaAmarillaSeleccionada;
    private bool notaVerdeSeleccionada;

    private bool notaErradaRoja;
    private bool notaErradaAzul;
    private bool notaErradaAmarilla;
    private bool notaErradaVerde;

    [Header("Velocidad de animacion Roja")]
    [SerializeField] float smoothTimeRoja;
    [SerializeField] float velocidadMaxRoja;

    [Header("Velocidad de animacion Azul")]
    [SerializeField] float smoothTimeAzul;
    [SerializeField] float velocidadMaxAzul;

    [Header("Velocidad de animacion Amarilla")]
    [SerializeField] float smoothTimeAmarilla;
    [SerializeField] float velocidadMaxAmarilla;

    [Header("Velocidad de animacion verde")]
    [SerializeField] float smoothTimeVerde;
    [SerializeField] float velocidadMaxVerde;

    [Header("Velocidad de notas")]
    [SerializeField] float velNotasFacil;
    [SerializeField] float velNotasMedio;
    [SerializeField] float velNotasDificil;


    public bool NotaRojaSeleccionada { get => notaRojaSeleccionada; }
    public bool NotaAzulSeleccionada { get => notaAzulSeleccionada; }
    public bool NotaAmarillaSeleccionada { get => notaAmarillaSeleccionada; }
    public bool NotaVerdeSeleccionada { get => notaVerdeSeleccionada; }


    private void Awake()
    {
        espadaController = FindObjectOfType<EspadaController>();

        animDesaparecerTermino = true;
        animExplosionTermino = true;
        movimientoIniciado = false;
    }

    private void Update()
    {
        MoverObjetoADireccion3();
        ConsultarPosicionNota2();
        IniciarAnimDesaparecer2();
    }

    public void PonerVelNotasNormal()
    {
        velocidadMaxRoja = 3.0f;
        velocidadMaxAzul = 3.0f;
        velocidadMaxAmarilla = 3.0f;
        velocidadMaxVerde = 3.0f;
    }
    public void DecidirIzquierdaODerecha()
    {
       
        if (animDesaparecerTermino && !movimientoIniciado) //Entra 2 veces
        {

            int i = Random.Range(1, 3);
            switch (i)
            {
                case 1: //Mirar izquierda
                    CalcularPuntoAparicion(ejeXIzquierdoApa);
                    CalcularObjetivo(ejeXIzquierdoObj);
                    PonerPosicionInicial();
                    //ApuntarEjeXaObjetivo();
                    ActivarAnimAparicion(); //Activar anim de aparicion

                    movimientoIniciado = true;
                    animDesaparecerTermino = false;

                    break;

                case 2: //Mirar Derecha
                    CalcularPuntoAparicion(ejeXDerechoApa);
                    CalcularObjetivo(ejeXDerechoObj);
                    PonerPosicionInicial();
                    //ApuntarEjeXaObjetivo();//

                    ActivarAnimAparicion();

                    movimientoIniciado = true;
                    animDesaparecerTermino = false;

                    break;
            }
        }
    }

    public void ElegirNotaColor(int i)
    {
        //if (animDesaparecerTermino && !movimientoIniciado && animExplosionTermino)
        //{
        notaSeleccionada = i; //Aca esta el error
        
        switch (i)
        {
            case 1: //rojo
                    //espadaController.ReiniciarGolpes();
                colorElegido = false; //Se compara de la mitad de la animacion de aparicion en adelante
                ModificarColor(i);
                PonerPuntoAparicion(spawnNotaRojaX, spawnNotaRojaY);
                PonerPuntoObjetivo(destinoNotaRojaX, destinoNotaRojaY);
                PonerPosicionInicial();
                notaErrada = false;
                ActivarAnimAparicion();

                movimientoIniciado = true;
                animDesaparecerTermino = false;
                animExplosionTermino = false;
                break;

            case 2: // - azul
                colorElegido = false;
                ModificarColor(i);
                PonerPuntoAparicion(spawnNotaAzulX, spawnNotaAzulY);
                PonerPuntoObjetivo(destinoNotaAzulX, destinoNotaAzulY);
                PonerPosicionInicial();
                notaErrada = false;
                ActivarAnimAparicion();

                movimientoIniciado = true;
                animDesaparecerTermino = false;
                animExplosionTermino = false;
                //-  
                break;

            case 3: //- verde
                colorElegido = false;
                ModificarColor(i);
                PonerPuntoAparicion(spawnNotaVerdeX, spawnNotaVerdeY);
                PonerPuntoObjetivo(destinoNotaVerdeX, destinoNotaVerdeY);
                PonerPosicionInicial();
                notaErrada = false;
                ActivarAnimAparicion();

                movimientoIniciado = true;
                animDesaparecerTermino = false;
                animExplosionTermino = false;
                break;

            case 4: //amarillo
                colorElegido = false;
                ModificarColor(i);
                PonerPuntoAparicion(spawnNotaAmarillaX, spawnNotaAmarillaY);
                PonerPuntoObjetivo(destinoNotaAmarillaX, destinoNotaAmarillaY);
                PonerPosicionInicial();
                notaErrada = false;
                ActivarAnimAparicion();

                movimientoIniciado = true;
                animDesaparecerTermino = false;
                animExplosionTermino = false;
                break;

        }

        //}
    }

    void PonerPuntoAparicion(float ejeX, float ejeY)
    {
        puntoAparicion = new Vector2(ejeX, ejeY);
    }
    void PonerPuntoObjetivo(float ejeX, float ejeY)
    {
        objetivoAMirar = new Vector2(ejeX, ejeY);

        
    }

    public void ReiniciarBoolsFallo() //Se ejecuta al final de la anim desaparecer
    {
        animDesaparecerTermino = true;
        movimientoIniciado = false;
        animExplosionTermino = true;

        //notaErrada = false;

        DesactivarAnimAparicion(); //animAparecer = false
        notaAnimator.SetBool("iniciarDesaparecer", false);

        notaAnimator.SetBool("errorAzul", false);
        notaAnimator.SetBool("errorVerde", false);
        notaAnimator.SetBool("errorRojo", false);
        notaAnimator.SetBool("errorAmarillo", false);
    }
    void CalcularObjetivo(float ejeObj)
    {
        float alturaRandom = Random.Range(minValorObjY, maxValorObjY);
        objetivoAMirar = new Vector2(ejeObj, alturaRandom);
        
    }

    void CalcularPuntoAparicion(float ejeApa) //debe salir de la cabeza mas o menos de 3 a 1,5
    {
        float alturaApRandom = Random.Range(minValorAparY, maxValorAparY);
        puntoAparicion = new Vector2(ejeApa, alturaApRandom);
        
    }
    //void ApuntarEjeXaObjetivo()
    //{
    //    Vector2 direccion = new Vector2((objetivoAMirar.x - (puntoAparicion.x)), (objetivoAMirar.y - (puntoAparicion.y)));
    //    puntoPivotGo.transform.right = direccion;
    //}

    public void IniciarComparacionColor()
    {
        colorElegido = true;
    }

    void ActivarAnimAparicion()
    {
        notaAnimator.SetBool("iniciarAparecer", true);
    }
    void DesactivarAnimAparicion()
    {
        notaAnimator.SetBool("iniciarAparecer", false);
    }

    public void ActivarAnimDesaparicionFallo() //se activa cuando se falla la tecla
    {
        notaErrada = true;
        notaAnimator.SetBool("errorRojo", true);
        notaAnimator.SetBool("errorAzul", true);
        notaAnimator.SetBool("errorAmarillo", true);
        notaAnimator.SetBool("errorVerde", true);
    }

    public void ActivarAnimDesaparicionFallo(int i) //Si el jugador toca antes, desaparece la nota y recibe daño
    {
        switch (i)
        {
            case 1: //rojo
                notaErrada = true;
                notaAnimator.SetBool("errorRojo", true);
                break;

            case 2: //Azul
                notaErrada = true;
                notaAnimator.SetBool("errorAzul", true);
                break;

            case 3: //Verde
                notaErrada = true;
                notaAnimator.SetBool("errorVerde", true);
                break;

            case 4: //Amarillo
                notaErrada = true;
                notaAnimator.SetBool("errorAmarillo", true);

                break;
        }
    }

    public void ActivarAnimExplosion(int i)
    {
        switch (i)
        {
            case 1://rojo
                animExplosionTermino = true;
                notaAnimator.SetBool("explosionRoja", true);
                break;

            case 2://azul
                animExplosionTermino = true;
                notaAnimator.SetBool("explosionAzul", true);
                break;

            case 3://verde
                animExplosionTermino = true;
                notaAnimator.SetBool("explosionVerde", true);
                break;

            case 4://violeta3
                animExplosionTermino = true;
                notaAnimator.SetBool("explosionAmarilla", true);
                break;
        }
    }

    public void ActivarAnimExplosion2(int i)
    {
        switch (i)
        {
            case 1://rojo
                notaAnimator.SetBool("notaExploto", true); //Si exploto la nota, vuelve al estado inactivo, se reinicia en ReiniciarBoolSAnim
                notaRojaIndicadorAnim.SetBool("explotarNota", true); //Se reinicia cuando termina la anim de explosion
                break;

            case 2://azul
                notaAnimator.SetBool("notaExploto", true);
                notaAzulIndicadorAnim.SetBool("explotarNota", true);
                break;

            case 3://verde
                notaAnimator.SetBool("notaExploto", true);
                notaVerdeIndicadorAnim.SetBool("explotarNota", true);
                break;

            case 4://amarilla
                notaAnimator.SetBool("notaExploto", true);
                notaAmarillaIndicadorAnim.SetBool("explotarNota", true);
                break;
        }
    }

    public void ReiniciarAnimExpl()
    {
        notaAnimator.SetBool("explosionRoja", false);
        notaAnimator.SetBool("explosionAzul", false);
        notaAnimator.SetBool("explosionVerde", false);
        notaAnimator.SetBool("explosionAmarilla", false);

        animExplosionTermino = true;
    }

    public void ReiniciarAnimExpl(Animator anim) //Cuando termina de explotar la nota, se pone falso
    {
        anim.SetBool("explotarNota", false);
    }
    public void ReiniciarBoolSAnim2()
    {
        
    }
    public void ReiniciarBoolSAnim() //Se ejecuta al comenzar la anim de aparicion
        //Se ejecuta cuando termina de explotar la nota
    {
        notaAnimator.SetBool("notaRoja", false);
        notaAnimator.SetBool("notaAzul", false);
        notaAnimator.SetBool("notaVerde", false);
        notaAnimator.SetBool("notaVioleta", false);

        notaAnimator.SetBool("notaExploto", false);

        notaAnimator.SetBool("iniciarDesaparecer", false);
    }

    public void ModificarColor(int tipAtk)
    {
        switch (tipAtk)
        {
            case 1://rojo

                notaSpriteR.sprite = spriteNotas[0];
                
                notaAnimator.SetBool("notaRoja", true);

                break;
            case 2://azul
                //notaSpriteR.color = colorAzul;
                notaSpriteR.sprite = spriteNotas[1];
                
                notaAnimator.SetBool("notaAzul", true);
                break;
            case 3://verde
                //notaSpriteR.color = colorVerde;
                notaSpriteR.sprite = spriteNotas[2];
                
                notaAnimator.SetBool("notaVerde", true);
                break;
            case 4://Amarillo
                //notaSpriteR.color = colorVioleta;
                notaSpriteR.sprite = spriteNotas[3];
                
                notaAnimator.SetBool("notaVioleta", true);
                break;
        }
    }

    void ConsultarPosicionNota() //Color elegido == true analiza la nota
    {
        if (colorElegido)
        {
            switch (notaSeleccionada)
            {
                case 1: //rojo Se activa aunque la nota seleccionada es 2
                    if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaRoja.transform.position) < distanciaMinAcierto)
                    {
                        notaEncimaRoja = true;
                        notaEncimaAzul = false;
                        notaEncimaVerde = false;
                        notaEncimaAmarilla = false;
                        
                    }
                    else
                    {
                        notaEncimaRoja = false;
                        
                    }
                    break;

                case 2: //azul
                    if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaAzul.transform.position) < distanciaMinAcierto)
                    {
                        notaEncimaAzul = true;
                        notaEncimaRoja = false;
                        notaEncimaVerde = false;
                        notaEncimaAmarilla = false;
                        
                    }
                    else
                    {
                        notaEncimaAzul = false;
                        
                    }
                    break;

                case 3: //Verde
                    if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaVerde.transform.position) < distanciaMinAcierto)
                    {
                        notaEncimaVerde = true;
                        notaEncimaRoja = false;
                        notaEncimaAzul = false;
                        notaEncimaAmarilla = false;
                        
                    }
                    else
                    {
                        notaEncimaVerde = false;
                        
                    }
                    break;

                case 4: //amarillo
                    if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaAmarilla.transform.position) < distanciaMinAcierto)
                    {
                        notaEncimaAmarilla = true;
                        notaEncimaRoja = false;
                        notaEncimaAzul = false;
                        notaEncimaVerde = false;
                        
                    }
                    else
                    {
                        notaEncimaAmarilla = false;
                        
                    }
                    break;
            }
        }
    }

    void ConsultarPosicionNota2() //Color elegido == true analiza la nota
    {
        if (Vector2.Distance(notaRojaAMoverGo.transform.position, indicadorNotaRoja.transform.position) < distanciaMinAcierto && notaRojaSeleccionada)
        {
            notaEncimaRoja = true;
            //notaEncimaAzul = false;
            //notaEncimaVerde = false;
            //notaEncimaAmarilla = false;
            
        }
        else
        {
            notaEncimaRoja = false;
            
        }
        ///Azulll
        if (Vector2.Distance(notaAzulAMoverGo.transform.position, indicadorNotaAzul.transform.position) < distanciaMinAcierto && notaAzulSeleccionada)
        {
            notaEncimaAzul = true;
            //notaEncimaAzul = false;
            //notaEncimaVerde = false;
            //notaEncimaAmarilla = false;
            
        }
        else
        {
            notaEncimaAzul = false;
            
        }

        if (Vector2.Distance(notaAmarillaAMoverGo.transform.position, indicadorNotaAmarilla.transform.position) < distanciaMinAcierto && notaAmarillaSeleccionada)
        {
            notaEncimaAmarilla = true;
            
        }
        else
        {
            notaEncimaAmarilla = false;
            
        }

        if (Vector2.Distance(notaVerdeAMoverGo.transform.position, indicadorNotaVerde.transform.position) < distanciaMinAcierto && notaVerdeSeleccionada)
        {
            notaEncimaVerde = true;
            
        }
        else
        {
            notaEncimaVerde = false;
            
        }

    }

    void PonerPosicionInicial()
    {
        notaAMoverGo.transform.position = puntoAparicion;
    }
    void IniciarAnimDesaparecer()
    {
        switch (notaSeleccionada)
        {
            case 1: //rojo
                if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaRoja.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAMoverGo.transform.position, objetivoAMirar) < diMinAciertoVerdeRoja)
                {
                    notaAnimator.SetBool("iniciarDesaparecer", true); // donde se reinicia?
                }
                break;

            case 2: //azul
                if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaAzul.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAMoverGo.transform.position, objetivoAMirar) < disMinAciertoAzulAmarilla)
                {
                    notaAnimator.SetBool("iniciarDesaparecer", true);
                }
                break;

            case 3: //Verde
                if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaVerde.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAMoverGo.transform.position, objetivoAMirar) < diMinAciertoVerdeRoja)
                {
                    notaAnimator.SetBool("iniciarDesaparecer", true);
                }
                break;

            case 4: //amarillo
                if (Vector2.Distance(notaAMoverGo.transform.position, indicadorNotaAmarilla.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAMoverGo.transform.position, objetivoAMirar) < disMinAciertoAzulAmarilla)
                {
                    notaAnimator.SetBool("iniciarDesaparecer", true);
                }
                break;
        }
    }

    void IniciarAnimDesaparecer2()
    {
        if (Vector2.Distance(notaRojaAMoverGo.transform.position, indicadorNotaRoja.transform.position) < distanciaMinAcierto && Vector2.Distance(notaRojaAMoverGo.transform.position, objetivoAMirarRoja) < diMinAciertoVerdeRoja)
        {
            notaRojaAnim.SetBool("desaparecerRoja", true); // donde se reinicia?
        }

        if (Vector2.Distance(notaAzulAMoverGo.transform.position, indicadorNotaAzul.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAzulAMoverGo.transform.position, objetivoAMirarAzul) < disMinAciertoAzulAmarilla)
        {
            notaAzulAnim.SetBool("desaparecerAzul", true);
        }

        if (Vector2.Distance(notaVerdeAMoverGo.transform.position, indicadorNotaVerde.transform.position) < distanciaMinAcierto && Vector2.Distance(notaVerdeAMoverGo.transform.position, objetivoAMirarVerde) < diMinAciertoVerdeRoja)
        {
            notaVerdeAnim.SetBool("desaparecerVerde", true);
        }

        if (Vector2.Distance(notaAmarillaAMoverGo.transform.position, indicadorNotaAmarilla.transform.position) < distanciaMinAcierto && Vector2.Distance(notaAmarillaAMoverGo.transform.position, objetivoAMirarAmarilla) < disMinAciertoAzulAmarilla)
        {
            notaAmarillaAnim.SetBool("desaparecerAmarilla", true);
        }

    }
    void MoverObjetoADireccion() // Debe desactivarse al iniciar la animacion de explosion
    {
        if (!animExplosionTermino) //Si la anim explosion no esta activa
        {
            notaAMoverGo.transform.position = Vector2.SmoothDamp(notaAMoverGo.transform.position, objetivoAMirar, ref velocidadActual, smoothTime, velocidadMax);
        }
    }

    void MoverObjetoADireccion2() // Debe desactivarse al iniciar la animacion de explosion
    {
        if (!notaErrada)
            notaAMoverGo.transform.position = Vector2.SmoothDamp(notaAMoverGo.transform.position, objetivoAMirar, ref velocidadActual, smoothTime, velocidadMax);
    }

    void MoverObjetoADireccion3() // Debe desactivarse al iniciar la animacion de explosion
    {
        if (!notaErradaRoja)
            notaRojaAMoverGo.transform.position = Vector2.SmoothDamp(notaRojaAMoverGo.transform.position, objetivoAMirarRoja, ref velocidadActualRoja, smoothTimeRoja, velocidadMaxRoja);
        if (!notaErradaAzul)
            notaAzulAMoverGo.transform.position = Vector2.SmoothDamp(notaAzulAMoverGo.transform.position, objetivoAMirarAzul, ref velocidadActualAzul, smoothTimeAzul, velocidadMaxAzul);
        if (!notaErradaAmarilla)
            notaAmarillaAMoverGo.transform.position = Vector2.SmoothDamp(notaAmarillaAMoverGo.transform.position, objetivoAMirarAmarilla, ref velocidadActualAmarilla, smoothTimeAmarilla, velocidadMaxAmarilla);
        if (!notaErradaVerde)
            notaVerdeAMoverGo.transform.position = Vector2.SmoothDamp(notaVerdeAMoverGo.transform.position, objetivoAMirarVerde, ref velocidadActualVerde, smoothTimeVerde, velocidadMaxVerde);
    }



    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>


    public void SeleccionarNotaRoja() //Debe iniciarse en la mitad de la anim desaparecer o al comienzo  de la anim aparecer
    {
        notaRojaSeleccionada = true;
    }
    public void DesactivarNotaRoja() //Debe desactivarse cuando la nota esta inactiva
    {
        notaRojaSeleccionada = false; //tal vez deba ponerse en false apenas inicia la anim desaparecer
        notaRojaAnim.SetBool("errorRoja", false);
        notaRojaAnim.SetBool("desaparecerRoja", false);
    }

    public void PonerPosicionInicialRoja() // Se debe poner cuando termina de desaparecer o explota, cuarto evento
    {
        notaRojaAMoverGo.transform.position = puntoAparicionRoja;
    }
    public void ElegirNotaRoja() //Se retea sola la posicion inicial cuando esta inactiva
    {
        /*notaRojaSeleccionada = true; *///Debe iniciarse en la mitad de la anim aparecer y volverse falso en nota inactiva

        puntoAparicionRoja = new Vector2(spawnNotaRojaX, spawnNotaRojaY); //Spawn
        objetivoAMirarRoja = new Vector2(destinoNotaRojaX, destinoNotaRojaY);

        PonerPosicionInicialRoja();

        notaErradaRoja = false; //Inicia el movimiento de la nota
        notaRojaAnim.SetBool("desaparecerRoja", false);
        notaRojaAnim.SetBool("aparecerRoja", true);
    }

    public void ActivarAnimFalloRojo() //Cuando fallo al apretar la tecla
    {
        notaErradaRoja = true;
        notaRojaAnim.SetBool("errorRoja", true);
    }

    public void ReiniciarAparecerRoja() //Se reinicia al comenzar la animacion de error y al comenzar desaparecer, tal vez se debe reiniciar antes seleccionar una nota
    {
        notaRojaAnim.SetBool("aparecerRoja", false);
        notaRojaAnim.SetBool("desaparecerRoja", false);
    }

    public void ActivarAnimExplosionRoja() //Se reinicia solo cuando termina de explotar
    {
        notaRojaIndicadorAnim.SetBool("explotarNota", true);
    }


    ////Azullllllllllll
    ///

    public void SeleccionarNotaAzul() //Debe iniciarse en la mitad de la anim desaparecer
    {
        notaAzulSeleccionada = true;
    }
    public void DesactivarNotaAzul() //Debe desactivarse cuando la nota esta inactiva, al final de la anim desaparecer
    {
        notaAzulSeleccionada = false; //tal vez deba ponerse en false apenas inicia la anim desaparecer
        notaAzulAnim.SetBool("errorAzul", false);
        notaAzulAnim.SetBool("desaparecerAzul", false);
    }

    public void PonerPosicionInicialAzul() // Se debe poner cuando termina de desaparecer o explota
    {
        notaAzulAMoverGo.transform.position = puntoAparicionAzul;
    }
    public void ElegirNotaAzul() //Se retea sola la posicion inicial cuando esta inactiva
    {
        /*notaRojaSeleccionada = true; *///Debe iniciarse en la mitad de la anim aparecer y volverse falso en nota inactiva

        puntoAparicionAzul = new Vector2(spawnNotaAzulX, spawnNotaAzulY); //Spawn
        objetivoAMirarAzul = new Vector2(destinoNotaAzulX, destinoNotaAzulY);

        PonerPosicionInicialAzul();

        notaErradaAzul = false; //Inicia el movimiento de la nota
        notaAzulAnim.SetBool("desaparecerAzul", false);
        notaAzulAnim.SetBool("aparecerAzul", true); //reiniciar aparecer
    }

    public void ActivarAnimFalloAzul() //Cuando fallo al apretar la tecla
    {
        notaErradaAzul = true;
        notaAzulAnim.SetBool("errorAzul", true);
    }

    public void ReiniciarAparecerAzul() //Se reinicia al comenzar la animacion de error y al comenzar desaparecer, tal vez se debe reiniciar antes seleccionar una nota
    {
        notaAzulAnim.SetBool("aparecerAzul", false);
        notaAzulAnim.SetBool("desaparecerAzul", false);
    }

    public void ActivarAnimExplosionAzul() //Se reinicia solo cuando termina de explotar
    {
        notaAzulIndicadorAnim.SetBool("explotarNota", true);
    }


    ///Verdeeeeeeeeeeee
    ///
    public void SeleccionarNotaVerde() //Debe iniciarse en la mitad de la anim desaparecer
    {
        notaVerdeSeleccionada = true;
    }
    public void DesactivarNotaVerde() //Debe desactivarse cuando la nota esta inactiva
    {
        notaVerdeSeleccionada = false; //tal vez deba ponerse en false apenas inicia la anim desaparecer
        notaVerdeAnim.SetBool("errorVerde", false);
        notaVerdeAnim.SetBool("desaparecerVerde", false);
    }

    public void PonerPosicionInicialVerde() // Se debe poner cuando termina de desaparecer o explota
    {
        notaVerdeAMoverGo.transform.position = puntoAparicionVerde;
    }
    public void ElegirNotaVerde() //Se retea sola la posicion inicial cuando esta inactiva
    {
        /*notaRojaSeleccionada = true; *///Debe iniciarse en la mitad de la anim aparecer y volverse falso en nota inactiva

        puntoAparicionVerde = new Vector2(spawnNotaVerdeX, spawnNotaVerdeY); //Spawn
        objetivoAMirarVerde = new Vector2(destinoNotaVerdeX, destinoNotaVerdeY);

        PonerPosicionInicialVerde();

        notaErradaVerde = false; //Inicia el movimiento de la nota
        notaVerdeAnim.SetBool("desaparecerVerde", false);
        notaVerdeAnim.SetBool("aparecerVerde", true);
    }

    public void ActivarAnimFalloVerde() //Cuando fallo al apretar la tecla
    {
        notaErradaVerde = true;
        notaVerdeAnim.SetBool("errorVerde", true);
    }

    public void ReiniciarAparecerVerde() //Se reinicia al comenzar la animacion de error y al comenzar desaparecer, tal vez se debe reiniciar antes seleccionar una nota
    {
        notaVerdeAnim.SetBool("aparecerVerde", false);
        notaVerdeAnim.SetBool("desaparecerVerde", false); //Tal vez se deba crear un metodo nuevo y ponerlo como cuarto evento
    }

    public void ActivarAnimExplosionVerde() //Se reinicia solo cuando termina de explotar
    {
        notaVerdeIndicadorAnim.SetBool("explotarNota", true);
    }

    //Amarilloooooooooooooooooooooooooooooooooooooooooooooooooo

    public void SeleccionarNotaAmarilla() //Debe iniciarse en la mitad de la anim desaparecer
    {
        notaAmarillaSeleccionada = true;
    }
    public void DesactivarNotaAmarilla() //Debe desactivarse cuando la nota esta inactiva
    {
        notaAmarillaSeleccionada = false; //tal vez deba ponerse en false apenas inicia la anim desaparecer
        notaAmarillaAnim.SetBool("errorAmarilla", false);
        notaAmarillaAnim.SetBool("desaparecerAmarilla", false);
    }

    public void PonerPosicionInicialAmarilla() // Se debe poner cuando termina de desaparecer o explota
    {
        notaAmarillaAMoverGo.transform.position = puntoAparicionAmarilla;
    }
    public void ElegirNotaAmarilla() //Se retea sola la posicion inicial cuando esta inactiva
    {
        /*notaRojaSeleccionada = true; *///Debe iniciarse en la mitad de la anim aparecer y volverse falso en nota inactiva

        puntoAparicionAmarilla = new Vector2(spawnNotaAmarillaX, spawnNotaAmarillaY); //Spawn
        objetivoAMirarAmarilla = new Vector2(destinoNotaAmarillaX, destinoNotaAmarillaY);

        PonerPosicionInicialAmarilla();

        notaErradaAmarilla = false; //Inicia el movimiento de la nota
        notaAmarillaAnim.SetBool("desaparecerAmarilla", false);
        notaAmarillaAnim.SetBool("aparecerAmarilla", true);
    }

    public void ActivarAnimFalloAmarilla() //Cuando fallo al apretar la tecla
    {
        notaErradaAmarilla = true;
        notaAmarillaAnim.SetBool("errorAmarilla", true);
    }

    public void ReiniciarAparecerAmarilla() //Se reinicia al comenzar la animacion de error y al comenzar desaparecer, tal vez se debe reiniciar antes seleccionar una nota
    {
        notaAmarillaAnim.SetBool("aparecerAmarilla", false);
        notaAmarillaAnim.SetBool("desaparecerAmarilla", false);
    }

    public void ActivarAnimExplosionAmarilla() //Se reinicia solo cuando termina de explotar
    {
        notaAmarillaIndicadorAnim.SetBool("explotarNota", true);
    }
}