using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    public void Init(Upgrade upgrade)
    {
        text.text = $"{upgrade.name} ${upgrade.price}";
        image.sprite = upgrade.sprite;
        button.onClick.AddListener(upgrade.Purchase);
    }
}
