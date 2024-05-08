using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Priest_MoveState : BaseState
{
    public Priest_MoveState(Priest entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Priest>().AnimCntrllr.SetTrigger(GetEntity<Priest>().AnimParam_Move);
        GetEntity<Priest>().CheckNearestMonster();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        GetEntity<Priest>().MoveToTarget();
    }
}
