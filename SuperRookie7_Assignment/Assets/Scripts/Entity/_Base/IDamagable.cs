namespace Entity.Base
{
    public interface IDamagable
    {
        public void AttackTarget();
        public void DamagedEntity(float Damage);
    }
}