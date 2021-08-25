using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyAnimations : BabyBehaviour
{
    [SerializeField] private Sprite crawlSprite;
    [SerializeField] private Sprite crySprite;
    [SerializeField] private Sprite sleepSprite;
    [SerializeField] private Transform yawnSprite;

    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        var sleep = GetComponent<BabySleep>();
        sleep.OnRestStateHasChanged += OnRestStateChanged;

        var movement = GetComponent<BabyMovement>();
        movement.OnDirectionHasChanged += OnDirectionChanged;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void OnStateChanged(BabyState state)
    {
        base.OnStateChanged(state);

        switch(state)
        {
            case BabyState.CRYING:
                spriteRenderer.sprite = crySprite;
                break;
            case BabyState.SLEEPING:
                spriteRenderer.sprite = sleepSprite;
                break;
            default:
                spriteRenderer.sprite = crawlSprite;
                break;
        }
    }

    private void OnDirectionChanged()
    {
        var scale = spriteRenderer.transform.localScale;
        scale.x = Direction.x >= 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        spriteRenderer.transform.localScale = scale;
    }

    private void OnRestStateChanged(RestState state)
    {
        yawnSprite.gameObject.SetActive(state == RestState.TIRED);
    }
}
