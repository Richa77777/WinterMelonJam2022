using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Terminal
{
    public class OrderController : MonoBehaviour
    {
        private const int k = 2;
        private const int p = 3;

        private int _orderNumber = 1;

        private int _cropsAmount;
        private int _award;

        [SerializeField] private TextMeshProUGUI _cropAmountText;
        [SerializeField] private TextMeshProUGUI _awardText;

        private Player.PlayerCropController _cropController;
        private Player.PlayerMoneyController _moneyController;

        [SerializeField] private GameObject _dialogTab;

        private Animator _animator;
        private Animator _cameraAnimator;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip _terminal;
        [SerializeField] private AudioClip _terminal2;
        [SerializeField] private AudioClip _taskCompleted;
        [SerializeField] private AudioClip _notCompleted;

        private Camera _camera;

        [SerializeField] private DialogOnOff _trigger;


        private void Start()
        {
            _cropController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerCropController>();
            _moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerMoneyController>();

            _camera = Camera.main;
            _animator = GetComponent<Animator>();
            _cameraAnimator = _camera.GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            GenerateOrder();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_dialogTab.activeInHierarchy == true)
                {
                    Time.timeScale = 0f;

                    _trigger._stop = true;
                    _cameraAnimator.Play("GoToTerminal", -1, 0f);
                    StartCoroutine(CheckOrderConditions());
                }
            }
        }

        public IEnumerator CheckOrderConditions()
        {
            if (_cropController.CurrentCropValue >= _cropsAmount)
            {
                _dialogTab.SetActive(false);
                _animator.Play("Accept", -1, 0);
                _audioSource.PlayOneShot(_taskCompleted);
                _cropController.AddCropValue(-_cropsAmount);
                _moneyController.AddMoney(_award);
                _orderNumber++;
                GenerateOrder();

                yield return new WaitForSecondsRealtime(2f);

                TabOn();
            }

            else if (_cropController.CurrentCropValue < _cropsAmount)
            {
                _dialogTab.SetActive(false);
                _animator.Play("Angry", -1, 0);
                _audioSource.PlayOneShot(_notCompleted);

                yield return new WaitForSecondsRealtime(2f);

                TabOn();

            }
        }

        private void TabOn()
        {
            Time.timeScale = 1f;
            _dialogTab.SetActive(true);
            _trigger._stop = false;
            _animator.Play("Nooone", -1, 0);
        }

        private void GenerateOrder()
        {
            if (_orderNumber == 1)
            {
                _cropsAmount = 1;
                _award = 5;

                SetText();

            }

            else if (_orderNumber != 1)
            {
                _cropsAmount = Mathf.RoundToInt(k + (_orderNumber * 1.2f));
                _award = Mathf.RoundToInt(p + (_orderNumber * 2.5f));

                SetText();
            }
        }

        private void SetText()
        {
            _cropAmountText.text = _cropsAmount.ToString();
            _awardText.text = _award.ToString();
        }

        public void PlayTerminal()
        {
            _audioSource.PlayOneShot(_terminal);
        }

        public void PlayTerminal2()
        {
            _audioSource.PlayOneShot(_terminal2);
        }
    }
}
