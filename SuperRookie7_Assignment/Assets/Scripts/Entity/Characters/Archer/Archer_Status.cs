using Entity.Base;
using UnityEngine;

public class Archer_Status : BaseStatus
{
    // 투사체 속도
    [SerializeField]
    private float projectile_SPD;
     
    public float Projectile_SPD { get => projectile_SPD; }
}
