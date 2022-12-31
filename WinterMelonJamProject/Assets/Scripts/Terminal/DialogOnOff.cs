using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnOff : MonoBehaviour
{
    [SerializeField] private Animator _dialogAnimator;
    [SerializeField] private GameObject _target;

    private Player.PlayerCameraMove _playerCameraMove;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _playerCameraMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerCameraMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerCameraMove.enabled = false;

            StopAllCoroutines();
            StartCoroutine(GoToTerminal());

            _dialogAnimator.gameObject.SetActive(true);
            _dialogAnimator.Play("DialogAnim", -1, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(Return());
            _playerCameraMove.enabled = true;
            _dialogAnimator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _dialogAnimator.Play("DialogAnimOff", -1, 0);
        }
    }

    private IEnumerator GoToTerminal()
    {
        while (_camera.transform.position != _target.transform.position)
        {
            Vector3 direction = new Vector3(_target.transform.position.x, _target.transform.position.y, -10f);

            _camera.transform.position = Vector3.Lerp(_camera.transform.position, direction, 2.5f * Time.deltaTime);
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 1.25f, 2.5f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Return()
    {
        while (_camera.orthographicSize != 2.5f)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 2.5f, 2.5f * Time.deltaTime);
            yield return null;
        }
    }
}
