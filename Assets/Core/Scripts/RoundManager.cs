using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [SerializeField] private List<Round> rounds;

    private int roundIndex = -1;
    private float timeRemaining = 0;

    public bool RoundIsActive { get; private set; }
    public Round CurrentRound => rounds[roundIndex];
    public float TimeRemaining
    {
        get => timeRemaining;
        private set
        {
            timeRemaining = value;
            OnTimeRemainingChanged?.Invoke(timeRemaining);
        }

    }

    public event Action<int> OnRoundHasStarted;
    public event Action<int> OnRoundHasEnded;
    public event Action<float> OnTimeRemainingChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void StartRound()
    {
        roundIndex++;
        RoundIsActive = true;
        Debug.Log($"Round {roundIndex + 1} has started.");
        OnRoundHasStarted?.Invoke(roundIndex);
        StartCoroutine(RoundUpdate());
    }

    private IEnumerator RoundUpdate()
    {
        TimeRemaining = CurrentRound.duration;
        while (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            yield return null;
        }

        RoundIsActive = false;
        Debug.Log($"Round {roundIndex + 1} has ended.");
        OnRoundHasEnded?.Invoke(roundIndex);
    }
}
