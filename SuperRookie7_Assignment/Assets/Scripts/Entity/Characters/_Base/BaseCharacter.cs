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
        protected CircleCollider2D circleCollider;
        public CircleCollider2D CircleCollider { get => circleCollider; }

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
        public EState ECurState 
        {
            get
            {
                return eCurState;
            }
            set
            {
                // Debug.Log($"Knight - Before State: {eCurState} | After State: {value}");
                eCurState = value;
            } 
        }
        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine KnightFSM { get; set; }
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
            AnimParam_Skill = Animator.StringToHash("skill");
            AnimParam_Die = Animator.StringToHash("die");
            AnimParam_Victory = Animator.StringToHash("victory");

            AnimParam_AtkTime = Animator.StringToHash("atk_time");
        }

        // IFiniteStateMachinable
        public override void ChangeStateFSM(EState nextState)
        {
            ECurState = nextState;

            switch (ECurState)
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
    }
}