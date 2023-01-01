using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayOrNo : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.Play("FadeOff", -1, 0);
        Invoke(nameof(Offad), 2f);
    }

    private void Offad()
    {
        _animator.gameObject.SetActive(false);
    }
}
