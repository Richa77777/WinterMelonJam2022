using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Farm
{
    public class GardenBed : MonoBehaviour
    {
        private UnityEvent _phaseChanged = new UnityEvent();

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

        [Header("Crops")]
        [SerializeField] private Sprite[] _crop1 = new Sprite[5];

        [Header("Times")]
        private const int _p1Time = 3;
        private const int _p2Time = 5;
        private const int _p3Time = 5;
        private float _currentTime;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _phaseChanged.AddListener(CheckSprite);

            _spriteRenderer = GetComponent<SpriteRenderer>();

            _currentTime = _p1Time + _p2Time + _p3Time;

            _phase = Phases.Phase1;
            _phaseChanged?.Invoke();
        }

        private void Update()
        {
            Growth();
        }

        private void Growth()
        {
            _currentTime -= Time.deltaTime;

            if (_phase == Phases.Phase1 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time)
            {
                _phase = Phases.Phase2;
                _phaseChanged?.Invoke();
            }

            if (_phase == Phases.Phase2 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time - _p2Time)
            {
                _phase = Phases.Phase3;
                _phaseChanged?.Invoke();
            }

            if (_phase == Phases.Phase3 && _currentTime < (_p1Time + _p2Time + _p3Time) - _p1Time - _p2Time - _p3Time)
            {
                _phase = Phases.Phase4;
                _phaseChanged?.Invoke();
            }
        }

        private void CheckSprite()
        {
            switch(_phase)
            {
                case Phases.Phase0:
                    _spriteRenderer.sprite = _crop1[0];
                    break;

                case Phases.Phase1:
                    _spriteRenderer.sprite = _crop1[1];
                    break;

                case Phases.Phase2:
                    _spriteRenderer.sprite = _crop1[2];
                    break;

                case Phases.Phase3:
                    _spriteRenderer.sprite = _crop1[3];
                    break;

                case Phases.Phase4:
                    _spriteRenderer.sprite = _crop1[4];
                    break;
            }
        }
    }
}
