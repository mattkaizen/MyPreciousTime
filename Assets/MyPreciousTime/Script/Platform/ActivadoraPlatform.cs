using UnityEngine;

public class ActivadoraPlatform : MonoBehaviour
{
    [Header("Plataforma a activar")]
    [SerializeField] GameObject sigPlataformaGO;

    private bool activoPlataforma;
    public void ActivarPlatformaGO()
    {
        if (!activoPlataforma)
        {
            activoPlataforma = true;
            sigPlataformaGO.SetActive(true);
        }
    }
}