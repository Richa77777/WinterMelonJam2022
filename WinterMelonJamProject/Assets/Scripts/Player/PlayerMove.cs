using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        public enum PlayerRotates
        {
            Up,
            Down,
            Right,
            Left
        }

        private PlayerRotates _playerRotate = PlayerRotates.Down;

        public PlayerRotates PlayerRotate { get { return _playerRotate; } }

        [Range(0.0f, 5.0f)]
        [SerializeField] private float _normalSpeed;
        private float _currentSpeed;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        float _horizontalAxis;
        float _verticalAxis;

        private AudioSource _audioSource;
        [SerializeField] private AudioClip _stepSound;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            _currentSpeed = _normalSpeed;
        }

        private void Update()
        {
            if (_horizontalAxis != 0 || _verticalAxis != 0)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetFloat("Horizontal", _horizontalAxis);
                _animator.SetFloat("Vertical", _verticalAxis);

                switch(_horizontalAxis)
                {
                    case > 0:
                        _playerRotate = PlayerRotates.Right;
                        break;

                    case < 0:
                        _playerRotate = PlayerRotates.Left;
                        break;
                }

                switch (_verticalAxis)
                {
                    case > 0:
                        _playerRotate = PlayerRotates.Up;
                        break;

                    case < 0:
                        _playerRotate = PlayerRotates.Down;
                        break;
                }
            }

            else if (_horizontalAxis == 0 || _verticalAxis == 0)
            {
                _animator.SetBool("isWalking", false);

                switch (_playerRotate)
                {
                    case PlayerRotates.Up:
                        _animator.SetFloat("Vertical", 1);
                        break;

                    case PlayerRotates.Down:
                        _animator.SetFloat("Vertical", -1);
                        break;

                    case PlayerRotates.Right:
                        _animator.SetFloat("Horizontal", 1);
                        break;

                    case PlayerRotates.Left:
                        _animator.SetFloat("Horizontal", -1);
                        break;

                }
            }

            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    _currentSpeed *= 2;
            //}

            //if (Input.GetKeyUp(KeyCode.LeftShift))
            //{
            //    _currentSpeed = _normalSpeed;
            //}
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _horizontalAxis = Input.GetAxisRaw("Horizontal");
            _verticalAxis = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(_horizontalAxis, _verticalAxis);

            _rigidbody.velocity = direction.normalized * _currentSpeed;
        }

        public void PlaySound()
        {
            _audioSource.Play();
        }
    }
}
