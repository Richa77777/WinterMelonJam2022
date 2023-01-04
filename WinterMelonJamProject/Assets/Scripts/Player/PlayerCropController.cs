using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Farm;

namespace Player
{
    public class PlayerCropController : MonoBehaviour
    {
        [SerializeField] private CropsBase _cropsBase;

        [SerializeField] private CropsBase.CropsEnum _currentCrop;

        [SerializeField] private int _currentCropValue;
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private Animator _chooseCropAnimator;
        [SerializeField] private Image _currentCropImage;

        [SerializeField] private float _imageDistanceX, _imageDistanceY;
        public CropsBase.CropsEnum CurrentCrop { get { return _currentCrop; } }
        public int CurrentCropValue { get { return _currentCropValue; } }

        private void Start()
        {
            _cropsBase = GameObject.FindGameObjectWithTag("Player").GetComponent<CropsBase>();

            SetCurrentCropImage();
        }

        public void SetCurrentCrop(string crop)
        {
            _currentCrop = (CropsBase.CropsEnum)System.Enum.Parse(typeof(CropsBase.CropsEnum), crop);
            SetCurrentCropImage();
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

            if (_currentCropImage.gameObject.activeInHierarchy == true)
            {
                MoveImageToCursor();
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

        private void SetCurrentCropImage()
        {
            switch(_currentCrop)
            {
                case CropsBase.CropsEnum.None:
                    _currentCropImage.sprite = null;
                    _currentCropImage.gameObject.SetActive(false);
                    break;

                case CropsBase.CropsEnum.Crop1:
                    _currentCropImage.sprite = _cropsBase._CropsBase._Crop1.Sprite;
                    _currentCropImage.gameObject.SetActive(true);
                    break;
            }
        }    

        private void MoveImageToCursor()
        {
            _currentCropImage.transform.position = new Vector3(Input.mousePosition.x + _imageDistanceX, Input.mousePosition.y + _imageDistanceY, 0f);
        }
    }
}