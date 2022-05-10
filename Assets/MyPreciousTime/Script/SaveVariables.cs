using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVariables : MonoBehaviour
{
    public static SaveVariables inst;

    private int numeroDeNivel;

    public int NumeroDeNivel { get => numeroDeNivel; set => numeroDeNivel = value; }
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