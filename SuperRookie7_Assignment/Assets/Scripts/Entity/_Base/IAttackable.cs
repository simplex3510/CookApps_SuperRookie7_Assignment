namespace Entity.Base
{
    public interface IAttackable
    {
        public void AttackTarget();
        public bool AttackedEntity(float Damage);
    }
}