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
    [Header("Tipo de Movimiento de Bala")]
    [SerializeField] int tipoMovimimento;

    private Vector3 inicialPos;
    private Rigidbody2D platformRb;

    private float current;
    private float target;

    private bool activoMovimiento;
    private bool llegoPosA;
    private bool llegoPosB;

    private void Awake()
    {
        platformRb = GetComponent<Rigidbody2D>();

        inicialPos = platformRb.position;
    }

    private void Update()
    {
        CalcularDistanciaObjetivo();
    }

    private void FixedUpdate()
    {
        MoverPosAPosB();
    }

    private void CalcularDistanciaObjetivo()
    {
        if(!activoMovimiento)
        {
            current = 0;
            target = 1;
            activoMovimiento = true;
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

    private void MoverPosAPosB()
    {
        if (activoMovimiento)
        {
            current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);
            platformRb.MovePosition(Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current)));
        }
    }

}