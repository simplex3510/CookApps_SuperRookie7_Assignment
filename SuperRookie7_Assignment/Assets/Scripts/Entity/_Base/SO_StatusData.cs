using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_StatusData", menuName = "Scriptable Object/SO_StatusData")]
public class SO_StatusData : ScriptableObject
{
    public float maxHP;
    public float str;
    public float spd;

    public float atk_rng;
    public float atk_time;
}
