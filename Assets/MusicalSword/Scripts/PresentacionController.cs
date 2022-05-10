using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PresentacionController : MonoBehaviour
{
    [Header("Anim de panel")]
    [SerializeField] Animator animPanel;

    [Header("Anim de presentacion")]
    [SerializeField] Animator animPresentacion;

    [Header("Panel Escena 1")]

    [SerializeField] Image panelEsc1Image;
    [SerializeField] Gradient gradientPanel;

    [Header("Panel Escena 1")]
    [SerializeField] AudioSource historiaAS;
    [SerializeField] AudioClip historiaAudioClip;

    private bool escenaActiva1;
    private bool escenaActiva2;
    private bool escenaActiva3;
    private bool escenaActiva4;
    private bool escenaActiva5;
    private bool escenaActiva6;
    private bool escenaActiva7;

    private int escogerMetodo;

    private void Awake()
    {
        escogerMetodo = 1;
    }


    private void Start()
    {
        panelEsc1Image.color = gradientPanel.Evaluate(1f);

        DesaparecerPanel();
    }

    private void Update()
    {
        DetectarInputJugador();
    }

    public void ActivarEscenaSwitch()
    {
        switch(escogerMetodo)
        {
            case 1:
                ActivarEscena1();
                escogerMetodo += 1;
                break;
            case 2:
                ActivarEscena2();
                escogerMetodo += 1;
                break;
            case 3:
                ActivarEscena3();
                escogerMetodo += 1;
                break;
            case 4:
                ActivarEscena4();
                escogerMetodo += 1;
                break;
            case 5:
                ActivarEscena5();
                escogerMetodo += 1;
                break;
            case 6:
                ActivarEscena6();
                escogerMetodo += 1;
                break;
            case 7:
                ActivarEscena7();
                escogerMetodo += 1;
                break;
        }
    }

    public void ActivarEscena1()
    {
        escenaActiva1 = true;
    }
    public void ActivarEscena2()
    {
        escenaActiva2 = true;
    }
    public void ActivarEscena3()
    {
        escenaActiva3 = true;
    }
    public void ActivarEscena4()
    {
        escenaActiva4 = true;
    }
    public void ActivarEscena5()
    {
        escenaActiva5 = true;
    }
    public void ActivarEscena6()
    {
        escenaActiva6 = true;
    }
    public void ActivarEscena7()
    {
        escenaActiva7 = true;
    }

    void DetectarInputJugador()
    {
        if (Input.GetKeyDown(GuardarVariables.inst.BotonAtaque1) | Input.GetKeyDown(GuardarVariables.inst.BotonAtaque2)
             | Input.GetKeyDown(GuardarVariables.inst.BotonAtaque3) | Input.GetKeyDown(GuardarVariables.inst.BotonAtaque4))
        {
            if(animPresentacion.GetBool("iniciarEscena1") && !animPresentacion.GetBool("iniciarEscena2") && escenaActiva1)
            {
                animPresentacion.SetBool("iniciarEscena2", true);
            }
            else if(animPresentacion.GetBool("iniciarEscena2") && !animPresentacion.GetBool("iniciarEscena3") && escenaActiva2)
            {
                animPresentacion.SetBool("iniciarEscena3", true);
            }
            else if (animPresentacion.GetBool("iniciarEscena3") && !animPresentacion.GetBool("iniciarEscena4") && escenaActiva3)
            {
                animPresentacion.SetBool("iniciarEscena4", true);
            }
            else if (animPresentacion.GetBool("iniciarEscena4") && !animPresentacion.GetBool("iniciarEscena5") && escenaActiva4)
            {
                animPresentacion.SetBool("iniciarEscena5", true);
            }
            else if (animPresentacion.GetBool("iniciarEscena5") && !animPresentacion.GetBool("iniciarEscena6") && escenaActiva5)
            {
                animPresentacion.SetBool("iniciarEscena6", true);
            }
            else if (animPresentacion.GetBool("iniciarEscena6") && !animPresentacion.GetBool("iniciarEscena7") && escenaActiva6)
            {
                animPresentacion.SetBool("iniciarEscena7", true);
            }
            else if (animPresentacion.GetBool("iniciarEscena7") && !animPresentacion.GetBool("iniciarEscena8") && escenaActiva7)
            {
                animPresentacion.SetBool("iniciarEscena8", true);
            }
        }
    }
    public void DesaparecerPanel()
    {
        animPanel.SetBool("desaparecerPanel", true);
        animPresentacion.SetBool("iniciarEscena1", true);

        historiaAS.PlayOneShot(historiaAudioClip, 0.85f);
    }
    public void IniciarEscena2()
    {
        animPresentacion.SetBool("iniciarEscena2", true);
    }

    public void IniciarEscena3()
    {
        animPresentacion.SetBool("iniciarEscena3", true);
    }

    public void IniciarEscena4()
    {
        animPresentacion.SetBool("iniciarEscena4", true);
    }

    public void IniciarEscena5()
    {
        animPresentacion.SetBool("iniciarEscena5", true);
    }

    public void IniciarEscena6()
    {
        animPresentacion.SetBool("iniciarEscena6", true);
    }

    public void IniciarEscena7()
    {
        animPresentacion.SetBool("iniciarEscena7", true);
    }

    public void IniciarEscena8()
    {
        animPresentacion.SetBool("iniciarEscena8", true);
    }

    public void CargarEscenaJuego()
    {
        //Tal vez una corrutina que disminuya el sonido, al comienzo de desaparecer pantalla(octavo evento) y cargar escena al final (noveno evento)

        SceneManager.LoadScene("Nivel1");
    }
}