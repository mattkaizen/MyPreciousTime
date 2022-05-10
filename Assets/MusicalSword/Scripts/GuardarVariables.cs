using UnityEngine;

public class GuardarVariables : MonoBehaviour
{ //Se debe poner en el menu principal
    public static GuardarVariables inst; 

    private KeyCode botonAtaque1;
    private KeyCode botonAtaque2;
    private KeyCode botonAtaque3;
    private KeyCode botonAtaque4;

    public KeyCode BotonAtaque1 { get => botonAtaque1; }
    public KeyCode BotonAtaque2 { get => botonAtaque2; }
    public KeyCode BotonAtaque3 { get => botonAtaque3; }
    public KeyCode BotonAtaque4 { get => botonAtaque4; }
    private void Awake()
    {
        if (GuardarVariables.inst == null)
        {
            GuardarVariables.inst = this;

            botonAtaque1 = KeyCode.A;
            botonAtaque2 = KeyCode.S;
            botonAtaque3 = KeyCode.F; //verde
            botonAtaque4 = KeyCode.D;

            DontDestroyOnLoad(gameObject); //No destruye este script
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CambiarInput()
    {

    }
}