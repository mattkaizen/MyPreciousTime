using System;
using System.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{
    /*Este Script se encarga de:
     * 
     * 1-Cambiar el tipo de daño que hace la espada
     * 2-Agregar un sprite de la espada
     * 
    */
    [Header("Daño de la espada")]
    [SerializeField] float dañoActual;

    [SerializeField] float dañoNormal; //25
    [SerializeField] float dañoAfilada;
    [SerializeField] float dañoPocoAfilada;
    [SerializeField] float dañoDesafilada;


    [Header("Imagen de la espada")]
    [SerializeField] Sprite espadaSprite;

    [Header("Animator de la espada")]
    [SerializeField] Animator espadaAnim;
    private SpriteRenderer espadaSpriteRend;

    private EspadaController espadaController;

    private int tipoDeDaño;

    private bool estaAtacando;
    public bool EstaAtacando { get => estaAtacando; set => estaAtacando = value; }
    public float DañoActual { get => dañoActual; }
    private void Awake()
    {
        espadaController = FindObjectOfType<EspadaController>();
        espadaSpriteRend = GetComponent<SpriteRenderer>();
        espadaSpriteRend.sprite = espadaSprite;


        tipoDeDaño = 1;
        dañoActual = dañoNormal;
    }

    private void Update()
    {
        ElegirDaño();
    }


    /// <summary>
    /// Cambia el tipo de daño, donde 1 es daño normal, 2 afilado, 3 poco afilado, 4 desafilado
    /// </summary>
    /// <param name="nuevoDaño"></param>
    public void ActualizarDañoActual(int nuevoDaño)
    {
        tipoDeDaño = nuevoDaño;
    }

    /// <summary>
    /// Activo un bool, espero el tiempo indicado por un periodo de ataque y desactivo el mismo bool
    /// </summary>
    /// <param name="metodoAct"></param>
    /// <param name="metodoDes"></param>
    /// <returns></returns>
    public IEnumerator RegistrarAtaque(float tiempo)
    {
        estaAtacando = true;
        
        ActivarAnimEspadaPresionada();
        yield return new WaitForSeconds(tiempo);
        DesactivarAnimEspadaPresionada();
        estaAtacando = false;
        espadaController.NumFallosEvento = 0;
        /*espadaController.NumFallos = 0;*/ //Se debe reiniciar cuando termina de atacar el monstruo
        //espadaController.GolpeAcertado = false; //Se debe reiniciar cuando el monstruo termina de atacar
        
    }

    void ElegirDaño()
    {
        switch (tipoDeDaño)
        {
            case 1: //Daño Normal
                dañoActual = dañoNormal;
                break;
            case 2: //Daño Afilado
                dañoActual = dañoAfilada;
                break;
            case 3: //Daño poco afilado
                dañoActual = dañoPocoAfilada;
                break;
            case 4: //Daño desafilado
                dañoActual = dañoDesafilada;
                break;
        }
    }

    void ActivarAnimEspadaPresionada()
    {
        espadaAnim.SetBool("espadaApretada", true);
    }

    void DesactivarAnimEspadaPresionada()
    {
        espadaAnim.SetBool("espadaApretada", false);
    }

}