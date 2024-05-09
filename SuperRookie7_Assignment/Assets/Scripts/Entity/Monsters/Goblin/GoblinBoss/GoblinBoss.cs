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
        circleCollider.radius = StatusData.so_StatusData.ATK_RNG * 0.5f;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    private void OnDisable()
    {
        EntityManager.Instance.spawnedMonstersDict.Remove(GetHashCode());
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