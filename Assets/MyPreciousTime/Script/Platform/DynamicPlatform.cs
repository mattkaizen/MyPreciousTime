using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatform : MonoBehaviour
{
    [Header("Controlador de plataforma")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] Vector3 goalPosition;
    [SerializeField] float speed; //En 5 se nota
    [SerializeField] float minDistance;

    private Vector3 inicialPos;
    private Rigidbody2D platformRb;

    private float current;
    private float target;

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

    //private void MoverPosAPosB()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        target = target == 0 ? 1 : 0; //si target es igual a 0, entonces 1, sino 0.
    //        //es lo mismo que si llego a pos
    //    }
    //    current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

    //    platformRb.position = Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current));
    //}

    private void CalcularDistanciaObjetivo()
    {
        if (Vector3.Distance(platformRb.position, goalPosition) < minDistance && !llegoPosA)
        {
            llegoPosA = true;
            llegoPosB = false;
            target = target == 0 ? 1 : 0; //si target es igual a 0, entonces 1, sino 0.
            Debug.Log("Pos B");
        }
        else if(platformRb.position.x > goalPosition.x && !llegoPosA) //Por si se caen los fps y no calcula la distancia en ese frame
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
        }
        else if(platformRb.position.x < inicialPos.x && !llegoPosB)
        {
            llegoPosB = true;
            llegoPosA = false;
            target = target == 0 ? 1 : 0;
            Debug.Log("Paso A");
        }
    }

    private void MoverPosAPosB()
    {
        current = Mathf.MoveTowards(current, target, speed * Time.deltaTime);

        platformRb.position = Vector3.Lerp(inicialPos, goalPosition, curve.Evaluate(current));
    }
}