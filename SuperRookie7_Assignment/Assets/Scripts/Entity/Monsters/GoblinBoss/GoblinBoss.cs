using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class GoblinBoss : Goblin
{
    #region Unity Life-Cycle
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetHashCode(), this);
    }

    public override void Start()
    {
        base.Start();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void OnTriggerStay2D(Collider2D collider)
    {
        base.OnTriggerStay2D(collider);
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);
    }
    #endregion

    protected override void InitializeEntity()
    {
        base.InitializeEntity();
    }

    protected override void InitializeStateDict()
    {
        base.InitializeStateDict();
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        base.InitializeStatusData();
    }
}