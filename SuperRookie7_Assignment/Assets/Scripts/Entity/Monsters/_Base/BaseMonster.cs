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
        protected Animator animCntrllr;
        public Animator AnimCntrllr { get => animCntrllr; }
        [SerializeField]
        protected float disappearDuration;
        public float DisappearDuration { get => disappearDuration; }

        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }

        [SerializeField]
        protected EState curState;
        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine GoblinFSM { get; set; }

        [SerializeField]
        protected BaseEntity target;
        [SerializeField]
        protected LayerMask targetLayerMask;

        [SerializeField]
        private BaseStatus statusData;
        public BaseStatus StatusData { get => statusData; }

        // BaseEntity
        protected override void AssignAnimationParameters()
        {
            AnimParam_AtkTime = Animator.StringToHash("atk_time");

            AnimParam_Idle = Animator.StringToHash("idle");
            AnimParam_Move = Animator.StringToHash("move");
            AnimParam_Battle = Animator.StringToHash("isBattle");
            AnimParam_Attack = Animator.StringToHash("attack");
            AnimParam_Die = Animator.StringToHash("die");
        }

        // IFiniteStateMachinable
        public override void ChangeStateFSM(EState nextState)
        {
            curState = nextState;

            switch (curState)
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

        // IDamagable
    }
}
