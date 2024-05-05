using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_IdleState : BaseState
{
    public Knight_IdleState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Knight).AnimCntrllr.SetTrigger((entity as Knight).AnimParam_Idle);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }
}
