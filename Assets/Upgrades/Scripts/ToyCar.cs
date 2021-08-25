using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Toy Car")]

public class ToyCar : Upgrade
{
    public ToyCarView prefab;
    public override void Purchase()
    {
        var car = Instantiate(prefab);
        car.transform.position = Random.insideUnitCircle * 3.0f;
    }
}
