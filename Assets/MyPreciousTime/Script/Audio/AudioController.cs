using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

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

    [Header("Audioclips")]
    [SerializeField] AudioClip musicaNivel1AudioClip;
    [SerializeField] AudioClip musicaVictoriaAudioClip;

    [Header("Velocidad a disminuir la musica")]
    [SerializeField] float velocidadDisminuidor; //0.05
    [SerializeField] float velCorrutina; //0.1


    private GameManager gameManager;

    private bool musicaIniciada;
    private bool musicaPausada;
    private bool musicaVictoria;

    private float velocidad;

    private string parametroMaster;
    private string parametroEspada;
    private string parametroMonstruo;

    private string parametroFx;
    private string parametroMusica;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            musicaAudioSource.PlayOneShot(musicaNivel1AudioClip, 0.8f);
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

    void ReproducirMusicaVictoria()
    {
        if (musicaVictoria && !musicaVictoriaAS.isPlaying)
        {
            musicaVictoriaAS.PlayOneShot(musicaVictoriaAudioClip);
        }
    }

    public void IniciarMusicaVictoria() //Iniciar cuando aparezca la pantalla en negro despues del jefe
    {
        musicaVictoria = true;
    }

    public void IniciarCorrutinaApagarMusica()
    {
        StartCoroutine(ApagarMusica());
    }

    IEnumerator ApagarMusica()
    {
        while (musicaAudioSource.volume > 0.01f)
        {
            musicaAudioSource.volume -= velocidadDisminuidor;
            yield return new WaitForSeconds(velCorrutina);
        }
    }
}
