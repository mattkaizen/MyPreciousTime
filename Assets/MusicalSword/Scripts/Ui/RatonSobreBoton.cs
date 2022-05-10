using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatonSobreBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator botonAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        botonAnimator.SetBool("expandirBoton", true);

        //menuPAudioSource.MenuPSource.PlayOneShot(menuPAudioSource.EncimaBoton);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        botonAnimator.SetBool("expandirBoton", false);
    }
}
