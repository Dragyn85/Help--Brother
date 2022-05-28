using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] Animator _animator;


    public void FadeIn()
    {
        _animator.SetTrigger("fadeIn");
    }
    public void FadeOut()
    {
        _animator.SetTrigger("fadeOut");
    }
}
