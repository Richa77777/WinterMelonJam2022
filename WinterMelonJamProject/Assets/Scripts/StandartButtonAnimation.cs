using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(Animator))]

public class StandartButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RuntimeAnimatorController _animatorController;
    
    public RuntimeAnimatorController AnimatorController { set { _animatorController = value; } }


    private Animator _animator;

    private Button _button;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _animatorController;

        _button = GetComponent<Button>();
        _button.transition = Selectable.Transition.None;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetBool("Down", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _animator.SetBool("Down", false);
    }
}
