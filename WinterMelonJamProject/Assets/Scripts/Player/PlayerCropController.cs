using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        [SerializeField] private int _currentCropValue;
        [SerializeField] private TextMeshProUGUI _text;

        public Crops CurrentCrop { get { return _currentCrop; } }

        public void SetCurrentCrop(string crop)
        {
            _currentCrop = (Crops)System.Enum.Parse(typeof(Crops), crop);
        }

        public void AddCropValue(int addValue)
        {
            _currentCropValue += addValue;
            _text.text = $"x{_currentCropValue}";
        }

    }
}