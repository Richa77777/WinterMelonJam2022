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
        [SerializeField] private Animator _chooseCropAnimator;

        public Crops CurrentCrop { get { return _currentCrop; } }
        public int CurrentCropValue { get { return _currentCropValue; } }


        public void SetCurrentCrop(string crop)
        {
            _currentCrop = (Crops)System.Enum.Parse(typeof(Crops), crop);
        }

        public void AddCropValue(int addValue)
        {
            if (_currentCropValue + addValue <= 9999999)
            {
                _currentCropValue += addValue;
                _text.text = $"x{_currentCropValue}";
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (_chooseCropAnimator.GetBool("isAppear") == true) 
                {
                    StartCoroutine(DisappearanceTab());
                }

                else if (_chooseCropAnimator.GetBool("isAppear") == false)
                {
                    AppearanceTab();
                }
            }
        }

        private void AppearanceTab()
        {
            _chooseCropAnimator.gameObject.SetActive(true);
            _chooseCropAnimator.Play("AppearanceTab", -1, 0);
            _chooseCropAnimator.SetBool("isAppear", true);
        }

        private IEnumerator DisappearanceTab()
        {
            _chooseCropAnimator.Play("DisappearanceTab", -1, 0);
            _chooseCropAnimator.SetBool("isAppear", false);

            yield return new WaitForSeconds(0.35f);

            _chooseCropAnimator.gameObject.SetActive(false);

        }
    }
}