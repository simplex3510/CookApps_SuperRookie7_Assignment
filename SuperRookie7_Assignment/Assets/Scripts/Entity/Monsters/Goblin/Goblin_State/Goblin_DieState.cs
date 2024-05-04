using FSM.Base.State;
using UnityEngine;

public class Goblin_DieState : BaseState
{
    public Goblin_DieState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Goblin).AnimCntrllr.SetTrigger((entity as Goblin).AnimParam_Die);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Goblin Die");
    }
}
