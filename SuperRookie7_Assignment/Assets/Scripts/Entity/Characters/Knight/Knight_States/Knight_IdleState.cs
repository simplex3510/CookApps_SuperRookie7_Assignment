using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_IdleState : BaseState
{
    public Knight_IdleState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Knight>().AnimCntrllr.SetTrigger(GetEntity<Knight>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {
        GetEntity<Knight>().CheckNearestMonster();
    }

    public override void OnStateUpdate()
    {

    }
}
