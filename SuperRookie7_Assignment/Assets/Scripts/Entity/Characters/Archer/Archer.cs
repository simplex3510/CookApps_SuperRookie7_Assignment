using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;

public partial class Archer : BaseCharacter
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

    protected override void InitializeStateDict()
    {
        StateDict[EState.Idle] = new Archer_IdleState(this);
        StateDict[EState.Move] = new Archer_MoveState(this);
        StateDict[EState.Attack] = new Archer_AttackState(this);
        StateDict[EState.Skill] = new Archer_SkillState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (statusData as Archer_Status).CurrentHP = statusData.so_StatusData.maxHP;
    }
}
