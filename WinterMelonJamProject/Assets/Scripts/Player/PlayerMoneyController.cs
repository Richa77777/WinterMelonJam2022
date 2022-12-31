using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Player
{
    public class PlayerMoneyController : MonoBehaviour
    {
        [SerializeField] private int _moneyValue;
        [SerializeField] private TextMeshProUGUI _text;

        public int Money { get { return _moneyValue; } }

        public void AddMoney(int value)
        {
            _moneyValue += value;
        }
    }
}