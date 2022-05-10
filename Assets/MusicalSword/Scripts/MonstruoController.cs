using UnityEngine;

public class MonstruoController : MonoBehaviour
{
    [Header("GameObjects de los monstruos")]
    [SerializeField] GameObject primerMonstruoGo;
    [SerializeField] GameObject segundoMonstruoGo;
    [SerializeField] GameObject tercerMonstruoGo;

    [Header("Vida de los monstruos, 25 = golpe")]
    [SerializeField] float vidaFacil; //25 se muere de un golpe
    [SerializeField] float vidaMedio; // 50 de 2 golpes
    [SerializeField] float vidaDificil; // 75 de 3 golpes

    [SerializeField] float vidaBossFinal;

    [Header("Vida de los monstruos, 25 = golpe")]
    [SerializeField] AudioSource jefeFinalAS;
    [SerializeField] AudioClip jefeRecibioGolpeAC;

    private GameManager gameManager;
    private Monstruo monstruoCopia;

    private bool monstruoActivoEnPantalla;
    private bool gritoBossFinalEmitido;
    private bool jefeFinalActivado;

    private int monstruosGenerados;


    public bool JefeFinalActivado { get => jefeFinalActivado; set => jefeFinalActivado = value; }

    //private int monstruosGenerados; //Si la cantidad de monstruos es la mitad del numero maximo de monstruos,
    //se modifican sus valores de vida y ataque


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        monstruoActivoEnPantalla = false;
    }

    private void Update()
    {
        ObtenerYActivarMonstruos();
        ConsultarEstadoMonstruo();
        ActivarJefeFinal();
    }

    public void ActivarSonidoGolpeJefe() //Si el jefe esta activo, activar en comparar ataques
    {
        if (jefeFinalActivado)
        {
            jefeFinalAS.PlayOneShot(jefeRecibioGolpeAC, 0.85f);
        }
    }

    void ActivarAtaqueMonstruo(ref Monstruo mons)
    {
        if (!mons.MonstruoActivo)
        {
            mons.MonstruoActivo = true;
            mons.MonstruoAnimator.SetBool("aparecerMonstruo", true);
            mons.MonstruoAnimator.SetInteger("tipoMonstruo", monstruosGenerados);
        }

    }

    void ActivarAnimBossFinal(ref Monstruo mons) //Requiere modificar, primero una pantalla en negro, un grito y aparece el boss
    {
        if (!mons.MonstruoActivo)
        {
            mons.MonstruoActivo = true;
            mons.MonstruoAnimator.SetBool("aparecerMonstruo", true);
            mons.MonstruoAnimator.SetInteger("tipoMonstruo", monstruosGenerados);
        }

    }

    void ConsultarEstadoMonstruo()
    {
        monstruoActivoEnPantalla = monstruoCopia.MonstruoActivo;
    }

    //Crear lista de monstruos

    void ObtenerYActivarMonstruos() //Falta que se desactiven los monstruos cuando mueran.
    {
        if (gameManager.JuegoActivo && monstruosGenerados < 11)
        {
            GameObject monstruoGo = ObjectPooler.SharedInstance.GetPooledObject(); //Obtengo un monstruo
            if (monstruoGo != null && !monstruoActivoEnPantalla) //Si no es nulo el objeto Y no hay monstruos activos, se activa y comienza a atacar.
            {
                monstruoGo.SetActive(true);
                monstruoCopia = monstruoGo.GetComponent<Monstruo>();
                monstruosGenerados += 1;
                SeleccionarAnimLayer(monstruoCopia);

               
                AsignarVidaMaxima();
                ActivarAtaqueMonstruo(ref monstruoCopia);

            }
        }
    }

    void ActivarJefeFinal()
    {
        if (gameManager.JuegoActivo)
        {
            GameObject monstruoGo = ObjectPooler.SharedInstance.GetPooledObject();
            if (monstruosGenerados == 10 && !monstruoActivoEnPantalla && monstruoGo != null)
            {
                jefeFinalActivado = true;
                monstruoGo.SetActive(true);
                monstruoCopia = monstruoGo.GetComponent<Monstruo>();
                monstruosGenerados += 1;
                SeleccionarAnimLayer(monstruoCopia);

                
                AsignarVidaMaxima();
                ActivarAnimBossFinal(ref monstruoCopia);
            }
        }
    }

    public void SeleccionarAnimLayer(Monstruo mons) //Se puede hacer al azar pero requiere que se indentifiquen todos los sprites
    {
        switch (monstruosGenerados)
        {
            case 1:
                mons.MonstruoAnimator.SetLayerWeight(0, 1.0f); //El primer monstruo tiene la capa base
                //mons.MonstruoAnimator.SetLayerWeight(spritesColocados, 1.0f);
                break;
            case 2:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 3:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 4:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 5: //Mini boss que cambia de forma
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 6:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 7:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 8:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 9:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 10:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
            case 11:
                mons.MonstruoAnimator.SetLayerWeight(0, 0.0f);
                mons.MonstruoAnimator.SetLayerWeight(monstruosGenerados - 1, 1.0f);
                break;
        }

        //Necesito un int de SpriteColocados y un switch con cada caso
        //Quitar el peso a la layer base y agregarselo al actual

    }
    public void AsignarVidaMaxima() //Para asignar una dificultad interesante
    {
        switch (monstruosGenerados)
        {
            case 1:
                monstruoCopia.PonerVidaMaxima(vidaFacil);
                break;
            case 2:
                monstruoCopia.PonerVidaMaxima(vidaDificil);
                break;
            case 3:
                monstruoCopia.PonerVidaMaxima(vidaMedio);
                break;
            case 4:
                monstruoCopia.PonerVidaMaxima(vidaFacil); //Puedo crear un switch con un random range para que sea aleatorio
                break;
            case 5: //Monstruo dificil 
                monstruoCopia.PonerVidaMaxima(vidaDificil);
                break;
            case 6:
                monstruoCopia.PonerVidaMaxima(vidaDificil);
                break;
            case 7:
                monstruoCopia.PonerVidaMaxima(vidaMedio);
                break;
            case 8:
                monstruoCopia.PonerVidaMaxima(vidaMedio);
                break;
            case 9:
                monstruoCopia.PonerVidaMaxima(vidaMedio);
                break;
            case 10:
                monstruoCopia.PonerVidaMaxima(vidaFacil);
                break;
            case 11: // Boss final
                monstruoCopia.PonerVidaMaxima(vidaBossFinal);
                break;
            default:
                monstruoCopia.PonerVidaMaxima(PonerVidaAleatoria());
                break;
        }
    }

    float PonerVidaAleatoria()
    {
        int i = Random.Range(1, 4);
        float nuevaVida = vidaFacil;

        switch (i)
        {
            case 1:
                nuevaVida = vidaFacil;
                break;
            case 2:
                nuevaVida = vidaMedio;
                break;
            case 3:
                nuevaVida = vidaDificil;
                break;
        }
        return nuevaVida;
    }

    public void RestarVidaMonstruoActual(float dmg)
    {
        monstruoCopia.RestarVida(dmg);
    }

    public void ActivarAnimDmgMonstruoActivo()
    {
        monstruoCopia.ActivarAnimDmg();
    }

    public MoverNotaMusical PasarNotaMusicalActiva()
    {
        return monstruoCopia.MoverNotaMusical;
    }

    public float PasarDmgMonstruoActivo()
    {
        return monstruoCopia.DmgMonstruo;
    }

    public int PasarTipoAtaqueMonstruoActivo()
    {
        return monstruoCopia.ObtenerTipoAtaqueActivo();
    }

    public Monstruo PasarMonstruoActivo()
    {
        return monstruoCopia;
    }

}