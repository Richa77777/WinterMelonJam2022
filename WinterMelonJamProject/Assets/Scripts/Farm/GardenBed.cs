using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Player;
using TMPro;
using System.Linq;

namespace Farm
{
    public class GardenBed : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private CropsBase _cropsBase;

        public enum Phases
        {
            Phase0, // Empty
            Phase1, // Seeds
            Phase2, // Sprouts
            Phase3, // Adult
            Phase4 // Fetus
        }

        private Phases _phase;

        public Phases Phase { get { return _phase; } }

        [Header("Times")]
        private const int _p1Time = 3;
        private const int _p2Time = 5;
        private const int _p3Time = 5;
        private const float q = 0.04f;

        private float n1;
        private float n2;
        private float n3;

        private PlayerMove _player;
        private PlayerCropController _playerCropController;

        [SerializeField] private CropsBase.CropsEnum _currentCrop;
        private float _currentTime;

        private SpriteRenderer _spriteRenderer;

        private PlayerLevelController _playerLevelController;

        [SerializeField] private TextMeshProUGUI _textTimeCrop;

        [SerializeField] private LayerMask _gardenBedLayerMask;

        [SerializeField] Vector2 _sizeBoxCast;

        private Collider2D _collider;

        private AudioSource _audioSource;

        [SerializeField] private AudioClip _seed;
        [SerializeField] private AudioClip _gather;

        private void Start()
        {
            _playerCropController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCropController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _player = _playerCropController.gameObject.GetComponent<PlayerMove>();
            _playerLevelController = _player.gameObject.GetComponent<PlayerLevelController>();
            _collider = GetComponent<Collider2D>();
            _audioSource = GetComponent<AudioSource>();
            _cropsBase = _playerCropController.gameObject.GetComponent<CropsBase>();

            float x = (_playerLevelController.CurrentLevel - 1) * q;

            n1 = _p1Time * (1 - x);
            n2 = _p2Time * (1 - x);
            n3 = _p3Time * (1 - x);

            float i = n1 + n2 + n3;

            _textTimeCrop.text = i.ToString("F2");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                float x = (_playerLevelController.CurrentLevel - 1) * q;

                n1 = _p1Time * (1 - x);
                n2 = _p2Time * (1 - x);
                n3 = _p3Time * (1 - x);

                float i = n1 + n2 + n3;

                _textTimeCrop.text = i.ToString("F2");
            }

            if (_phase != Phases.Phase0)
            {
                Growth();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_phase == Phases.Phase0)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector3(_player.gameObject.transform.position.x, _player.gameObject.transform.position.y - 1f, 0f), _sizeBoxCast, _gardenBedLayerMask);

                if (hits.Contains(_collider))
                {
                    _audioSource.PlayOneShot(_seed);
                    Sowing(_playerCropController.CurrentCrop);
                }

            }

            if (_phase == Phases.Phase4)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector3(_player.gameObject.transform.position.x, _player.gameObject.transform.position.y - 1f, 0f), _sizeBoxCast, _gardenBedLayerMask);

                if (hits.Contains(_collider))
                {
                    _audioSource.PlayOneShot(_gather);

                    _phase = Phases.Phase0;

                    CheckSprite();

                    _currentCrop = CropsBase.CropsEnum.None;
                    _currentTime = 0f;

                    _playerCropController.AddCropValue(1);
                }
            }
        }

        private void Sowing(CropsBase.CropsEnum crop)
        {
            if (crop != CropsBase.CropsEnum.None)
            {
                float x = (_playerLevelController.CurrentLevel - 1) * q;

                n1 = _p1Time * (1 - x);
                n2 = _p2Time * (1 - x);
                n3 = _p3Time * (1 - x);

                _currentCrop = crop;
                _currentTime = n1 + n2 + n3;

                _textTimeCrop.text = _currentTime.ToString("F2");

                _phase = Phases.Phase1;
                CheckSprite();
                Growth();
            }
        }

        private void Growth()
        {
            _currentTime -= Time.deltaTime;

            if (_phase == Phases.Phase1 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time)
            {
                _phase = Phases.Phase2;
                CheckSprite();
            }

            if (_phase == Phases.Phase2 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time - _p2Time)
            {
                _phase = Phases.Phase3;
                CheckSprite();
            }

            if (_phase == Phases.Phase3 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time - _p2Time - _p3Time)
            {
                _phase = Phases.Phase4;
                CheckSprite();
            }
        }

        private void CheckSprite()
        {
            Sprite[] currentSprites = new Sprite[5];

            switch (_currentCrop)
            {
                case CropsBase.CropsEnum.Crop1:
                    currentSprites = _cropsBase._CropsBase._Crop1.GardenBedSprites;
                    break;
            }

            switch (_phase)
            {
                case Phases.Phase0:
                    _spriteRenderer.sprite = currentSprites[0];
                    break;

                case Phases.Phase1:
                    _spriteRenderer.sprite = currentSprites[1];
                    break;

                case Phases.Phase2:
                    _spriteRenderer.sprite = currentSprites[2];
                    break;

                case Phases.Phase3:
                    _spriteRenderer.sprite = currentSprites[3];
                    break;

                case Phases.Phase4:
                    _spriteRenderer.sprite = currentSprites[4];
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}
