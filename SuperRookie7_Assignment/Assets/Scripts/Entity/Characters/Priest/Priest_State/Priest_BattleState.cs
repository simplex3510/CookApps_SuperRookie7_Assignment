using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest_BattleState : BaseState
{
    private float timeInterval;

    public Priest_BattleState(Priest entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Priest>().AnimCntrllr.SetBool(GetEntity<Priest>().AnimParam_Battle, true);
    }

    public override void OnStateExit()
    {
        GetEntity<Priest>().AnimCntrllr.SetBool(GetEntity<Priest>().AnimParam_Battle, false);
    }

    public override void OnStateUpdate()
    {
        timeInterval = 1f / GetEntity<Priest>().StatusData.so_StatusData.ATK_SPD;

        if (timeInterval <= Time.time - GetEntity<Priest>().LastAttackTime)
        {
            GetEntity<Priest>().AnimCntrllr.SetFloat(GetEntity<Priest>().AnimParam_AtkTime,
                                                     GetEntity<Priest>().StatusData.so_StatusData.ATK_SPD);
            GetEntity<Priest>().AnimCntrllr.SetTrigger(GetEntity<Priest>().AnimParam_Attack);
            GetEntity<Priest>().LastAttackTime = Time.time;
        }
    }
}

