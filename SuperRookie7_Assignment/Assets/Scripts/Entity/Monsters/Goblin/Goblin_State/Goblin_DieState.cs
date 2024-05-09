using FSM.Base.State;
using Singleton.Manager;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_DieState : BaseState
{
    private const float AnimDuration_Die = 0.7f;
    private float animationTimer = 0f;
    private bool isRendererOff = false;

    public Goblin_DieState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        EntityManager.Instance.spawnedMonstersDict.Remove(GetEntity<Goblin>().GetHashCode());
        EntityManager.Instance.readyMonsterQueue.Enqueue(GetEntity<Goblin>());

        GetEntity<Goblin>().HealthBar.gameObject.SetActive(false);
        GetEntity<Goblin>().CircleCollider.enabled = false;
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Die);
        animationTimer = Time.time;
    }

    public override void OnStateExit()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetEntity<Goblin>().GetHashCode(), GetEntity<Goblin>());

        GetEntity<Goblin>().HealthBar.gameObject.SetActive(true);
        GetEntity<Goblin>().CircleCollider.enabled = true;
        GetEntity<Goblin>().GetComponent<SpriteRenderer>().enabled = true;
        isRendererOff = false;
    }

    public override void OnStateUpdate()
    {
        if (!isRendererOff && AnimDuration_Die < Time.time - animationTimer)
        {
            GetEntity<Goblin>().GetComponent<SpriteRenderer>().enabled = false;
            isRendererOff = true;
        }
    }
}
