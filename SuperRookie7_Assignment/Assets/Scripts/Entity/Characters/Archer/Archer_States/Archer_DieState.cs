using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DieState : BaseState
{
    private float respawnTimer;

    public Archer_DieState(Archer entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedCharactersDict.Remove(GetEntity<Archer>().GetHashCode());
        GetEntity<Archer>().HealthBar.gameObject.SetActive(false);
        GetEntity<Archer>().AnimCntrllr.SetTrigger(GetEntity<Archer>().AnimParam_Die);
        respawnTimer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedCharactersDict.Add(GetEntity<Archer>().GetHashCode(), GetEntity<Archer>());
        GetEntity<Archer>().HealthBar.gameObject.SetActive(true);
    }

    public override void OnStateUpdate()
    {
        if (GetEntity<Archer>().StatusData.so_StatusData.RespawnTime < Time.time - respawnTimer)
        {
            EntityManager.Instance.Respawn<Archer>(GetEntity<Archer>());
        }
    }
}
