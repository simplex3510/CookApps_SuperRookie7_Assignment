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

        circleCollider = GetComponent<CircleCollider2D>();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();

        GoblinFSM = new FiniteStateMachine(StateDict[ECurState]);

        InitializeStatusData();

        StartCoroutine(UpdateFSM());
    }

    private void OnEnable()
    {
        EntityManager.Instance.spawnedMonstersDict.Add(GetHashCode(), this);
    }

    public override void Start()
    {
        InitializeEntity();
    }

    private void FixedUpdate()
    {
        CircleCollider.radius = StatusData.so_StatusData.ATK_RNG / 2;
        CircleCollider.offset = new Vector2(0, -CircleCollider.radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
        {
            if (collision.gameObject.GetComponentInChildren<BaseCharacter>() == target)
            {
                IsBattle = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!IsBattle)
        {
            if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collision.gameObject.GetComponentInChildren<BaseCharacter>() == target)
                {
                    IsBattle = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsBattle = false;
    }

    private void OnDisable()
    {
        EntityManager.Instance.spawnedMonstersDict.Remove(GetHashCode());
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
        base.InitializeStateDict();
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
        StatusData.CurrentHP = StatusData.so_StatusData.MaxHP;
    }
}