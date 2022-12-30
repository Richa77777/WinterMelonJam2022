using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Player;

namespace Farm
{
    public class GardenBed : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
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

        [Header("CropsBase")]
        [SerializeField] private Sprite[] _crop1 = new Sprite[5];

        [Header("Times")]
        private const int _p1Time = 3;
        private const int _p2Time = 5;
        private const int _p3Time = 5;

        private PlayerCropController _playerCropController;

        private PlayerCropController.Crops _currentCrop;
        private float _currentTime;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerCropController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCropController>();
        }

        private void Update()
        {
            if (_phase != Phases.Phase0)
            {
                Growth();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            print("Клавиша нажата.");

            if (_phase == Phases.Phase0)
            {
                print("Фаза нулевая.");
                Sowing(_playerCropController.CurrentCrop);
            }

            if (_phase == Phases.Phase4)
            {
                _phase = Phases.Phase0;

                CheckSprite();

                _currentCrop = PlayerCropController.Crops.None;
                _currentTime = 0f;

                _playerCropController.AddCropValue(1);
            }
        }

        private void Sowing(PlayerCropController.Crops crop)
        {
            print("Проверка на культуру.");

            if (crop != PlayerCropController.Crops.None)
            {
                print("Проверка пройдена.");
                _currentCrop = crop;
                _currentTime = _p1Time + _p2Time + _p3Time;
                _phase = Phases.Phase1;
                CheckSprite();
                Growth();

                print("Культура посажена.");
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

            switch(_currentCrop)
            {
                case PlayerCropController.Crops.Crop1:
                    currentSprites = _crop1;
                    break;
            }

            switch(_phase)
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
            throw new System.NotImplementedException();
        }
    }
}
