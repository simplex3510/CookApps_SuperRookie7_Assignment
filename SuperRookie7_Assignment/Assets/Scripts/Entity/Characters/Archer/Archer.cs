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
        KnightFSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();
    }

    public override void Start()
    {
        
    }

    protected override void InitializeStateDict()
    {
        StateDict[EState.Idle] = new Archer_IdleState(this);
        StateDict[EState.Move] = new Archer_MoveState(this);
        StateDict[EState.Battle] = new Archer_AttackState(this);
        StateDict[EState.Skill] = new Archer_SkillState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (StatusData as Archer_Status).CurrentHP = StatusData.so_StatusData.MaxHP;
    }
}
