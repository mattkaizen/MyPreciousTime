using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Vol : MonoBehaviour
{
    public Slider musicSlider;
    public float sliderValue;
    public Image imagenMute;

    private string parametroMusica;
    private float velocidad;

    [SerializeField] AudioMixer audioMixer;


    private void Awake()
    {
        velocidad = 30.0f;
        parametroMusica = "MusicaVolume";
    }
    // Start is called before the first frame update
    void Start()
    {
        ObtenerValoresSlider();

        musicSlider.value = PlayerPrefs.GetFloat("volumenAudio", 0.85f);
        //AudioListener.volume = musicSlider.value;
        //RevisarSiEstoyMute();
        
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        //PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        PlayerPrefs.SetFloat(parametroMusica, musicSlider.value);
        //AudioListener.volume = musicSlider.value;
        audioMixer.SetFloat(parametroMusica, Mathf.Log10(valor) * velocidad);
        //RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }



    private void OnDisable()
    {
        GuardarValoresSlider();
    }

    void GuardarValoresSlider()
    {
        PlayerPrefs.SetFloat(parametroMusica, musicSlider.value);
    }

    void ObtenerValoresSlider()
    {
        musicSlider.value = PlayerPrefs.GetFloat(parametroMusica, musicSlider.value);
    }
    public void ModificarValorMusicaSlider(float value)
    {
        audioMixer.SetFloat(parametroMusica, Mathf.Log10(value) * velocidad);
    }
}