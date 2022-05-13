using UnityEngine;

public class ActivadoraPlatform : MonoBehaviour
{
    [Header("Plataforma a activar")]
    [SerializeField] GameObject sigPlataformaGO;

    [Header("Bala a activar")]
    [SerializeField] bool activaBala;
    [SerializeField] GameObject balasGO;

    [Header("Plataforma a activar")]
    [SerializeField] bool desactivarOtraPlatf;
    [SerializeField] GameObject prePlatafGO;

    private bool activoPlataforma;
    public void ActivarPlatformaGO()
    {
        if (!activoPlataforma)
        {
            activoPlataforma = true;
            sigPlataformaGO.SetActive(true);

            if(desactivarOtraPlatf)
            {
                prePlatafGO.SetActive(false);
            }
            if(activaBala)
            {
                balasGO.SetActive(true);
            }
        }
    }
}