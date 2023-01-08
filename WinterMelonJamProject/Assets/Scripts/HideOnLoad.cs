using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnLoad : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
