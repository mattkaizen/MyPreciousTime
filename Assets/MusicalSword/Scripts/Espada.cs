using System;
using System.Collections;
using UnityEngine;

public class Espada : MonoBehaviour
{
    /*Este Script se encarga de:
     * 
     * 1-Cambiar el tipo de da�o que hace la espada
     * 2-Agregar un sprite de la espada
     * 
    */
    [Header("Da�o de la espada")]
    [SerializeField] float da�oActual;

    [SerializeField] float da�oNormal; //25
    [SerializeField] float da�oAfilada;
    [SerializeField] float da�oPocoAfilada;
    [SerializeField] float da�oDesafilada;


    [Header("Imagen de la espada")]
    [SerializeField] Sprite espadaSprite;

    [Header("Animator de la espada")]
    [SerializeField] Animator espadaAnim;
    private SpriteRenderer espadaSpriteRend;

    private EspadaController espadaController;

    private int tipoDeDa�o;

    private bool estaAtacando;
    public bool EstaAtacando { get => estaAtacando; set => estaAtacando = value; }
    public float Da�oActual { get => da�oActual; }
    private void Awake()
    {
        espadaController = FindObjectOfType<EspadaController>();
        espadaSpriteRend = GetComponent<SpriteRenderer>();
        espadaSpriteRend.sprite = espadaSprite;


        tipoDeDa�o = 1;
        da�oActual = da�oNormal;
    }

    private void Update()
    {
        ElegirDa�o();
    }


    /// <summary>
    /// Cambia el tipo de da�o, donde 1 es da�o normal, 2 afilado, 3 poco afilado, 4 desafilado
    /// </summary>
    /// <param name="nuevoDa�o"></param>
    public void ActualizarDa�oActual(int nuevoDa�o)
    {
        tipoDeDa�o = nuevoDa�o;
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

    void ElegirDa�o()
    {
        switch (tipoDeDa�o)
        {
            case 1: //Da�o Normal
                da�oActual = da�oNormal;
                break;
            case 2: //Da�o Afilado
                da�oActual = da�oAfilada;
                break;
            case 3: //Da�o poco afilado
                da�oActual = da�oPocoAfilada;
                break;
            case 4: //Da�o desafilado
                da�oActual = da�oDesafilada;
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