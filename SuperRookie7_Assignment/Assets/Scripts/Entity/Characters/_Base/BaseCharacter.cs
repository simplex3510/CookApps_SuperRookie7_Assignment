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
        private Vector2 spawnPosition;

        public int AnimParam_AtkTime { get; private set; }
        public int AnimParam_Idle { get; private set; }
        public int AnimParam_Move { get; private set; }
        public int AnimParam_Battle { get; private set; }
        public int AnimParam_Skill { get; private set; }
        public int AnimParam_Attack { get; private set; }
        public int AnimParam_Die { get; private set; }
        public int AnimParam_Victory { get; private set; }
        
        protected FiniteStateMachine KnightFSM { get; set; }

        #region MonoBehavior
        protected override void Awake()
        {
            base.Awake();
            offset_RNG = 1f;
        }

        public override void Start()
        {
            transform.position = spawnPosition;
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

            healthBar.value = StatusData.Current_HP;

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