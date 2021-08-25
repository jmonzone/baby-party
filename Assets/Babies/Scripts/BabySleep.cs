using System;
using System.Collections;
using UnityEngine;

public enum RestState
{
    RESTED,
    TIRED,
    CRYING,
    ASLEEP
}
public class BabySleep : BabyBehaviour
{
    [SerializeField] private float baseRestorationSpeed = 20.0f;
    [SerializeField] private float baseDepletionSpeed = 5.0f;
    [SerializeField] private LayerMask blanketLayer;

    private float energy = MAX_ENERGY;
    private float energyDepletionSpeed;
    private RestState currentState = RestState.RESTED;

    public const float MAX_ENERGY = 100;
    public float Energy
    {
        get => energy;
        private set
        {
            energy = Mathf.Clamp(value, 0, MAX_ENERGY);
            if (energy == 0) OnRestHasReachedZero?.Invoke();
            if (CurrentState != RestState.ASLEEP) UpdateRestState();
        }
    }

    public RestState CurrentState
    {
        get => currentState;
        private set
        {
            currentState = value;
            OnRestStateHasChanged?.Invoke(currentState);
        }
    }

    public event Action OnSleepHasStarted;
    public event Action OnSleepHasEnded;
    public event Action OnRestHasReachedZero;
    public event Action<RestState> OnRestStateHasChanged;

    protected override void OnSpawn()
    {
        base.OnSpawn();
        Energy = MAX_ENERGY;
        energyDepletionSpeed = baseDepletionSpeed * UnityEngine.Random.Range(0.5f, 1.5f);
    }

    protected override void OnPickUp()
    {
        base.OnPickUp();
        StopAllCoroutines();
        UpdateRestState();
    }

    protected override void OnDropped()
    {
        base.OnDropped();
        if (CurrentState == RestState.RESTED) return;

        var colliders = Physics2D.OverlapCircleAll(transform.position, colliderRadius, blanketLayer);
        if (colliders.Length > 0) {
            var blanket = colliders[0];
            Sleep(blanket.transform.position);
        }
    }

    protected override void OnStateChanged(BabyState state)
    {
        base.OnStateChanged(state);
        if (state == BabyState.SLEEPING) CurrentState = RestState.ASLEEP;
    }

    private void Sleep(Vector3 position)
    {
        transform.position = position;
        OnSleepHasStarted?.Invoke();
        StartCoroutine(SleepUpdate());
    }

    private IEnumerator SleepUpdate()
    {
        while (Energy < MAX_ENERGY)
        {
            Energy += Time.deltaTime * baseRestorationSpeed;
            yield return null;
        }

        OnSleepHasEnded?.Invoke();
        UpdateRestState();
    }

    private void Update()
    {
        if (CurrentState != RestState.ASLEEP) Energy -= Time.deltaTime * energyDepletionSpeed;
    }

    private void UpdateRestState()
    {
        if (energy > 33) CurrentState = RestState.RESTED;
        else if (energy > 0) CurrentState = RestState.TIRED;
        else if (energy == 0)CurrentState = RestState.CRYING;
    }
}
