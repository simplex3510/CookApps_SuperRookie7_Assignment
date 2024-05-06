using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_DieState : BaseState
{
    private const float AnimDuration_Die = 0.4f;
    private float timer = 0f;
    private bool isRendererOff = false;

    public Goblin_DieState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedMonstersDict.Remove(GetEntity<Goblin>().GetHashCode());
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Die);
        timer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetEntity<Goblin>().GetHashCode(), GetEntity<Goblin>());
        GetEntity<Goblin>().GetComponent<SpriteRenderer>().enabled = true;
        isRendererOff = false;
    }

    public override void OnStateUpdate()
    {
        if (!isRendererOff && AnimDuration_Die < Time.time - timer)
        {
            EntityManager.Instance.readyMonsterQueue.Enqueue(GetEntity<Goblin>());
            GetEntity<Goblin>().GetComponent<SpriteRenderer>().enabled = false;
            isRendererOff = true;
        }
    }
}
