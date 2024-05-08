using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin_IdleState : BaseState
{
    public Assassin_IdleState(Assassin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Assassin>().AnimCntrllr.SetTrigger(GetEntity<Assassin>().AnimParam_Idle);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }
}
