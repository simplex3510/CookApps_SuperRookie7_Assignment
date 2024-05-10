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

        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }

        protected FiniteStateMachine GoblinFSM { get; set; }

        #region MonoBehavior
        protected override void Awake()
        {
            base.Awake();
            offset_RNG = 0.77f;
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

            healthBar.value = StatusData.Current_HP;

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
