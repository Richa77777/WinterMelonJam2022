using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Terminal
{
    public class OrderController : MonoBehaviour
    {
        private const int k = 2;
        private const int p = 3;

        private int _orderNumber = 1;

        private int _cropsAmount;
        private int _award;

        [SerializeField] private TextMeshProUGUI _cropAmountText;
        [SerializeField] private TextMeshProUGUI _awardText;

        private Player.PlayerCropController _cropController;
        private Player.PlayerMoneyController _moneyController;

        [SerializeField] private GameObject _dialogTab;
        private void Start()
        {
            _cropController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerCropController>();
            _moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerMoneyController>();

            GenerateOrder();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_dialogTab.activeInHierarchy == true)
                {
                    CheckOrderConditions();
                }
            }
        }

        private void CheckOrderConditions()
        {
            if (_cropController.CurrentCropValue >= _cropsAmount)
            {
                _cropController.AddCropValue(-_cropsAmount);
                _moneyController.AddMoney(_award);
                _orderNumber++;
                GenerateOrder();
            }
        }

        private void GenerateOrder()
        {
            if (_orderNumber == 1)
            {
                _cropsAmount = 1;
                _award = 5;

                SetText();

            }

            else if (_orderNumber != 1)
            {
                _cropsAmount = Mathf.RoundToInt(k + (_orderNumber * 1.2f));
                _award = Mathf.RoundToInt(p + (_orderNumber * 2.5f));

                SetText();
            }
        }

        private void SetText()
        {
            _cropAmountText.text = _cropsAmount.ToString();
            _awardText.text = _award.ToString();
        }
    }
}
