using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animation))]

public class StandartButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Animation _onAnim;
    [SerializeField] private Animation _offAnim;

    public void OnPointerDown(PointerEventData eventData)
    {
        _onAnim.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _offAnim.Play();
    }
}
