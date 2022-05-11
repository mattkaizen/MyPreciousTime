using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVariables : MonoBehaviour
{
    public static SaveVariables inst;

    private bool ganoElJuego; //Si no gano el juego, se ve la anim de derrota en el menu principal

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
}