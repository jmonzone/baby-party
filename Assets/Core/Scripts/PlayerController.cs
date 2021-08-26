using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform carryPosition;
    [SerializeField] private float baseSpeed = 2.0f;
    [SerializeField] private LayerMask babyLayer;

    private BabyView pickedUpBaby;
    private float Speed => baseSpeed * (IsCarryingBaby ? 0.75f : 1f);
    private bool IsCarryingBaby => pickedUpBaby != null;


    private void Update()
    {
        var direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

        transform.position += (Vector3)direction * Time.deltaTime * Speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsCarryingBaby) DropBaby();
            else PickUpBaby();
        }

    }

    private void PickUpBaby()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.5f, babyLayer);
        if (collider)
        {
            pickedUpBaby = collider.GetComponentInParent<BabyView>();
            pickedUpBaby.PickUp(carryPosition);
        }
    }

    private void DropBaby()
    {
        pickedUpBaby.Drop();
        pickedUpBaby = null;
    }
}
