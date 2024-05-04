using FSM.Base.State;
using FSM.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Base
{
    public abstract class BaseCharacter : BaseEntity
    {
        [SerializeField]
        protected Animator animCntrllr;
        protected int AnimParam_Idle { get; private set; }
        protected int AnimParam_Move { get; private set; }
        protected int AnimParam_Attack { get; private set; }
        protected int AnimParam_Skill { get; private set; }

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
            AnimParam_Skill = Animator.StringToHash("skill");
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
                case EState.Skill:
                    FSM.ChangeState(StateDict[EState.Skill]);
                    break;
                default:
                    Debug.LogError("ChangeStateFSM Error");
                    break;
            }
        }
        // IDamagable
    }
}