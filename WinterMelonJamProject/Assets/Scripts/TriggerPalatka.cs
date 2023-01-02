using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPalatka : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canvas.SetActive(false);
        }
    }
}
