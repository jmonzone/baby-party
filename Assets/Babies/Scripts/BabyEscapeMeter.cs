using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyEscapeMeter : MonoBehaviour
{
    [SerializeField] private Slider meter;
    [SerializeField] private Image fill;

    private BabyEscape escape;

    private void Awake()
    {
        escape = GetComponent<BabyEscape>();
        escape.OnEscapeHasStarted += OnEscapeStarted;
        escape.OnEscapeHasCanceled += OnEscapeCanceled;

        meter.value = 0;
        meter.minValue = 0;
        meter.maxValue = BabyEscape.EscapeMax;
        meter.gameObject.SetActive(false);
    }

    private void OnEscapeStarted()
    {
        meter.gameObject.SetActive(true);
    }

    private void OnEscapeCanceled()
    {
        meter.gameObject.SetActive(false);
    }

    private void Update()
    {
        meter.value = escape.EscapeTimer;
        fill.color = Color.Lerp(Color.yellow, Color.red, meter.value / BabyEscape.EscapeMax);
    }
}
