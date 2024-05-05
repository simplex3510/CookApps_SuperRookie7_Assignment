using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_StatusData", menuName = "Scriptable Object/SO_Character_StatusData")]
public class SO_Character_StatusData : SO_StatusData
{
    [SerializeField]
    public float skl_rng;
    [SerializeField]
    public float skl_time;

    public float SKL_RNG { get => skl_rng; }
    public float SKL_Time { get => skl_time; }
}
