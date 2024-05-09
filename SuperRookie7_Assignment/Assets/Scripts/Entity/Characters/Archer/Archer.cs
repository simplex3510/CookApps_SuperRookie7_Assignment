using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entity.Base;
using FSM.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Archer : BaseCharacter
{
    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private Transform arrowParent;

    #region Unity Life-Cycle
    protected override void Awake()
    {
        base.Awake();

        animCntrllr = GetComponent<Animator>();
        AssignAnimationParameters();

        attackableCollider = GetComponent<CircleCollider2D>();
        damagableCollider = GetComponent<CapsuleCollider2D>();

        StateDict = new Dictionary<EState, IStatable>();
        InitializeStateDict();

        KnightFSM = new FiniteStateMachine(StateDict[ECurState]);

        InitializeStatusData();

        EntityManager.Instance.spawnedCharactersDict.Add(GetHashCode(), this);

        StartCoroutine(UpdateFSM());
    }

    public override void Start()
    {
        InitializeEntity();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void OnTriggerStay2D(Collider2D collider)
    {
        base.OnTriggerStay2D(collider);
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);
    }
    #endregion

    protected override void InitializeEntity()
    {
        target = null;
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
        StateDict[EState.Idle] = new Archer_IdleState(this);
        StateDict[EState.Move] = new Archer_MoveState(this);
        StateDict[EState.Battle] = new Archer_BattleState(this);
        StateDict[EState.Die] = new Archer_DieState(this);
    }

    protected override void AssignAnimationParameters()
    {
        base.AssignAnimationParameters();
    }

    protected override void InitializeStatusData()
    {
        StatusData.Current_HP = StatusData.so_StatusData.Max_HP;
    }

    protected void Fire()
    {
        Instantiate(arrow, arrowParent);
    }
}
