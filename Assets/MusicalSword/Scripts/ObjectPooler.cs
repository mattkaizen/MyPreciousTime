using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    /* Este script hace:
     * 1-Crea una lista de Game objects
     * 2-Asignar un sprite a cada objeto de la lista
     * */
    public static ObjectPooler SharedInstance;

    [Header("Piscina de GameObjects")]
    [SerializeField] List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int cantidadMinMonstruos;
    [SerializeField] int cantidadMaxMonstruos;
    private int amountToPool; 

    [Header("Piscina de Sprites")]
    [SerializeField] List<Sprite> pooledSprites;

    private Monstruo monstruo;



    void Awake()
    {
        SharedInstance = this;
        //amountToPool = Random.Range(cantidadMinMonstruos, cantidadMaxMonstruos + 1);
        amountToPool = 11; //el jefe final es el 11
    }

    // Start is called before the first frame update
    void Start()
    {
        CrearPiscinaDeObjetos();
    }

    void CrearPiscinaDeObjetos()
    {
        // Un bucle for a través de la lista de objetos, desactivandolos y añadiendolos a la lista
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);

            monstruo = obj.GetComponent<Monstruo>();
            PonerSprite(monstruo);
            //SeleccionarAnimLayer(monstruo);

            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // Los pone como hijos del Enemigos Manager
        }
    }

    void PonerSprite(Monstruo mons) //Los layer de animacion tienen que estar en el mismo orden que la lista de sprites
    {
        for (int i = 0; i < pooledSprites.Count; i++)
        {
            if (mons.MontruoSpriteR.sprite == null) //Si no tiene sprite, se agrega un sprite y se elimina de la lista
            {            
                mons.MontruoSpriteR.sprite = pooledSprites[i]; //
                pooledSprites.Remove(pooledSprites[i]);
            }
        }
    }

   

    public void RemoverMonstruoMuertoLista()
    {
        pooledObjects.Remove(pooledObjects[0]); //Es 0

    }

    public GameObject GetPooledObject()
    {
        // Por la cantidad de objetos que hay en la lista
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // Si el objeto NO esta activo, retorna ese objeto
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // Si esta activo, retorna nulo  
        return null;
    }
}