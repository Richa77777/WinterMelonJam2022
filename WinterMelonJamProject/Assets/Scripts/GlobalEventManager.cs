using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static GlobalEventManager EventManager;

    [SerializeField] private UnityEvent<int, int> _levelUp = new UnityEvent<int, int>();

    private void Start()
    {
        EventManager = this;    
    }

    public void LevelUp(int currentPlayerMoney, int cost)
    {
        if (currentPlayerMoney >= cost)
        {
            _levelUp?.Invoke(1, cost);
            print("LVL UP!");
        }
    }
}
