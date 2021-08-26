using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToyCarState
{
    IDLE,
    WANDER,
    LURE,
    BROKEN
}

public class ToyCarStateMachine : MonoBehaviour
{
    private ToyCarState currentState;
    public ToyCarState CurrentState
    {
        get => currentState;
        private set
        {
            currentState = value;
            OnCurrentStateHasChanged?.Invoke(currentState);

        }
    }

    public event Action<ToyCarState> OnCurrentStateHasChanged;

    private void Awake()
    {
        if (RoundManager.Instance.RoundIsActive) StartCoroutine(StateUpdate());
        RoundManager.Instance.OnRoundHasStarted += OnRoundStarted;
        RoundManager.Instance.OnRoundHasEnded += OnRoundEnded;

        var movement = GetComponent<ToyCarMovement>();
        movement.OnDestinationHasBeenReached += OnDestinationReached;
    }
    private void OnRoundStarted(int round)
    {
        StartCoroutine(StateUpdate());
    }

    private void OnRoundEnded(int round)
    {
        StopAllCoroutines();
    }

    private void OnDestinationReached()
    {
        CurrentState = ToyCarState.IDLE;
    }

    private IEnumerator StateUpdate()
    {
        while (true)
        {
            switch (CurrentState)
            {
                case ToyCarState.IDLE:
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 5.0f));
                    if (CurrentState == ToyCarState.IDLE) CurrentState = ToyCarState.WANDER;
                    break;
            }
            yield return null;
        }

    }
}
