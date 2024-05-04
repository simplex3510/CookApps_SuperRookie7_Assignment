using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Goblin_AttackState : BaseState
{
    public Goblin_AttackState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Goblin).AnimCntrllr.SetBool((entity as Goblin).AnimParam_Attack, true);
    }

    public override void OnStateExit()
    {
        (entity as Goblin).AnimCntrllr.SetBool((entity as Goblin).AnimParam_Attack, false);
        (entity as Goblin).CheckNearestEnemy();
    }

    public override void OnStateUpdate()
    {

    }
}
