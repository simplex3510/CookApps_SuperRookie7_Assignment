using FSM.Base.State;
using FSM.Base;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Entity.Base
{
    public abstract class BaseMonster : BaseEntity
    {
        [SerializeField]
        protected Transform rootTransfrom;
        public Transform Root { get => rootTransfrom; }

        [SerializeField]
        protected Animator animCntrllr;
        public Animator AnimCntrllr { get => animCntrllr; }

        [SerializeField]
        protected CircleCollider2D circleCollider;
        public CircleCollider2D CircleCollider { get => circleCollider; }

        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }

        [SerializeField]
        private EState eCurState = EState.None;
        public EState ECurState { get => eCurState; }
        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine GoblinFSM { get; set; }
        protected bool IsBattle { get; set; }
        public float LastAttackTime { get; set; }

        [SerializeField]
        protected BaseEntity target;
        [SerializeField]
        protected LayerMask targetLayerMask;

        [SerializeField]
        private BaseStatus statusData;
        public BaseStatus StatusData { get => statusData; }
        [SerializeField]
        private float disappearDuration;
        public float DisappearDuration { get => disappearDuration; }

        #region MonoBehavior
        protected virtual void FixedUpdate()
        {
            circleCollider.radius = statusData.so_StatusData.ATK_RNG * 0.5f;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collision.gameObject.GetComponent<BaseCharacter>() == target)
                {
                    IsBattle = true;
                }
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsBattle)
            {
                if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
                {
                    if (collision.gameObject.GetComponent<BaseCharacter>() == target)
                    {
                        IsBattle = true;
                    }
                }
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D collision)
        {
            if (((1 << collision.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collision.gameObject.GetComponent<BaseMonster>() == target)
                {
                    IsBattle = false;
                }
            }
        }
        #endregion

        // BaseEntity
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
            AnimParam_Die = Animator.StringToHash("die");

            AnimParam_AtkTime = Animator.StringToHash("atk_time");
        }

        #region IAttackable Method
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

        // IFiniteStateMachinable
        public override void ChangeStateFSM(EState nextState)
        {
            eCurState = nextState;

            switch (ECurState)
            {
                case EState.Idle:
                    GoblinFSM.ChangeState(StateDict[EState.Idle]);
                    break;
                case EState.Move:
                    GoblinFSM.ChangeState(StateDict[EState.Move]);
                    break;
                case EState.Battle:
                    GoblinFSM.ChangeState(StateDict[EState.Battle]);
                    break;
                case EState.Die:
                    GoblinFSM.ChangeState(StateDict[EState.Die]);
                    break;
                default:
                    Debug.LogError("ChangeStateFSM Error");
                    break;
            }
        }
    }
}
