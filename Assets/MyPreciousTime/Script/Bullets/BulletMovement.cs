using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Controlador de Bala")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] Vector3 goalPosition;
    [SerializeField] float speed; //En 5 se nota
    [SerializeField] float minDistance;
    [Space]
    //[Header("Tipo de Movimiento de Bala")]
    //[SerializeField] int tipoMovimimento;

    [Space]
    [Header("Nuevas posiciones de Aparicion")]
    [SerializeField] bool cambiaPos;
    [SerializeField] Vector3 inicialPos2;
    [SerializeField] Vector3 goalPosition2;
    [Space]
    [SerializeField] Vector3 inicialPos3;
    [SerializeField] Vector3 goalPosition3;

    [SerializeField] float speed2;
    [SerializeField] float speed3;
    [Space]
    [Header("Activa otra Bala")]
    [SerializeField] bool activaOtraBala;
    [SerializeField] GameObject otraBalaGO;
    [SerializeField] float tiempoAActivarBala;


    private Vector3 inicialPos;
    private Rigidbody2D platformRb;
    private AudioController audioController;
    private GameManager gameManager;

    private int tipoMovimiento;

    private float current;
    private float target;

    private bool activoMovimiento;
    private bool activoBala;

    private bool llegoPosA;
    private bool llegoPosB;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        platformRb = GetComponent<Rigidbody2D>();
        audioController = FindObjectOfType<AudioController>();
        inicialPos = platformRb.position;
        tipoMovimiento = 1;
    }

    private void Update()
    {
        if (gameManager.JuegoActivo)
        {
            if (!cambiaPos)
            {
                CalcularDistanciaObjetivo();
            }
            if(cambiaPos)
            {
                CalcularDistObjBalas();
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.JuegoActivo)
        {
            if (!cambiaPos)
            {
                MoverPosAPosB();
            }
            if(cambiaPos)
            {
                MoverPosAPosB(tipoMovimiento);
            }
        }
    }

    private void CalcularDistanciaObjetivo()
    {
        if(!activoMovimiento)
        {
            current = 0;
            target = 1;
            activoMovimiento = true;
            audioController.ReproducirSonidoProyectil();
        }
        else if (Vector3.Distance(platformRb.position, goalPosition) < minDistance)
        {
            current = 0;
            target = 1; //si target es igual a 0, entonces 1, sino 0.
            platformRb.position = inicialPos;
            llegoPosA = true;
            activoMovimiento = false;
        }
    }
    private void CalcularDistObjBalas()
    {
        if (!activoMovimiento)
        {
            current = 0;
            target = 1;
            activoMovimiento = true;
            audioController.ReproducirSonidoProyectil();

            if (activaOtraBala && !activoBala)
            {
                activoBala = true;
                StartCoroutine(ActivarOtraBala());
            }
        }
        else if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && tipoMovimiento == 1)
        {
            current = 0;
            target = 1; //si target es igual a 0, entonces 1, sino 0.
            tipoMovimiento = 2;

            platformRb.position = inicialPos2;
            Debug.Log("cambPos");
            activoMovimiento = false;
        }
        else if (Vector3.Distance(platformRb.position, goalPosition2) < minDistance && tipoMovimiento == 2)
        {
            current = 0;
            target = 1; //si target es igual a 0, entonces 1, sino 0.
            tipoMovimiento = 3;
            Debug.Log("cambPos2");
            platformRb.position = inicialPos3;
            activoMovimiento = false;
        }
        else if (Vector3.Distance(platformRb.position, goalPosition3) < minDistance && tipoMovimiento == 3)
        {
            current = 0;
            target = 1; //si target es igual a 0, entonces 1, sino 0.
            tipoMovimiento = 1;

            platformRb.position = inicialPos;
            activoMovimiento = false;
        }
    }

    IEnumerator ActivarOtraBala()
    {
        yield return new WaitForSeconds(tiempoAActivarBala);
        otraBalaGO.SetActive(true);
    }

    private void MoverPosAPosB()
    {
        if (activoMovimiento)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
            platformRb.MovePosition(Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current)));
        }
    }
    private void MoverPosAPosB(int primerMov)
    {
        if (activoMovimiento && primerMov == 1)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
            platformRb.MovePosition(Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current)));
        }
        if (activoMovimiento && primerMov == 2)
        {
            current = Mathf.MoveTowards(current, target, speed2 * Time.deltaTime);
            platformRb.MovePosition(Vector3.Lerp(inicialPos2, goalPosition2, curve.Evaluate(current)));
        }
        if (activoMovimiento && primerMov == 3)
        {
            current = Mathf.MoveTowards(current, target, speed3 * Time.deltaTime);
            platformRb.MovePosition(Vector3.Lerp(inicialPos3, goalPosition3, curve.Evaluate(current)));
        }
    }

}