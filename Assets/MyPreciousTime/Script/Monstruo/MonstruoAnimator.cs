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

    public void ActivarAnimDa�o()
    {
        monsAnim.SetBool("Da�ar", true);
    }
}