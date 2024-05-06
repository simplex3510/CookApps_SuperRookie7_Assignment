using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Knight : BaseCharacter
{
    #region Unity Life-Cycle
    private void Awake()
    {
        animCntrllr = GetComponent<Animator>();
        AssignAnimationParameters();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();

        curState = EState.Idle;
        KnightFSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();
        EntityManager.Instance.spawnedCharactersDict.Add(GetHashCode(), this);
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
        StateDict[EState.Idle] = new Knight_IdleState(this);
        StateDict[EState.Move] = new Knight_MoveState(this);
        StateDict[EState.Battle] = new Knight_BattleState(this);
        StateDict[EState.Skill] = new Knight_SkillState(this);
        StateDict[EState.Die] = new Knight_DieState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        (StatusData as Knight_Status).CurrentHP = StatusData.so_StatusData.MaxHP;
    }
}
