using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Knight_BattleState : BaseState
{
    private float timeInterval;
    private float lastAttackTime = 0;
    private float timeSinceLastAttack;

    public Knight_BattleState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Knight>().AnimCntrllr.SetBool(GetEntity<Knight>().AnimParam_Battle, true);
    }

    public override void OnStateExit()
    {
        GetEntity<Knight>().AnimCntrllr.SetBool(GetEntity<Knight>().AnimParam_Battle, false);
        GetEntity<Knight>().CheckNearestMonster();
    }

    public override void OnStateUpdate()
    {
        timeInterval = 1f / GetEntity<Knight>().StatusData.so_StatusData.ATK_Time;
        timeSinceLastAttack = Time.time - lastAttackTime;

        if (timeInterval <= timeSinceLastAttack)
        {
            GetEntity<Knight>().AnimCntrllr.SetFloat(GetEntity<Knight>().AnimParam_AtkTime,
                                                     GetEntity<Knight>().StatusData.so_StatusData.ATK_Time);
            GetEntity<Knight>().AnimCntrllr.SetTrigger(GetEntity<Knight>().AnimParam_Attack);
        }
    }
}
