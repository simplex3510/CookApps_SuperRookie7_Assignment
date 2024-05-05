using FSM.Base.State;
using UnityEngine;

public class Goblin_DieState : BaseState
{
    private float respawnTime;

    public Goblin_DieState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Die);
        GetEntity<Goblin>().StartRespawnCoroutine();
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Goblin Die");
    }
}
