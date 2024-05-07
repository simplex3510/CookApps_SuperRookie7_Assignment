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

        circleCollider = GetComponent<CircleCollider2D>();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();

        curState = EState.Idle;
        KnightFSM = new FiniteStateMachine(StateDict[curState]);

        InitializeStatusData();

        EntityManager.Instance.spawnedCharactersDict.Add(GetHashCode(), this);

        StartCoroutine(UpdateFSM());
    }

    public override void Start()
    {
        InitializeEntity();
    }

    private void FixedUpdate()
    {
        CircleCollider.radius = StatusData.so_StatusData.ATK_RNG / 2;
        CircleCollider.offset = new Vector2 (0, CircleCollider.radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
        {
            if (collision.gameObject.GetComponentInChildren<BaseMonster>() == target)
            {
                IsBattle = true;
            } 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsBattle = false;
    }
    #endregion

    protected override void InitializeEntity()
    {
        IsBattle = false;
        LastAttackTime = 0f;

        InitializeStatusData();

        animCntrllr.ResetTrigger(AnimParam_Idle);
        animCntrllr.ResetTrigger(AnimParam_Move);
        animCntrllr.ResetTrigger(AnimParam_Attack);
        animCntrllr.ResetTrigger(AnimParam_Die);

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
