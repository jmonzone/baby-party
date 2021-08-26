using System;
using UnityEngine;

public class BabyMovement : BabyBehaviour
{
    [SerializeField] private float crawlSpeed = 1.0f;
    [SerializeField] private LayerMask wallLayer;

    private bool canMove = false;
    private Vector3 targetPosition;

    private Vector2 direction;
    public new Vector2 Direction
    {
        get => direction;
        private set
        {
            direction = value;
            OnDirectionHasChanged?.Invoke();
        }
    }

    public event Action OnDestinationHasBeenReached;
    public event Action OnDirectionHasChanged;

    protected override void OnSpawn()
    {
        base.OnSpawn();
    }

    protected override void OnStateChanged(BabyState state)
    {
        base.OnStateChanged(state);
        if (state == BabyState.WANDER) {
            targetPosition = GetRandomPosition();
            canMove = true;
        }
        else if (state == BabyState.WANTS_TO_ESCAPE)
        {
            targetPosition = GetEscapePosition();
            canMove = true;
        }
        else canMove = false;
    }

    private Vector3 GetEscapePosition()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 10, wallLayer);
        var closestCollider = colliders[0];
        foreach(var collider in colliders)
        {
            var currentDistance = Vector3.Distance(closestCollider.ClosestPoint(transform.position), transform.position);
            var distanceToCompare = Vector3.Distance(collider.ClosestPoint(transform.position), transform.position);

            if (distanceToCompare < currentDistance) closestCollider = collider;
        }
        return closestCollider.ClosestPoint(transform.position);
    }
    private Vector3 GetRandomPosition()
    {
        var direction = UnityEngine.Random.Range(0, 2) == 0 ? Vector3.up : Vector3.right;
        var sign = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        var randomPosition = (direction * sign) + transform.position;
        return randomPosition;
    }

    private void Update()
    {
        if (canMove)
        {
            Direction = targetPosition - transform.position;
            transform.position += (Vector3)direction.normalized * crawlSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                OnDestinationHasBeenReached?.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(canMove) Gizmos.DrawLine(transform.position, targetPosition);
    }
}
