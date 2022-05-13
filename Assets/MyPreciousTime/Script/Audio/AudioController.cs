using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    [Header("AudioMixer principal")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Sliders de sonidos de menu de opciones")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFxSlider;

    [Header("AudioSources")]
    [SerializeField] AudioSource musicaAudioSource;
    [SerializeField] AudioSource musicaVictoriaAS;
    [SerializeField] AudioSource sonidoVictoriaAS;
    [SerializeField] AudioSource sonidoGolpeAS;
    [SerializeField] AudioSource sonidoSaltoAS;
    [SerializeField] AudioSource sonidoCargarEscenaAS;
    [SerializeField] AudioSource sonidoInicioEscenaAS;
    [SerializeField] AudioSource muerteAS;
    [SerializeField] AudioSource proyectilAudioSource;

    [Header("Audioclips")]
    [SerializeField] AudioClip musicaNivelAudioClip;
    [SerializeField] AudioClip musicaVictoriaAudioClip;
    [SerializeField] AudioClip sonidoVictoriaAudioClip;
    [SerializeField] AudioClip sonidoGolpeAudioClip;
    [SerializeField] AudioClip sonidoCargarEscAudioClip;
    [SerializeField] AudioClip sonidoInicioEscAudioClip;
    [SerializeField] AudioClip muerteAudioClip;
    [SerializeField] AudioClip proyectilAudioClip;
    [SerializeField] List<AudioClip> listaSaltosAudioClip;
    [SerializeField] List<AudioClip> listaGolpesAudioClip;

    [Header("Velocidad a disminuir la musica")]
    [SerializeField] float velocidadDisminuidor; //0.05
    [SerializeField] float velCorrutinaApagar;//0.1
    [SerializeField] float velCorrutinaApagarVictoria;

    [Header("Velocidad a aumentar la musica")]
    [SerializeField] float velocidadAumentador; //0.05
    [SerializeField] float velCorrutinaIniciar; //0.1




    private GameManager gameManager;
    private GoldPlatform goldPlatfom;
    private MonstruoAnimator monstAnim;



    private bool musicaIniciada;
    private bool musicaPausada;
    private bool musicaVictoria;
    private bool sonidoVictoria;
    private bool sonidoGolpeJefe;
    private bool terminoSonidos;

    private bool primeraReproduccionMusica;

    private float velocidad;

    private string parametroMaster;
    private string parametroEspada;
    private string parametroMonstruo;

    private string parametroFx;
    private string parametroMusica;


    public bool TerminoSonidos { get => terminoSonidos; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        goldPlatfom = FindObjectOfType<GoldPlatform>();
        monstAnim = FindObjectOfType<MonstruoAnimator>();
        velocidad = 30.0f;
        parametroMaster = "MasterVolume";
        parametroMusica = "MusicaVolume";
        parametroFx = "SoundEffectVolume";

    }

    private void Start()
    {
        ObtenerValoresSlider();
    }

    void Update()
    {
        IniciarMusica();
        ReproducirSonidoGolpe();
        RastrearSonidoVictoria();
        ReproducirMusicaVictoria();
    }

    private void OnDisable()
    {
        GuardarValoresSlider();
    }

    void GuardarValoresSlider()
    {
        PlayerPrefs.SetFloat(parametroMaster, masterSlider.value);
        PlayerPrefs.SetFloat(parametroMusica, musicSlider.value);
        PlayerPrefs.SetFloat(parametroFx, soundFxSlider.value);
    }

    void ObtenerValoresSlider()
    {
        masterSlider.value = PlayerPrefs.GetFloat(parametroMaster, masterSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat(parametroMusica, musicSlider.value);
        soundFxSlider.value = PlayerPrefs.GetFloat(parametroFx, soundFxSlider.value);
    }
    public void ModificarValorMasterSlider(float value)
    {
        audioMixer.SetFloat(parametroMaster, Mathf.Log10(value) * velocidad);
    }
    public void ModificarValorSoundEffect(float value)
    {
        audioMixer.SetFloat(parametroFx, Mathf.Log10(value) * velocidad);
    }
    public void ModificarValorMusicaSlider(float value)
    {
        audioMixer.SetFloat(parametroMusica, Mathf.Log10(value) * velocidad);
    }

    public void IniciarMusica()//Le modifique el volumen en el Audiomixer
    {
        if (!musicaAudioSource.isPlaying && gameManager.JuegoActivo && !musicaPausada)
        {
            musicaAudioSource.PlayOneShot(musicaNivelAudioClip, 0.8f);

            if (!primeraReproduccionMusica)
            {
                IniciarCorrutinaAparecerMusica();
                //Armar codigo de control que inicie 1 corrutina al principio del nivel
            }
        }
    }

    public void PausarMusica()
    {
        musicaPausada = true;
        musicaAudioSource.Pause();
    }

    public void ReanudarMusica()
    {
        musicaPausada = false;
        musicaAudioSource.UnPause();
    }

    public void ActivarMusicaVictoria() //Se ejecuta al final de la anim desaparecer panel CanvasFOndo
    {
        musicaVictoria = true;
    }
    public void ReproducirMusicaVictoria()
    {
        if (musicaVictoria && !musicaVictoriaAS.isPlaying)
        {
            musicaVictoriaAS.PlayOneShot(musicaVictoriaAudioClip);
        }
    }

    public void IniciarMusicaVictoria() //Iniciar cuando aparezca la pantalla en negro despues del jefe
    {
        musicaVictoria = true;
        StartCoroutine(AparecerMusicaVictoria());
    }

    //------------------------------------------------

    public void ReproducirSonidoMuerte()
    {
        muerteAS.PlayOneShot(muerteAudioClip);
    }
    public void ReproducirSonidoSalto()
    {
        sonidoSaltoAS.PlayOneShot(listaSaltosAudioClip[Random.Range(0, listaSaltosAudioClip.Count)]);
    }

    public void RastrearSonidoVictoria()
    {
        if (sonidoVictoria && !sonidoVictoriaAS.isPlaying)
        {
            terminoSonidos = true;
        }
    }
    public void ReproducirSonidoVictoria() //Se reproduce sonido de golpe cuando termina de reproducir el sonido de victoria
    {
        if (sonidoGolpeJefe && !sonidoGolpeAS.isPlaying && !sonidoVictoria)
        {
            sonidoVictoriaAS.PlayOneShot(sonidoVictoriaAudioClip);
            sonidoVictoria = true;
        }
    }
    public void ReproducirSonidoGolpe() //Se reproduce en el update
    {
        if (goldPlatfom.ActivarVictoriaFase && gameManager.PlatfDoradasTocadas == gameManager.PlataformasDoradasXNivel)
        {
            if (!sonidoGolpeJefe && !sonidoGolpeAS.isPlaying)
            {
                sonidoGolpeJefe = true;
                monstAnim.ActivarAnimDaño();
                ReproducirSonidoDeGolpePorFase(gameManager.NumeroDeNivel);
                //sonidoGolpeAS.PlayOneShot(sonidoGolpeAudioClip);
            }
            ReproducirSonidoVictoria();
        }
    }

    public void ReproducirSonidoDeGolpePorFase(int numNivel) //gamemanager.
    {
        switch(numNivel)
        {
            case 1:
                if(gameManager.NumeroDeFase == 1)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[0]);
                }
                if (gameManager.NumeroDeFase == 2)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[1]);
                }
                if (gameManager.NumeroDeFase == 3)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[2]);
                }
                break;

            case 2:
                if (gameManager.NumeroDeFase == 1)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[3]);
                }
                if (gameManager.NumeroDeFase == 2)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[4]);
                }
                if (gameManager.NumeroDeFase == 3)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[5]);
                }
                break;

            case 3:
                if (gameManager.NumeroDeFase == 1)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[6]);
                }
                if (gameManager.NumeroDeFase == 2)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[7]);
                }
                if (gameManager.NumeroDeFase == 3)
                {
                    sonidoGolpeAS.PlayOneShot(listaGolpesAudioClip[8]);
                }
                break;
        }
    }

    public void ReproducirSonidoProyectil()
    {
        proyectilAudioSource.PlayOneShot(proyectilAudioClip);
    }

    //public void ReproducirSonidoVictoria() //Se reproduce en el update
    //{
    //    if (goldPlatfom.ActivarVictoria)
    //    {
    //        if (!sonidoVictoria && !sonidoVictoriaAS.isPlaying)
    //        {
    //            sonidoVictoria = true;
    //            sonidoVictoriaAS.PlayOneShot(sonidoVictoriaAudioClip);
    //        }
    //        ReproducirSonidoGolpeJefe();
    //    }
    //}

    public void IniciarCorrutinaApagarMusica()
    {
        StartCoroutine(ApagarMusica());
    }

    public void ReproducirSonidoNuevaEscena()
    {
        sonidoCargarEscenaAS.PlayOneShot(sonidoCargarEscAudioClip);
    }
    public void ReproducirSonidoInicioEscena()
    {
        sonidoInicioEscenaAS.PlayOneShot(sonidoInicioEscAudioClip);
    }

    public void IniciarCorrutinaAparecerMusica()
    {
        if (!musicaIniciada)
        {
            musicaIniciada = true;
            StartCoroutine(AparecerMusica());
        }
    }

    public void IniciarCorrutinaAparecerMusicaVictoria()
    {
        StartCoroutine(AparecerMusicaVictoria());
    }
    public void IniciarCorrutinaApagarMusicaVictoria()
    {
        StartCoroutine(ApagarMusicaVictoria());
    }
    IEnumerator AparecerMusicaVictoria()
    {
        musicaVictoriaAS.volume = 0.01f;
        while (musicaVictoriaAS.volume < 0.8f)
        {
            musicaVictoriaAS.volume += velocidadAumentador;
            yield return new WaitForSeconds(velCorrutinaIniciar);
        }
    }
    IEnumerator AparecerMusica()
    {
        primeraReproduccionMusica = true;
        musicaAudioSource.volume = 0.01f;
        while (musicaAudioSource.volume < 0.8f)
        {
            musicaAudioSource.volume += velocidadAumentador;
            yield return new WaitForSeconds(velCorrutinaIniciar);
        }
    }
    IEnumerator ApagarMusica()
    {
        while (musicaAudioSource.volume > 0.01f)
        {
            musicaAudioSource.volume -= velocidadDisminuidor;
            yield return new WaitForSeconds(velCorrutinaApagar);
        }
    }

    IEnumerator ApagarMusicaVictoria()
    {
        while (musicaAudioSource.volume > 0.01f)
        {
            musicaAudioSource.volume -= velocidadDisminuidor;
            yield return new WaitForSeconds(velCorrutinaApagarVictoria);
        }
    }
}