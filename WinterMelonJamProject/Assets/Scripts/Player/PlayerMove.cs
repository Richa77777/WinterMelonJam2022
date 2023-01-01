using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider2D))]

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
        private BoxCollider2D _collider;
        private Animator _animator;

        float _horizontalAxis;
        float _verticalAxis;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip _stepSound;

        private bool _canMove = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _collider = GetComponent<BoxCollider2D>();

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
                        _collider.size = new Vector2(0.2819109f, 2.117311f);
                        break;

                    case < 0:
                        _playerRotate = PlayerRotates.Left;
                        _collider.size = new Vector2(0.2819109f, 2.117311f);
                        break;
                }

                switch (_verticalAxis)
                {
                    case > 0:
                        _playerRotate = PlayerRotates.Up;
                        _collider.size = new Vector2(0.40346241f, 2.11731052f);
                        break;

                    case < 0:
                        _playerRotate = PlayerRotates.Down;
                        _collider.size = new Vector2(0.40346241f, 2.11731052f);
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
            if (_canMove)
            {
                Move();
            }
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

        //public void BlockMoving()
        //{
        //    _canMove = false;


        //    _horizontalAxis = 0f;
        //    _verticalAxis = 0f;

        //    _rigidbody.velocity = new Vector2(_horizontalAxis, _verticalAxis) * _currentSpeed;
        //}

        //public void UnblockMoving()
        //{
        //    _canMove = true;
        //}
    }
}
