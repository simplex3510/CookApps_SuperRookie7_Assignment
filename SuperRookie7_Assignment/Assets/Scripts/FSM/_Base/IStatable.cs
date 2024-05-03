namespace FSM.Base.State
{
    public interface IStatable
    {
        public void OnStateEnter();
        public void OnStateUpdate();
        public void OnStateExit();
    }
}