using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        public enum PlayerStates
        {
            Idle,
            Walk,
            Run
        }

        private PlayerStates _playerState;

        public PlayerStates PlayerState { get { return _playerState; } }

        [Range(0.0f, 5.0f)]
        [SerializeField] private float _normalSpeed;
        private float _currentSpeed;

        private Rigidbody2D _rigidbody;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentSpeed = _normalSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _currentSpeed *= 2;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _currentSpeed = _normalSpeed;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical);

            _rigidbody.velocity = direction.normalized * _currentSpeed;
        }
    }
}
