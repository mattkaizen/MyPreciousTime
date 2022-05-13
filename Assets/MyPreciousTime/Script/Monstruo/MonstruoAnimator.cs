using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstruoAnimator : MonoBehaviour
{
    private Animator monsAnim;

    private void Awake()
    {
        monsAnim = GetComponent<Animator>();
    }

    public void ActivarAnimDaño()
    {
        monsAnim.SetBool("Dañar", true);
    }
}