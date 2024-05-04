using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Knight : BaseCharacter
{
    private void Awake()
    {
        animCntrllr = GetComponent<Animator>();
        AssignAnimationParameters();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();


        curState = EState.Idle;
        FSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();
    }

    private void OnEnable()
    {
        EntityManager.Instance.spawnedCharactersDict.Add(GetHashCode(), this);
    }

    private void OnDisable()
    {
        EntityManager.Instance.spawnedCharactersDict.Remove(GetHashCode());
    }

    protected override void InitializeStateDict()
    {
        StateDict[EState.Idle] = new Knight_IdleState(this);
        StateDict[EState.Move] = new Knight_MoveState(this);
        StateDict[EState.Attack] = new Knight_AttackState(this);
        StateDict[EState.Skill] = new Knight_SkillState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (statusData as Knight_Status).CurrentHP = statusData.so_StatusData.maxHP;
    }
}
