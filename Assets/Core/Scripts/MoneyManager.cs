using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundManager roundManager;

    [Header("Options")]
    [SerializeField] private int startingMoney = 200;

    private int money;
    public int Money
    {
        get => money;
        private set
        {
            money = value;
            OnMoneyHasChanged?.Invoke(money);
        }
    }

    public event Action<int> OnMoneyHasChanged;

    private void Start()
    {
        Money = startingMoney;
        roundManager.OnRoundHasEnded += OnRoundHasEnded;
    }

    private void OnRoundHasEnded(int round)
    {
        Money += roundManager.CurrentRound.reward;

    }
}
