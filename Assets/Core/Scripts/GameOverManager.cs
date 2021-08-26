using System;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BabyManager babyManager;

    public event Action OnGameOver;

    private void Awake()
    {
        babyManager.OnBabyHasEscaped += _ => GameOver();
    }

    private void GameOver()
    {
        Debug.Log("A baby has escaped, game over!");
        OnGameOver?.Invoke();
    }
}
