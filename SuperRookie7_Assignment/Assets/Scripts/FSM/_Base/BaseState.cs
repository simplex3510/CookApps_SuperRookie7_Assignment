using Entity.Base;

namespace FSM.Base.State
{
    public enum EState : int
    {
        None = 0,
        Idle,
        Move,
        Attack,
        Skill,
        Die,
        Size
    }

    public abstract class BaseState : IStatable
    {
        protected BaseEntity entity;

        protected BaseState(BaseEntity entity) => this.entity = entity;

        public T GetEntity<T>() where T : BaseEntity => (T)entity;

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
