using System;
using UnityEngine;

public class BabyEscape : BabyBehaviour
{
    [SerializeField] private float escapeSpeed = 2.0f;
    public float EscapeTimer { get; private set; } = 0;
    public const float EscapeMax = 10;

    private bool isEscaping = false;
    public bool IsEscaping
    {
        get => isEscaping;
        private set
        {
            isEscaping = value;
            if (isEscaping) OnEscapeHasStarted?.Invoke();
            else OnEscapeHasCanceled?.Invoke();
        }
    }

    public event Action OnEscapeHasStarted;
    public event Action OnEscapeHasCanceled;
    public event Action OnBabyHasEscaped;

    protected override void OnStateChanged(BabyState state)
    {
        base.OnStateChanged(state);
        IsEscaping = state == BabyState.ESCAPE;
    }

    private void Update()
    {
        if (IsEscaping) EscapeTimer += Time.deltaTime * escapeSpeed;
        else EscapeTimer = 0;

        if (EscapeTimer >= EscapeMax) Escape();
    }

    private void Escape()
    {
        gameObject.SetActive(false);
        OnBabyHasEscaped?.Invoke();
    }
}
