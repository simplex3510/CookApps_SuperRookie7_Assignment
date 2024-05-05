using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.Base.State;

public class Goblin_BattleState : BaseState
{
    private float timeInterval;
    private float lastAttackTime = 0;

    public Goblin_BattleState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Goblin>().AnimCntrllr.SetBool(GetEntity<Goblin>().AnimParam_Battle, true);
    }

    public override void OnStateExit()
    {
        GetEntity<Goblin>().AnimCntrllr.SetBool(GetEntity<Goblin>().AnimParam_Battle, false);
        GetEntity<Goblin>().CheckNearestCharacter();
    }

    public override void OnStateUpdate()
    {
        timeInterval = 1f / GetEntity<Goblin>().StatusData.so_StatusData.ATK_Time;

        if (timeInterval <= Time.time - lastAttackTime)
        {
            GetEntity<Goblin>().AnimCntrllr.SetFloat(GetEntity<Goblin>().AnimParam_AtkTime,
                                                     GetEntity<Goblin>().StatusData.so_StatusData.ATK_Time);
            GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Attack);
        }
    }
}
