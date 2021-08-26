using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCarMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private Vector3 targetPosition;
    private bool canMove = false;

    public event Action OnDestinationHasBeenReached;

    private void Awake()
    {
        var state = GetComponent<ToyCarStateMachine>();
        state.OnCurrentStateHasChanged += OnStateChanged;
    }

    private void OnStateChanged(ToyCarState state)
    {
        if (state == ToyCarState.WANDER)
        {
            targetPosition = UnityEngine.Random.insideUnitCircle * 3.0f;
            canMove = true;
        }
        else canMove = false;
    }

    private void Update()
    {
        if (canMove)
        {
            var direction = targetPosition - transform.position;
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                OnDestinationHasBeenReached?.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (canMove) Gizmos.DrawLine(transform.position, targetPosition);
    }
}
