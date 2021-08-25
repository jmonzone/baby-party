using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Music Box")]

public class MusicBox : Upgrade
{
    public MusicBoxView prefab;
    public override void Purchase()
    {
        var musicBox = Instantiate(prefab);
        musicBox.transform.position = Random.insideUnitCircle * 3.0f;
    }
}
