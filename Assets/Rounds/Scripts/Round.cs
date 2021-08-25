using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BabyAmount
{
    public BabyView baby;
    public int amount;
}

[CreateAssetMenu(menuName = "Round")]
public class Round : ScriptableObject
{
    public float duration = 30f;
    public int reward = 300;
    public List<BabyAmount> babies;

    public int TotalBabies
    {
        get
        {
            var total = 0;
            foreach(var baby in babies)
            {
                total += baby.amount;
            }
            return total;
        }
    }
}
