using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private UpgradeManager upgradeManager;

    [Header("UI References")]
    [SerializeField] private Text roundText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text timeRemainingText;
    [SerializeField] private Button startRoundbutton;
    [SerializeField] private Transform upgradeButtonsParent;

    private List<UpgradeView> upgradeButtons = new List<UpgradeView>();

    private void Awake()
    {
        roundManager.OnRoundHasStarted += OnRoundStarted;
        roundManager.OnRoundHasEnded += OnRoundEnded;
        roundManager.OnTimeRemainingChanged += UpdateTimeRemainingText;

        moneyManager.OnMoneyHasChanged += UpdateMoneyText;

        startRoundbutton.onClick.AddListener(() => roundManager.StartRound());

        upgradeButtonsParent.GetComponentsInChildren(upgradeButtons);

        var i = 0;
        upgradeManager.upgrades.ForEach((upgrade) =>
        {
            upgradeButtons[i].Init(upgrade);
            i++;
        });
    }

    private void OnRoundStarted(int round)
    {
        UpdateRoundText(round);
        startRoundbutton.gameObject.SetActive(false);
    }

    private void OnRoundEnded(int round)
    {
        UpdateRoundText(round + 1);
        startRoundbutton.gameObject.SetActive(true);
    }

    private void UpdateRoundText(int round)
    {
        roundText.text = $"Round {round + 1}";
    }

    private void UpdateTimeRemainingText(float timeRemaining)
    {
        timeRemainingText.text = $"Time Remaining: {Mathf.CeilToInt(timeRemaining)}s";
    }

    private void UpdateMoneyText(int money)
    {
        moneyText.text = $"${money}";
    }
}
