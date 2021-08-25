using System;
using System.Collections;
using UnityEngine;

public enum BabyState
{
    IDLE,
    CRAWLING,
    ESCAPE,
    CRYING,
    SLEEPING,
    CARRIED
}

public class BabyStateMachine : BabyBehaviour
{
    [SerializeField] private LayerMask wallLayer;

    private BabyState currentState;
    public new BabyState CurrentState
    {
        get => currentState;
        private set
        {
            currentState = value;
            OnCurrentStateHasChanged?.Invoke(currentState);
        }
    }

    public event Action<BabyState> OnCurrentStateHasChanged;

    protected override void OnSpawn()
    {
        base.OnSpawn();
        CurrentState = BabyState.IDLE;
        StartCoroutine(StateUpdate());
    }

    protected override void OnPickUp()
    {
        base.OnPickUp();
        CurrentState = BabyState.CARRIED;
    }

    protected override void OnDropped()
    {
        base.OnDropped();
        CurrentState = BabyState.IDLE;
    }

    protected override void OnRestZero()
    {
        base.OnRestZero();
        CurrentState = BabyState.CRYING;
    }
    protected override void OnSleepStarted()
    {
        base.OnSleepStarted();
        CurrentState = BabyState.SLEEPING;
    }

    protected override void OnSleepEnded()
    {
        base.OnSleepEnded();
        CurrentState = BabyState.CRAWLING;
    }

    protected override void OnDestinationReached()
    {
        base.OnDestinationReached();
        if (CurrentState != BabyState.CRAWLING) return;

        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.25f, wallLayer);
        if (colliders.Length > 0) CurrentState = BabyState.ESCAPE;
        else CurrentState = BabyState.IDLE;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    private IEnumerator StateUpdate()
    {
        while (true)
        {
            switch(CurrentState)
            {
                case BabyState.IDLE:
                    yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 5.0f));
                    if (CurrentState == BabyState.IDLE) CurrentState = BabyState.CRAWLING;
                    break;
            }
            yield return null;
        }
        
    }
}
