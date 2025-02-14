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
        private Player.PlayerCameraMove _playerCameraMove;

        [SerializeField] private GameObject _dialogTab;

        private Animator _animator;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip _terminal;
        [SerializeField] private AudioClip _terminal2;
        [SerializeField] private AudioClip _taskCompleted;
        [SerializeField] private AudioClip _notCompleted;

        private Camera _camera;

        [SerializeField] private DialogOnOff _trigger;

        private bool _goToTerminalStop = true;
        private bool _inProgress = false;

        private void Start()
        {
            _cropController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerCropController>();
            _moneyController = _cropController.gameObject.GetComponent<Player.PlayerMoneyController>();
            _playerCameraMove = _moneyController.gameObject.GetComponent<Player.PlayerCameraMove>();

            _camera = Camera.main;
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            GenerateOrder();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                if (_dialogTab.activeInHierarchy == true && _inProgress == false)
                {
                    _trigger.StopGoToDialog();

                    StartCoroutine(GoToTerminal());
                    StartCoroutine(CheckOrderConditions());
                }
            }
        }

        private IEnumerator CheckOrderConditions()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            if (_cropController.CurrentCropValue >= _cropsAmount)
            {
                _dialogTab.SetActive(false);
                
                _animator.Play("Accept", -1, 0);

                //_audioSource.clip = _taskCompleted;
                _audioSource.PlayOneShot(_taskCompleted);
                
                _cropController.AddCropValue(-_cropsAmount);
                _moneyController.AddMoney(_award);
                
                _orderNumber++;
                
                GenerateOrder();
            }

            else if (_cropController.CurrentCropValue < _cropsAmount)
            {
                _dialogTab.SetActive(false);

                _animator.Play("Angry", -1, 0);

                _audioSource.clip = _notCompleted;
                _audioSource.Play();
            }

            yield return new WaitForSeconds(2f);
            
            TabOn();;
        }

        private IEnumerator GoToTerminal()
        {
            _goToTerminalStop = false;

            Vector3 direction = new Vector3(transform.localPosition.x, transform.localPosition.y, -10f);

            while (_camera.transform.localPosition != Vector3.Lerp(_camera.transform.localPosition, direction, 2.5f * Time.deltaTime) && !_goToTerminalStop)
            {
                if (_playerCameraMove.enabled == false)
                {
                    _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, direction, 2.5f * Time.deltaTime);
                    yield return null;
                }
            }

            _goToTerminalStop = true;

            if (_playerCameraMove.enabled == false)
            {
                _trigger.StartGoToDialog();
            }
        }

        private void TabOn()
        {
            if (_playerCameraMove.enabled == false)
            {
                _dialogTab.SetActive(true);
            }

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
            _audioSource.clip = _terminal;
            _audioSource.Play();
        }

        public void PlayTerminal2()
        {
            _audioSource.clip = _terminal2;
            _audioSource.Play();
        }

        public void StopGoToTerminal()
        {
            _goToTerminalStop = true;
        }
    }
}
