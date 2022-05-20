using UnityEngine;

public class ActivadoraPlatform : MonoBehaviour
{
    [Header("Plataforma?")]
    [SerializeField] bool iniciaDesactivada;

    [Header("Plataforma a activar")]
    [SerializeField] GameObject sigPlataformaGO;
    [SerializeField] bool activaAnim;
    [SerializeField] Animator sigPlataformaAnim;

    [Header("Bala a activar")]
    [SerializeField] bool activaBala;
    [SerializeField] GameObject balasGO;

    [Header("Plataforma a activar")]
    [SerializeField] bool desactivarOtraPlatf;
    [SerializeField] GameObject prePlatafGO;

    private Animator platformAnim;
    private bool activoPlataforma;

    private void Awake()
    {
        platformAnim = GetComponent<Animator>();

        if (iniciaDesactivada)
        {
            platformAnim.SetBool("Desactivar", true);
        }
    }
    public void ActivarPlatformaGO()
    {
        if (!activoPlataforma)
        {
            activoPlataforma = true;
            sigPlataformaGO.SetActive(true);
            if(activaAnim)
            {
                sigPlataformaAnim.SetBool("Desactivar", false);
                sigPlataformaAnim.SetBool("Activar", true);
            }

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