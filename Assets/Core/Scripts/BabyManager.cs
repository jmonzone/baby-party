using System;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private BabyView babyPrefab;
    private ObjectPool<BabyView> babyPool;

    private void Awake()
    {
        roundManager.OnRoundHasStarted += _ => SpawnBabies();
        babyPool = new ObjectPool<BabyView>(babyPrefab, 10, (baby) =>
        {
            baby.Init(roundManager);
        });
    }

    private void SpawnBabies()
    {
        for (var i = 0; i < roundManager.CurrentRound.TotalBabies; i++)
        {
            var spawnPosition = UnityEngine.Random.insideUnitCircle * 3.0f;
            babyPool.NextObject.Spawn(spawnPosition);
        }
    }
}
