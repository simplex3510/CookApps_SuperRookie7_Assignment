using Entity.Base;
using UnityEngine;

public class Archer_Status : BaseStatus
{
    // ����ü �ӵ�
    [SerializeField]
    private float projectile_SPD;
     
    public float Projectile_SPD { get => projectile_SPD; }
}
