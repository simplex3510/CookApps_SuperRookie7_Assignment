using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Assassin_DieState : BaseState
{
    private float respawnTimer;

    public Assassin_DieState(Assassin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedCharactersDict.Remove(GetEntity<Assassin>().GetHashCode());
        GetEntity<Assassin>().AnimCntrllr.SetTrigger(GetEntity<Assassin>().AnimParam_Die);
        respawnTimer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedCharactersDict.Add(GetEntity<Assassin>().GetHashCode(), GetEntity<Assassin>());
    }

    public override void OnStateUpdate()
    {
        if (GetEntity<Assassin>().StatusData.so_StatusData.RespawnTime < Time.time - respawnTimer)
        {
            EntityManager.Instance.Respawn<Assassin>(GetEntity<Assassin>());
        }
    }
}
