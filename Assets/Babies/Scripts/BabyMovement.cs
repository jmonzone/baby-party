using System;
using UnityEngine;
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
        if (state == BabyState.CRAWLING) FindTargetPosition();
        else canMove = false;
    }

    private void FindTargetPosition()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 10, wallLayer);
        if (colliders.Length > 0 && UnityEngine.Random.Range(0, 1.0f) < .25f)
        {
            var collider = colliders[UnityEngine.Random.Range(0, colliders.Length)];
            targetPosition = collider.ClosestPoint(transform.position);
        }
        else targetPosition = UnityEngine.Random.insideUnitCircle * 2.5f;

        canMove = true;
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
