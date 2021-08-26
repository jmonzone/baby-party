using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyCry : BabyBehaviour
{
    [SerializeField] private float cryRadius = 2.0f;
    [SerializeField] private LayerMask cryAfflictedLayers;

    protected override void OnStateChanged(BabyState state)
    {
        base.OnStateChanged(state);
        if (state == BabyState.CRYING) Cry();
    }

    private void Cry()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, cryRadius, cryAfflictedLayers);
        foreach(var collider in colliders)
        {
            var baby = collider.GetComponentInParent<BabyView>();
            if (baby.transform != transform) baby.Scare();
        }
    }
}
