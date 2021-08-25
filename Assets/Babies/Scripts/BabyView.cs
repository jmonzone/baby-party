using System;
using UnityEngine;


public abstract class BabyBehaviour : MonoBehaviour
{
    protected float colliderRadius = 0.25f;

    private BabyStateMachine state;
    private BabySleep sleep;
    private BabyMovement movement;

    protected Vector2 Direction => movement.Direction; 

    protected virtual void Awake()
    {
        var baby = GetComponent<BabyView>();
        baby.OnBabyHasSpawned += OnSpawn;
        baby.OnBabyHasBeenPickedUp += OnPickUp;
        baby.OnBabyHasBeenDropped += OnDropped;

        state = GetComponent<BabyStateMachine>();
        state.OnCurrentStateHasChanged += OnStateChanged;
        
        movement = GetComponent<BabyMovement>();
        movement.OnDestinationHasBeenReached += OnDestinationReached;
        
        sleep = GetComponent<BabySleep>();
        sleep.OnRestHasReachedZero += OnRestZero;
        sleep.OnSleepHasStarted += OnSleepStarted;
        sleep.OnSleepHasEnded += OnSleepEnded;
    }
    protected virtual void OnSpawn() { }
    protected virtual void OnPickUp() { }
    protected virtual void OnDropped() { }
    protected virtual void OnRestZero() { }
    protected virtual void OnSleepStarted() { }
    protected virtual void OnSleepEnded() { }
    protected virtual void OnStateChanged(BabyState state) { }
    protected virtual void OnDestinationReached() { }
}

public class BabyView : MonoBehaviour
{
    public event Action OnBabyHasSpawned;
    public event Action OnBabyHasBeenPickedUp;
    public event Action OnBabyHasBeenDropped;

    public void Init(RoundManager roundManager)
    {
        roundManager.OnRoundHasEnded += _ => OnRoundEnded();
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        OnBabyHasSpawned?.Invoke();
    }

    public void PickUp(Transform carryPosition)
    {
        transform.SetParent(carryPosition);
        transform.localPosition = Vector3.zero;
        Debug.Log($"{name} has been picked up.");
        OnBabyHasBeenPickedUp?.Invoke();
    }

    public void Drop()
    {
        transform.parent = null;
        Debug.Log($"{name} has been dropped.");
        OnBabyHasBeenDropped?.Invoke();
    }

    private void OnRoundEnded()
    {
        gameObject.SetActive(false);
    }
}
