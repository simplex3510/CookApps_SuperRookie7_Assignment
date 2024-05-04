using FSM.Base.State;
using FSM.Base;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Entity.Base
{
    public abstract class BaseMonster : BaseEntity
    {
        [SerializeField]
        protected Animator animCntrllr;
        public Animator AnimCntrllr { get { return animCntrllr; } }
        [SerializeField]
        protected float disappearDuration;
        public float DisappearDuration { get { return disappearDuration; } }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }

        [SerializeField]
        protected EState curState;
        protected Dictionary<EState, IStatable> StateDict { get; set; }
        protected FiniteStateMachine FSM { get; set; }

        [SerializeField]
        protected BaseEntity target;
        [SerializeField]
        protected LayerMask targetLayerMask;

        [SerializeField]
        protected BaseStatus statusData;

        // BaseEntity
        protected override void AssignAnimationParameters()
        {
            AnimParam_Idle = Animator.StringToHash("idle");
            AnimParam_Move = Animator.StringToHash("move");
            AnimParam_Attack = Animator.StringToHash("canAttack");
            AnimParam_Die = Animator.StringToHash("die");
        }

        // IFiniteStateMachinable
        public override void ChangeStateFSM(EState nextState)
        {
            curState = nextState;

            switch (curState)
            {
                case EState.Idle:
                    FSM.ChangeState(StateDict[EState.Idle]);
                    break;
                case EState.Move:
                    FSM.ChangeState(StateDict[EState.Move]);
                    break;
                case EState.Attack:
                    FSM.ChangeState(StateDict[EState.Attack]);
                    break;
                case EState.Die:
                    FSM.ChangeState(StateDict[EState.Die]);
                    break;
                default:
                    Debug.LogError("ChangeStateFSM Error");
                    break;
            }
        }

        // IDamagable
    }
}
