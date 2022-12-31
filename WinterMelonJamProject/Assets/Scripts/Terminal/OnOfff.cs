using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terminal
{
    public class OnOfff : MonoBehaviour
    {
        private GameObject _dialor;

        private void Start()
        {
            _dialor = gameObject;
        }

        public void On()
        {
            _dialor.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void Off()
        {
            _dialor.SetActive(false);
        }
    }
}