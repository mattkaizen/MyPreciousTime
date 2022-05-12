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

    [Header("Es la ultima?")]
    [SerializeField] bool ultimaPlatSecuencia;

    private Vector3 inicialPos;
    private Rigidbody2D platformRb;
    private Animator platformAnim;

    private float current;
    private float target;

    private bool llegoPosA;
    private bool llegoPosB;
    private bool activoGoldPlatform;
    private bool tocoPlataforma;

    private bool llegoDestino;

    private int cantidadIdaVuelta;

    private void Awake()
    {
        current = 0;
        target = 1;

        platformAnim = GetComponent<Animator>();
        platformRb = GetComponent<Rigidbody2D>();

        inicialPos = platformRb.position;
    }

    private void Update()
    {
        CalcularDistanciaMeta();
        //ActivarGoldPlatformCantidad();
        //ActivarGoldPlatformTiempo();
    }

    private void FixedUpdate()
    {
        MoverPosAPosB();
    }

    public void TocoPlataforma()
    {
        tocoPlataforma = true;
    }
    private void ActivarGoldPlatformCantidad()
    {
        if (esSecuencial)
        {
            if (cantidadIdaVuelta == cantidadActivarGold && !activoGoldPlatform)
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
        if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && !llegoPosA)
        {
            llegoPosA = true;
            StartCoroutine(ActivarPlataformaSecuencial(platformAnim, sigPlataformaGO));

            if(ultimaPlatSecuencia)
            {
                goldPlatformAnim.SetBool("Activar", true);
            }
        }
    }

    IEnumerator ActivarPlataformaSecuencial(Animator thisPlatform, GameObject nextPlatform) //Si inicia al aparecer la plataforma
    {
        thisPlatform.SetBool("Activar", false);
        thisPlatform.SetBool("Desactivar", false);
        yield return new WaitForSeconds(timeToActivePlatform);
        nextPlatform.SetActive(true);
        thisPlatform.SetBool("Desactivar", true);
    }
    private void CalcularDistanciaObjetivo()
    {
        if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && !llegoPosA)
        {
            llegoPosA = true;
            llegoPosB = false;
            target = target == 0 ? 1 : 0; //si target es igual a 0, entonces 1, sino 0.
            Debug.Log("Pos B");
            if (tocoPlataforma)
                cantidadIdaVuelta++;
        }
        else if (platformRb.position.x > goalPosition.x && !llegoPosA) //Por si se caen los fps y no calcula la distancia en ese frame
        {
            llegoPosA = true;
            llegoPosB = false;
            target = target == 0 ? 1 : 0;
            Debug.Log("Paso B");
        }

        else if (Vector3.Distance(platformRb.position, inicialPos) < minDistance && !llegoPosB)
        {
            llegoPosB = true;
            llegoPosA = false;
            target = target == 0 ? 1 : 0;
            Debug.Log("Pos A");
            if (tocoPlataforma)
                cantidadIdaVuelta++;
        }
        else if (platformRb.position.x < inicialPos.x && !llegoPosB)
        {
            llegoPosB = true;
            llegoPosA = false;
            target = target == 0 ? 1 : 0;
            Debug.Log("Paso A");
        }
    }

    private void MoverPosAPosB()
    {
        if (tocoPlataforma && !llegoPosA)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

            platformRb.position = Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current));
        }
    }

    IEnumerator ActivarPlataformaGold() //Si inicia al aparecer la plataforma
    {
        yield return new WaitForSeconds(timeToActivePlatform);
        goldPlatformAnim.SetBool("Activar", true);
    }
}
