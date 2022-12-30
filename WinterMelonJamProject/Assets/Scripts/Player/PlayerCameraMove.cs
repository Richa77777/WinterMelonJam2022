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
            direction.x = Mathf.Clamp(direction.x, -9.2f, 10.2f);
            direction.y = Mathf.Clamp(direction.y, 0.27f, 2.9f);

            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, direction, _cameraSpeed * Time.deltaTime);
        }
    }
}