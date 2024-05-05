using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_StatusData", menuName = "Scriptable Object/SO_StatusData")]
public class SO_StatusData : ScriptableObject
{
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float str;
    [SerializeField]
    private float spd;
    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private float atk_rng;
    [SerializeField]
    private float atk_time;

    public float MaxHP { get => maxHP;  }
    public float STR { get => str; }
    public float SPD { get => spd; }
    public float RespawnTime { get => respawnTime; }

    public float ATK_RNG { get => atk_rng; }
    public float ATK_Time { get => atk_time; }
}
