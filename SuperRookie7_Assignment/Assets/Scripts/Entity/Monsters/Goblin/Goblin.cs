using UnityEngine;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Goblin : BaseMonster
{
    [SerializeField]
    private Transform rootTransfrom;
    public Transform Root { get { return rootTransfrom; } }

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
    }

    private void OnEnable()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetHashCode(), this);
        StartCoroutine(UpdateFSM());
    }

    private void OnDisable()
    {
        StopCoroutine(UpdateFSM());
        EntityManager.Instance.spawnedMonstersDict.Remove(GetHashCode());
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