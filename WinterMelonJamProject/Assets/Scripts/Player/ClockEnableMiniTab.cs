using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClockEnableMiniTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _tab;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tab.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tab.SetActive(false);
    }
}
