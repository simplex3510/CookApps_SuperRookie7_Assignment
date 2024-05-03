using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Scriptable Object/SO_StatusData")]
public class SO_StatusData : ScriptableObject
{
    public float maxHP;
    public float str;

    public float atk_rng;
    public float atk_time;

    public float skl_rng;
    public float skl_time;
}
