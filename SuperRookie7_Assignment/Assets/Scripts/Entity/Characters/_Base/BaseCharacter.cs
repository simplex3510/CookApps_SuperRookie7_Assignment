using FSM.Base.State;
using FSM.Base;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Entity.Base
{
    public abstract class BaseCharacter : BaseEntity
    {
        [SerializeField]
        protected Animator animCntrllr;
        public Animator AnimCntrllr { get => animCntrllr; }

        [SerializeField]
        protected CircleCollider2D attackableCollider;
        public CircleCollider2D AttackableCollider { get => attackableCollider; }

        [SerializeField]
        protected CapsuleCollider2D damagableCollider;
        public CapsuleCollider2D DamagableCollider { get => damagableCollider; }

        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Skill { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }
        public int AnimParam_Victory { get; private set; }

        [SerializeField]
        private EState eCurState = EState.None;
        public EState ECurState { get => eCurState; }

        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine KnightFSM { get; set; }
        protected bool IsBattle { get; set; }
        public float LastAttackTime { get; set; }

        [SerializeField]
        protected LayerMask targetLayerMask;
        [SerializeField]
        protected BaseEntity target;
        public BaseEntity Target { get => target; }

        [SerializeField]
        private BaseStatus statusData;
        public BaseStatus StatusData { get => statusData; }

        [SerializeField]
        private float disappearDuration;
        public float DisappearDuration { get => disappearDuration; }

        #region MonoBehavior
        protected virtual void FixedUpdate()
        {
            AttackableCollider.radius = statusData.so_StatusData.ATK_RNG;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collider.gameObject.GetComponentInChildren<BaseMonster>() == target)
                {
                    IsBattle = true;
                }
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D collider)
        {
            if (IsBattle == false)
            {
                if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
                {
                    if (collider.gameObject.GetComponentInChildren<BaseMonster>() == target)
                    {
                        IsBattle = true;
                    }
                }
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collider.gameObject.GetComponentInChildren<BaseMonster>() == target)
                {
                    target = null;
                    IsBattle = false;
                }
            }
        }
        #endregion

        #region BaseEntity
        protected override void InitializeStateDict()
        {
            StateDict[EState.None] = new BaseEntity_NoneState(null);
        }

        protected override void AssignAnimationParameters()
        {
            AnimParam_Idle = Animator.StringToHash("idle");
            AnimParam_Move = Animator.StringToHash("move");
            AnimParam_Battle = Animator.StringToHash("isBattle");
            AnimParam_Attack = Animator.StringToHash("attack");
            AnimParam_Skill = Animator.StringToHash("skill");
            AnimParam_Die = Animator.StringToHash("die");
            AnimParam_Victory = Animator.StringToHash("victory");

            AnimParam_AtkTime = Animator.StringToHash("atk_time");
        }
        #endregion

        #region IAttackable
        public override void AttackTarget()
        {
            if (target != null && target.AttackedEntity(StatusData.so_StatusData.STR) == true)
            {
                target = null;
            }
        }

        public override bool AttackedEntity(float damage)
        {
            StatusData.Current_HP -= damage;

            if (StatusData.Current_HP <= 0)
            {
                eCurState = EState.Die;
                return true;
            }

            return false;
        }
        #endregion

        #region IFiniteStateMachinable
        public override void ChangeStateFSM(EState eNextState)
        {
            eCurState = eNextState;

            switch (eCurState)
            {
                case EState.None:
                    Debug.LogWarning("None State");
                    break;
                case EState.Idle:
                    KnightFSM.ChangeState(StateDict[EState.Idle]);
                    break;
                case EState.Move:
                    KnightFSM.ChangeState(StateDict[EState.Move]);
                    break;
                case EState.Battle:
                    KnightFSM.ChangeState(StateDict[EState.Battle]);
                    break;
                case EState.Die:
                    KnightFSM.ChangeState(StateDict[EState.Die]);
                    break;
                case EState.Victory:
                    KnightFSM.ChangeState(StateDict[EState.Victory]);
                    break;
                default:
                    Debug.LogError("ChangeStateFSM Error");
                    break;
            }
        }
        #endregion
    }
}