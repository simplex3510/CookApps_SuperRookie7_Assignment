using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_BattleState : BaseState
{
    private float timeInterval;

    public Archer_BattleState(Archer entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Archer>().AnimCntrllr.SetBool(GetEntity<Archer>().AnimParam_Battle, true);
    }

    public override void OnStateExit()
    {
        GetEntity<Archer>().AnimCntrllr.SetBool(GetEntity<Archer>().AnimParam_Battle, false);
    }

    public override void OnStateUpdate()
    {
        timeInterval = 1f / GetEntity<Archer>().StatusData.so_StatusData.ATK_SPD;

        if (timeInterval <= Time.time - GetEntity<Archer>().LastAttackTime)
        {
            GetEntity<Archer>().AnimCntrllr.SetFloat(GetEntity<Archer>().AnimParam_AtkTime,
                                                     GetEntity<Archer>().StatusData.so_StatusData.ATK_SPD);
            GetEntity<Archer>().AnimCntrllr.SetTrigger(GetEntity<Archer>().AnimParam_Attack);
            GetEntity<Archer>().LastAttackTime = Time.time;
        }
    }
}

