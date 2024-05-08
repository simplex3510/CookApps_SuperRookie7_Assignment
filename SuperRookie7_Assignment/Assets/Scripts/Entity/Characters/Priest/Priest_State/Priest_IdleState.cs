using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Priest_IdleState : BaseState
{
    public Priest_IdleState(Priest entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Priest>().AnimCntrllr.SetTrigger(GetEntity<Priest>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }
}
