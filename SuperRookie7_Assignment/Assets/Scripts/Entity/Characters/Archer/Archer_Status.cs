using Entity.Base;

public class Archer_Status : BaseStatus
{
    [UnityEngine.SerializeField]
    private float currentHP;
    public float CurrentHP { get { return currentHP; } set { currentHP = value; } }
}
