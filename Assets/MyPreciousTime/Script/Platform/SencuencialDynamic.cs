using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SencuencialDynamic : MonoBehaviour
{
    [Header("Controlador de plataforma")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] Vector3 goalPosition;
    [SerializeField] float speed; //En 5 se nota
    [SerializeField] float minDistance;

    [Header("Controlador de Secuencial toques")]
    [SerializeField] bool esSecuencial;
    [SerializeField] int cantidadActivarGold;

    [Header("Controlador de Secuencial tiempo")]
    [SerializeField] bool esSecuencialTiempo;
    [SerializeField] float timeToActivePlatform;
    [SerializeField] Animator goldPlatformAnim;

    [Header("Siguiente plataforma a activar")]
    [SerializeField] GameObject sigPlataformaGO;
    [SerializeField] Animator sigPlataformaAnim;
    [SerializeField] bool activaScript;
    [SerializeField] bool activaAnim;
    [SerializeField] SencuencialDynamic sigPlataformaSecuencial;


    [Header("Es la ultima?")]
    [SerializeField] bool ultimaPlatSecuencia;

    [Header("Movimiento Automatico")]
    [SerializeField] bool movimientoAutomatico;
    [Header("Arranca en el centro?")]
    [SerializeField] int cantidadActivarSgtPlataforma;
    [SerializeField] bool arrancaCentro;
    [SerializeField] Vector3 nuevaPosIzquierda;
    [SerializeField] float speedAPosCentral;
    [SerializeField] AnimationCurve curveNueva;
    [SerializeField] float minDistanceCentro;

    [Header("Activa la dorada?")]
    [SerializeField] bool noActivaLaDorada;

    [Header("Desactiva Otras Plataform")]
    [SerializeField] bool desactivaPlataformas;
    [SerializeField] List<GameObject> plataformasADesactivar;


    [Header("Cambio de colores ")]
    [SerializeField] Color colorInicial;
    [SerializeField] Color colorFinal;
    [SerializeField] float smoothness;


    [Header("Es plat inicial ")]
    [SerializeField] bool esInicial;
    private float duration;

    private SpriteRenderer plataformaSpriteR;

    private Vector3 inicialPos;
    private Vector3 posCentral;

    private Rigidbody2D platformRb;
    private Animator platformAnim;

    private float current;
    private float target;

    private float currentNuevo;
    private float targetNuevo;

    private bool llegoPosA;
    private bool llegoPosB;
    private bool activoGoldPlatform;
    private bool tocoPlataforma;

    private bool llegoDestino;

    private bool terminoDesactivarPlataformas;

    private bool modificoPosInicial;
    private bool terminoSecuencia;
    private bool inicioDesactivar;
    private bool arreglarValores;

    private int cantidadIdaVuelta;

    private AudioController audioController;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
        plataformaSpriteR = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (!esSecuencial)
        {
            current = 0;
            target = 1;
        }
        platformAnim = GetComponent<Animator>();
        platformRb = GetComponent<Rigidbody2D>();

        inicialPos = platformRb.position;
        posCentral = platformRb.position;

        if (!esInicial)
        {
            platformAnim.SetBool("Desactivar", true);
        }
    }

    private void Update()
    {
        CalcularDistanciaMeta();
        CalcularDistanciaObjetivo();
        ActivarGoldPlatformCantidad();
        ActivarGoldPlatformCantidadSecuencia();
        DesactivarPlataformas();
        TerminoSecuencia();
        //ActivarGoldPlatformTiempo();
    }


    private void FixedUpdate()
    {
        MoverPosAPosB();
    }

    private void DesactivarPlataformas()
    {
        if(desactivaPlataformas && !terminoDesactivarPlataformas && tocoPlataforma)
        {
            for(int i = 0; i < plataformasADesactivar.Count; i++ )
            {
                plataformasADesactivar[i].SetActive(false);
            }
            terminoDesactivarPlataformas = true;
        }
    }
    private void ModificarPosInicial()
    {
        inicialPos = nuevaPosIzquierda;
    }
    public void TocoPlataforma()
    {
        tocoPlataforma = true;
    }
    private void ActivarGoldPlatformCantidad()
    {
        if (esSecuencial && !noActivaLaDorada && !arrancaCentro)
        {
            if (cantidadIdaVuelta == cantidadActivarGold && !activoGoldPlatform)
            {
                activoGoldPlatform = true;
                goldPlatformAnim.SetBool("Activar", true);
            }
        }
    }
    private void ActivarGoldPlatformCantidadSecuencia()
    {
        if (esSecuencial && !noActivaLaDorada && arrancaCentro)
        {
            if (!activoGoldPlatform && inicioDesactivar)
            {
                activoGoldPlatform = true;
                goldPlatformAnim.SetBool("Activar", true);
            }
        }
    }
    private void ActivarGoldPlatformTiempo()
    {
        if (esSecuencialTiempo)
        {
            if (!activoGoldPlatform && tocoPlataforma)
            {
                activoGoldPlatform = true;
                StartCoroutine(ActivarPlataformaGold());
            }
        }
    }

    private void CalcularDistanciaMeta()
    {
        if (!esSecuencial)
        {
            if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && !llegoPosA)
            {
                llegoPosA = true;
                StartCoroutine(ActivarPlataformaSecuencial(platformAnim, sigPlataformaGO));

                if (ultimaPlatSecuencia)
                {
                    goldPlatformAnim.SetBool("Activar", true);
                }
            }
        }
    }

    private void TerminoSecuencia()
    {
        if (esSecuencial /*&& !noActivaLaDorada*/)
        {
            if (cantidadIdaVuelta == cantidadActivarSgtPlataforma && !terminoSecuencia)
            {
                terminoSecuencia = true;
                Debug.Log("TerminoSecuenciia");
                inicialPos = posCentral;
            }
        }
    }

    IEnumerator ActivarPlataformaSecuencial(Animator thisPlatform, GameObject nextPlatform) //Si inicia al aparecer la plataforma
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);
        StartCoroutine(LerpColor());
        yield return new WaitForSeconds(timeToActivePlatform);
        if(activaScript)
        {
            sigPlataformaSecuencial.enabled = true;
            sigPlataformaAnim.SetBool("Desactivar", false);
            sigPlataformaAnim.SetBool("Activar", true);
        }
        if(activaAnim)
        {
            sigPlataformaAnim.SetBool("Desactivar", false);
            sigPlataformaAnim.SetBool("Activar", true);
        }
        nextPlatform.SetActive(true);
        thisPlatform.SetBool("Desactivar", true);
        thisPlatform.gameObject.SetActive(false);
    }
    private void CalcularDistanciaObjetivo()
    {
        if (esSecuencial)
        {
            if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && !llegoPosA) //A = izquuieirda
            {
                if (arrancaCentro && !modificoPosInicial)
                {
                    modificoPosInicial = true;
                    ModificarPosInicial();
                }
                llegoPosA = true;
                llegoPosB = false;
                target = target == 0 ? 1 : 0; //si target es igual a 0, entonces 1, sino 0.
                Debug.Log("Pos B");
                if (tocoPlataforma)
                {
                    cantidadIdaVuelta++;
                    audioController.AumentarPitch();
                    audioController.ReproducirSonidoToque();
                }
            }
            else if (platformRb.position.x > goalPosition.x && !llegoPosA) //Por si se caen los fps y no calcula la distancia en ese frame
            {
                llegoPosA = true;
                llegoPosB = false;
                target = target == 0 ? 1 : 0;
                Debug.Log("Paso B");
            }

            else if (Vector3.Distance(platformRb.position, inicialPos) < minDistance && !llegoPosB) //Derecha
            {
                //if(terminoSecuencia && !inicioDesactivar)
                //{
                //    inicioDesactivar = true;
                //    StartCoroutine(ActivarPlataformaSecuencial(platformAnim, sigPlataformaGO));
                //    return;
                //}
                llegoPosB = true;
                llegoPosA = false;
                target = target == 0 ? 1 : 0;
                Debug.Log("Pos A");
                if (tocoPlataforma)
                {
                    cantidadIdaVuelta++;
                    audioController.AumentarPitch();
                    audioController.ReproducirSonidoToque();
                }
            }
            if (Vector3.Distance(platformRb.position, posCentral) < minDistanceCentro && terminoSecuencia && arrancaCentro)
            {
                if (!inicioDesactivar)
                {
                    inicioDesactivar = true;
                    Debug.Log("secuenciaDesactvar");
                    StartCoroutine(ActivarPlataformaSecuencial(platformAnim, sigPlataformaGO));
                    return;
                }
            }
            else if (platformRb.position.x < inicialPos.x && !llegoPosB)
            {
                llegoPosB = true;
                llegoPosA = false;
                target = target == 0 ? 1 : 0;
                Debug.Log("Paso A");
            }
        }
    }

    private void MoverPosAPosB()
    {
        if (!movimientoAutomatico)
        {
            if (tocoPlataforma && !llegoPosA)
            {
                current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

                platformRb.position = Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current));
            }
        }

        if(movimientoAutomatico && !inicioDesactivar && !terminoSecuencia)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            platformRb.MovePosition(Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current)));
        }
        if (movimientoAutomatico && !inicioDesactivar && terminoSecuencia)
        {
            if(terminoSecuencia && !arreglarValores)
            {
                currentNuevo = 0;
                targetNuevo = 1;
            }
            //else if (terminoSecuencia && !arreglarValores && llegoPosB)
            //{
            //    target = 0;
            //    arreglarValores = true;
            //}
            //else if (terminoSecuencia && !arreglarValores && current == 0)
            //{
            //    target = 1;
            //    arreglarValores = true;
            //}
            currentNuevo = Mathf.MoveTowards(currentNuevo, targetNuevo, speedAPosCentral * Time.deltaTime);

            platformRb.MovePosition(Vector3.Lerp(platformRb.position, posCentral, curveNueva.Evaluate(currentNuevo)));

        }
    }

    IEnumerator ActivarPlataformaGold() //Si inicia al aparecer la plataforma
    {
        yield return new WaitForSeconds(timeToActivePlatform);
        goldPlatformAnim.SetBool("Activar", true);
    }

    IEnumerator LerpColor()
    {
        plataformaSpriteR.color = colorInicial;
        duration = timeToActivePlatform;
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            plataformaSpriteR.color = Color.Lerp(plataformaSpriteR.color, colorFinal, progress);
            //bloom.color.Interp(Color.red, Color.magenta, progress);
            progress += increment;
            Debug.Log("Lerp");
            yield return new WaitForSeconds(smoothness);
        }
    }
}
