using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_StatusData", menuName = "Scriptable Object/SO_StatusData")]
public class SO_StatusData : ScriptableObject
{
    [SerializeField]
    private float max_HP;
    [SerializeField]
    private float entity_STR;
    [SerializeField]
    private float entity_SPD;
    [SerializeField]
    private float respawnTime;

    [SerializeField]
    private float attack_RNG;
    [SerializeField]
    private float attack_SPD;

    public float Max_HP { get => max_HP;  }
    public float STR { get => entity_STR; }
    public float SPD { get => entity_SPD; }
    public float RespawnTime { get => respawnTime; }

    public float ATK_RNG { get => attack_RNG; }
    public float ATK_SPD { get => attack_SPD; }
}
