using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCropController : MonoBehaviour
    {
        public enum Crops
        {
            None,
            Crop1
        }

        [SerializeField] private Crops _currentCrop;

        public Crops CurrentCrop { get { return _currentCrop; } }

        public void SetCurrentCrop(string crop)
        {
            _currentCrop = (Crops)System.Enum.Parse(typeof(Crops), crop);
        }

    }
}