using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Player
{
    public class PlayerLevelController : MonoBehaviour
    {
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private TextMeshProUGUI _text;
        public int CurrentLevel { get { return _currentLevel; } }

        private Player.PlayerMoneyController _moneyController;

        private void Start()
        {
            _text.text = _currentLevel.ToString();

            _moneyController = gameObject.GetComponent<PlayerMoneyController>();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                GlobalEventManager.EventManager.LevelUp(_moneyController.MoneyValue, 4 * (_currentLevel));
            }
        }

        public void AddLevel(int value, int cost)
        {
            if (_currentLevel + value <= 25)
            {
                _currentLevel += value;
                _moneyController.AddMoney(-cost);
                _text.text = _currentLevel.ToString();
            }
        }
    }
}
