using UnityEngine;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Goblin : BaseMonster
{
    private void Awake()
    {
        animCntrllr = GetComponentInChildren<Animator>();
        AssignAnimationParameters();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();


        curState = EState.Idle;
        FSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();
    }

    private void OnEnable()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetHashCode(), this);
    }

    private void Start()
    {
        StartCoroutine(UpdateFSM());
    }

    private void OnDisable()
    {
        EntityManager.Instance.spawnedMonstersDict.Remove(GetHashCode());
    }

    protected override void InitializeStateDict()
    {
        StateDict[EState.Idle] = new Goblin_IdleState(this);
        StateDict[EState.Move] = new Goblin_MoveState(this);
        StateDict[EState.Attack] = new Goblin_AttackState(this);
        StateDict[EState.Die] = new Goblin_DieState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (statusData as Goblin_Status).CurrentHP = statusData.so_StatusData.maxHP;
    }
}