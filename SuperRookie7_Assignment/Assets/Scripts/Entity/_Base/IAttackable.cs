namespace Entity.Base
{
    public interface IAttackable
    {
        public void AttackTarget();
        public void AttackedEntity(float Damage);
    }
}