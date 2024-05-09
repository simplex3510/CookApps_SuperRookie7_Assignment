using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Priest_DieState : BaseState
{
    private float respawnTimer;

    public Priest_DieState(Priest entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedCharactersDict.Remove(GetEntity<Priest>().GetHashCode());
        GetEntity<Priest>().HealthBar.gameObject.SetActive(false);
        GetEntity<Priest>().AnimCntrllr.SetTrigger(GetEntity<Priest>().AnimParam_Die);
        respawnTimer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedCharactersDict.Add(GetEntity<Priest>().GetHashCode(), GetEntity<Priest>());
        GetEntity<Priest>().HealthBar.gameObject.SetActive(true);
    }

    public override void OnStateUpdate()
    {
        if (GetEntity<Priest>().StatusData.so_StatusData.RespawnTime < Time.time - respawnTimer)
        {
            EntityManager.Instance.Respawn<Priest>(GetEntity<Priest>());
        }
    }
}
