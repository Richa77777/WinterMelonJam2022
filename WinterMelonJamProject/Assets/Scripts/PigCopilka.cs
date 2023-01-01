using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigCopilka : MonoBehaviour
{
    [SerializeField] private GameObject _space;
    private Player.PlayerMoneyController _playerMoneyController;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _playerMoneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PlayerMoneyController>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (_space.activeInHierarchy == true && _playerMoneyController.MoneyValue >= 1000)
            {
                _playerMoneyController.AddMoney(-1000);
                _animator.gameObject.SetActive(true);
                _animator.Play("Won", -1, 0);
            }
        }
    }

}
