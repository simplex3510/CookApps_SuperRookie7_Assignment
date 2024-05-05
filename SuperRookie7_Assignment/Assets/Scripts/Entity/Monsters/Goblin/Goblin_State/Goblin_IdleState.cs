using Entity.Base;
using FSM.Base.State;

public class Goblin_IdleState : BaseState
{
    public Goblin_IdleState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {
        GetEntity<Goblin>().CheckNearestCharacter();
    }

    public override void OnStateUpdate()
    {

    }
}