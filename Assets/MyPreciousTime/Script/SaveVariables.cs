using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVariables : MonoBehaviour
{
    public static SaveVariables inst;

    private bool ganoElJuego; //Si no gano el juego, se ve la anim de derrota en el menu principal

    private bool mostrarPantallaDerrota;
    private bool mostrarTutorial;
    public bool MostrarTutorial { get => mostrarTutorial; set => mostrarTutorial = value; }
    public bool MostrarPantallaDerrota { get => mostrarPantallaDerrota; set => mostrarPantallaDerrota = value; }
    public bool GanoElJuego { get => ganoElJuego; set => ganoElJuego = value; }
    private void Awake()
    {
        if (SaveVariables.inst == null)
        {
            SaveVariables.inst = this;

            DontDestroyOnLoad(gameObject); //No destruye este script
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool ObtenerValorGanoJuego()
    {
        return inst.GanoElJuego;
    }

    public bool ObtenerValorPantallaDerrota()
    {
        return inst.MostrarPantallaDerrota;
    }

    public void ModificarValorGanoJuego(bool value)
    {
        inst.GanoElJuego = value;
    }
    public void ModificarValorPantallaDerrota(bool value)
    {
        inst.MostrarPantallaDerrota = value;
    }
    public void ActivarTutorial(bool value)
    {
        inst.mostrarTutorial = value;
    }
}