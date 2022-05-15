using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatonSobreBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator botonAnimator;
    [SerializeField] string nombreCondicionDeBoton;

    [Header("Valor a resetear")]
    [SerializeField] string condicionAReset;

    [Header("Es boton opcion")]
    [SerializeField] bool botonOpcion;
    [SerializeField] string condicionAReset2;

    [Header("Es boton menu")]
    [SerializeField] bool botonMenu;

    private string condMenu;

    private void Start()
    {
        if(botonMenu)
        {
            nombreCondicionDeBoton = "expandirBoton";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        botonAnimator.SetBool(nombreCondicionDeBoton, true);

        //menuPAudioSource.MenuPSource.PlayOneShot(menuPAudioSource.EncimaBoton);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        botonAnimator.SetBool(nombreCondicionDeBoton, false);

        if(botonOpcion)
        {
            botonAnimator.SetBool(condicionAReset2, true);
        }
    }

    public void DesactivarBoton(string condicionReset)
    {
        botonAnimator.SetBool(condicionReset, true);
    }

    public void DesactivarBool(string condicionReset)
    {
        botonAnimator.SetBool(condicionReset, false);
    }

    public void ResetearBool(bool estado)
    {
        botonAnimator.SetBool(condicionAReset, estado);
    }
}
