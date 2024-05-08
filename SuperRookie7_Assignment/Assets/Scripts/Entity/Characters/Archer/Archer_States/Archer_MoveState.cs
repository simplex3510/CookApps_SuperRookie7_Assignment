using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_MoveState : BaseState
{
    public Archer_MoveState(Archer entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Archer>().AnimCntrllr.SetTrigger(GetEntity<Archer>().AnimParam_Move);
        GetEntity<Archer>().CheckNearestMonster();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        GetEntity<Archer>().MoveToTarget();
    }
}
