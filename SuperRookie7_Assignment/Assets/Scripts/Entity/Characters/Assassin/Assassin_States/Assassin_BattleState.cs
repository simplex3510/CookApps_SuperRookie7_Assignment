using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Assassin_BattleState : BaseState
{
    private float timeInterval;

    public Assassin_BattleState(Assassin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Assassin>().AnimCntrllr.SetBool(GetEntity<Assassin>().AnimParam_Battle, true);
    }

    public override void OnStateExit()
    {
        GetEntity<Assassin>().AnimCntrllr.SetBool(GetEntity<Assassin>().AnimParam_Battle, false);
    }

    public override void OnStateUpdate()
    {
        timeInterval = 1f / GetEntity<Assassin>().StatusData.so_StatusData.ATK_SPD;

        if (timeInterval <= Time.time - GetEntity<Assassin>().LastAttackTime)
        {
            GetEntity<Assassin>().AnimCntrllr.SetFloat(GetEntity<Assassin>().AnimParam_AtkTime,
                                                     GetEntity<Assassin>().StatusData.so_StatusData.ATK_SPD);
            GetEntity<Assassin>().AnimCntrllr.SetTrigger(GetEntity<Assassin>().AnimParam_Attack);
            GetEntity<Assassin>().LastAttackTime = Time.time;
        }
    }
}
