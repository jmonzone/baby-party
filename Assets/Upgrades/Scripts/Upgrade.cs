using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int price;

    public abstract void Purchase();
}
