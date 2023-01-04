using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCameraMove : MonoBehaviour
    {
        private GameObject _target;
        private Camera _camera;

        [Range(0.0f, 5.0f)]
        [SerializeField] private float _cameraSpeed;

        [SerializeField] private float _minAxisX, _maxAxisX;
        [SerializeField] private float _minAxisY, _maxAxisY;

        private void Awake()
        {
            _target = gameObject;
            _camera = Camera.main;
        }

        private void Start()
        {
            _camera.transform.localPosition = new Vector3(_target.transform.localPosition.x, _target.transform.localPosition.y, _camera.transform.localPosition.z);
        }

        private void Update()
        {
            CameraMove();
        }

        private void CameraMove()
        {
            Vector3 direction = _target.transform.localPosition;
            direction.z = _camera.transform.localPosition.z;
            direction.x = Mathf.Clamp(direction.x, _minAxisX, _maxAxisX);// -4.55f, 1.52f);
            direction.y = Mathf.Clamp(direction.y, _minAxisY, _maxAxisY);// -0.48f, 3.45f);

            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, direction, _cameraSpeed * Time.deltaTime);
        }
    }
}