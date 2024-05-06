using FSM.Base.State;

public class Knight_IdleState : BaseState
{
    public Knight_IdleState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Knight>().AnimCntrllr.SetTrigger(GetEntity<Knight>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {
        GetEntity<Knight>().AnimCntrllr.ResetTrigger(GetEntity<Knight>().AnimParam_Idle);
    }

    public override void OnStateUpdate()
    {

    }
}
