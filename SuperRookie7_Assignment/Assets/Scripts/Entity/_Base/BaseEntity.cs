using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM.Base;
using FSM.Base.State;

namespace Entity.Base
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IFiniteStateMachinable
    {
        [SerializeField]
        protected Animator AnimCntrllr { get; set; }
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

        protected virtual void AssignAnimationParameters()
        {
            AnimParam_Idle = Animator.StringToHash("idle");
            AnimParam_Move = Animator.StringToHash("move");
            AnimParam_Attack = Animator.StringToHash("canAttack");
            AnimParam_Skill = Animator.StringToHash("skill");
        }

        protected abstract void InitializeStateDict();

        protected abstract void InitializeStatusData();

        // IFiniteStateMachinable
        public abstract IEnumerator UpdateFSM();
        public virtual void ChangeStateFSM(EState nextState)
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
        public abstract void AttackTarget();
        public abstract void DamagedEntity(float damage);
    }
}