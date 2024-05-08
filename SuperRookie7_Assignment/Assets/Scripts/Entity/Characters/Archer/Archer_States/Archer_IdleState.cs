using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Archer_IdleState : BaseState
{
    public Archer_IdleState(Archer entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Archer>().AnimCntrllr.SetTrigger(GetEntity<Archer>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
