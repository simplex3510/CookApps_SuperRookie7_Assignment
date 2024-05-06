using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Goblin : BaseMonster
{
    #region Unity Life-Cycle
    private void Awake()
    {
        rootTransfrom = transform.parent;

        animCntrllr = GetComponentInChildren<Animator>();
        AssignAnimationParameters();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();

        curState = EState.Idle;
        GoblinFSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();
        EntityManager.Instance.spawnedMonstersDict.Add(GetHashCode(), this);
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateFSM());
    }

    public override void Start()
    {
        InitializeEntity();
    }
    #endregion

    protected void InitializeEntity()
    {
        InitializeStatusData();
        ChangeStateFSM(EState.Idle);
    }

    protected override void InitializeStateDict()
    {
        StateDict[EState.Idle] = new Goblin_IdleState(this);
        StateDict[EState.Move] = new Goblin_MoveState(this);
        StateDict[EState.Battle] = new Goblin_BattleState(this);
        StateDict[EState.Die] = new Goblin_DieState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (StatusData as Goblin_Status).CurrentHP = StatusData.so_StatusData.MaxHP;
    }
}