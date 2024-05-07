using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Knight_DieState : BaseState
{
    private float respawnTimer;

    public Knight_DieState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedCharactersDict.Remove(GetEntity<Knight>().GetHashCode());
        GetEntity<Knight>().AnimCntrllr.SetTrigger(GetEntity<Knight>().AnimParam_Die);
        respawnTimer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedCharactersDict.Add(GetEntity<Knight>().GetHashCode(), GetEntity<Knight>());
    }

    public override void OnStateUpdate()
    {
        if (GetEntity<Knight>().StatusData.so_StatusData.RespawnTime < Time.time - respawnTimer)
        {
            EntityManager.Instance.Respawn<Knight>(GetEntity<Knight>());
        }
    }
}
