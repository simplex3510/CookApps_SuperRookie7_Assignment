using Entity.Base;

public class Knight_Status : BaseStatus
{
    [UnityEngine.SerializeField]
    private float currentHP;

    public float CurrentHP { get { return currentHP; } set { currentHP = value; } }
}
