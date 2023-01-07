using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terminal
{
    public class DialogOnOff : MonoBehaviour
    {
        [SerializeField] private Animator _dialogAnimator;
        [SerializeField] private GameObject _target;

        private Player.PlayerCameraMove _playerCameraMove;
        private OrderController _orderController;

        private Camera _camera;

        private bool _goToDialogStop = true;
        private bool _returnStop = true;

        public bool GoToDialogStop { get { return _goToDialogStop; } set { _goToDialogStop = value; } }

        private void Start()
        {
            _camera = Camera.main;
            _playerCameraMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerCameraMove>();
            _orderController = GameObject.FindGameObjectWithTag("Terminal").GetComponent<OrderController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _returnStop = true;

                StartCoroutine(GoToDialog());

                _dialogAnimator.gameObject.SetActive(true);
                _dialogAnimator.Play("DialogAnim", -1, 0);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _playerCameraMove.enabled = true;

                _orderController.StopGoToTerminal();
                StopGoToDialog();

                StartCoroutine(Return());

                _dialogAnimator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                _dialogAnimator.Play("DialogAnimOff", -1, 0);
            }
        }

        private IEnumerator GoToDialog()
        {
            print("Идет GoToDialog");

            _goToDialogStop = false;
            _playerCameraMove.enabled = false;

            Vector3 direction = new Vector3(_target.transform.position.x, _target.transform.position.y, -10f);

            while (_camera.transform.position != Vector3.Lerp(_camera.transform.position, direction, 2.5f * Time.deltaTime) && _goToDialogStop == false)
            {
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, direction, 2.5f * Time.deltaTime);
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 1.25f, 2.5f * Time.deltaTime);
                yield return null;
            }

            _goToDialogStop = true;
            print("Конец GoToDialog");
        }

        private IEnumerator Return()
        {
            print("Идет Return");
            _returnStop = false;

            while (_camera.orthographicSize != Mathf.Lerp(_camera.orthographicSize, 2.5f, 2.5f * Time.deltaTime) && !_returnStop)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 2.5f, 2.5f * Time.deltaTime);
                yield return null;
            }

            _returnStop = true;
            print("Конец Return");
        }

        public void StartGoToDialog()
        {
            StartCoroutine(GoToDialog());
        }

        public void StopGoToDialog()
        {
            _goToDialogStop = true;
        }

        public void StartReturn()
        {
            StartCoroutine(Return());
        }

        public void StopReturn()
        {
            _returnStop = true;
        }
    }
}
