using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Knight_AttackState : BaseState
{
    public Knight_AttackState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Knight).AnimCntrllr.SetBool((entity as Knight).AnimParam_Attack, true);
    }

    public override void OnStateExit()
    {
        (entity as Knight).AnimCntrllr.SetBool((entity as Knight).AnimParam_Attack, false);
    }

    public override void OnStateUpdate()
    {

    }
}
