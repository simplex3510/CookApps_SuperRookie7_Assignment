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


        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Skill { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }
        public int AnimParam_Victory { get; private set; }

        [SerializeField]
        protected EState curState;
        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine KnightFSM { get; set; }

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
        [SerializeField]
        private float lastAttackTime = 0;
        public float LastAttackTime { get => lastAttackTime; set => lastAttackTime = value; }

        // BaseEntity
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
            curState = nextState;

            switch (curState)
            {
                case EState.Idle:
                    KnightFSM.ChangeState(StateDict[EState.Idle]);
                    break;
                case EState.Move:
                    KnightFSM.ChangeState(StateDict[EState.Move]);
                    break;
                case EState.Battle:
                    KnightFSM.ChangeState(StateDict[EState.Battle]);
                    break;
                case EState.Skill:
                    KnightFSM.ChangeState(StateDict[EState.Skill]);
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