using UnityEngine;
using UnityEngine.Events;

public class InvocarEvento : MonoBehaviour
{
    [SerializeField] UnityEvent miEvento;
    [SerializeField] UnityEvent OtroEvento;
    [SerializeField] UnityEvent tecerEvento;
    [SerializeField] UnityEvent cuartoEvento;
    [SerializeField] UnityEvent quintaEvento;
    [SerializeField] UnityEvent sextoEvento;
    [SerializeField] UnityEvent septimoEvento;
    [SerializeField] UnityEvent octavoEvento;
    [SerializeField] UnityEvent novenoEvento;

    void InvocarUnEvento()
    {
        miEvento.Invoke();
    }
    void InvocarOtroEvento()
    {
        OtroEvento.Invoke();
    }

    void InvocarTercerEvento()
    {
        tecerEvento.Invoke();
    }
    void InvocarCuartoEvento()
    {
        cuartoEvento.Invoke();
    }

    void InvocarQuintoEvento()
    {
        quintaEvento.Invoke();
    }
    void InvocarSextoEvento()
    {
        sextoEvento.Invoke();
    }
    void InvocarSeptimoEvento()
    {
        septimoEvento.Invoke();
    }
    void InvocarOctavoEvento()
    {
        octavoEvento.Invoke();
    }
    void InvocarNovenoEvento()
    {
        novenoEvento.Invoke();
    }
}