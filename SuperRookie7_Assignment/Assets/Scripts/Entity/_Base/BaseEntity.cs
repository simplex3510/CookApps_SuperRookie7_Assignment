using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM.Base;
using FSM.Base.State;
using UnityEngine.UI;

namespace Entity.Base
{
    public abstract class BaseEntity : MonoBehaviour, IAttackable, IFiniteStateMachinable
    {
        [SerializeField]
        protected Slider healthBar;
        public Slider HealthBar { get => healthBar; }

        [SerializeField]
        protected Animator animCntrllr;
        public Animator AnimCntrllr { get => animCntrllr; }

        [SerializeField]
        protected EState eCurState = EState.None;
        public EState ECurState { get => eCurState; }
        protected Dictionary<EState, IStatable> StateDict { get; set; }

        [SerializeField]
        protected BaseStatus statusData;
        public BaseStatus StatusData { get => statusData; }
        [SerializeField]
        protected float offset_RNG;

        [SerializeField]
        protected CircleCollider2D attackableCollider;
        public CircleCollider2D AttackableCollider { get => attackableCollider; }

        [SerializeField]
        protected CapsuleCollider2D damagableCollider;
        public CapsuleCollider2D DamagableCollider { get => damagableCollider; }

        [SerializeField]
        protected LayerMask targetLayerMask;
        [SerializeField]
        protected BaseEntity target;
        public BaseEntity Target { get => target; }

        protected bool IsBattle { get; set; }
        public float LastAttackTime { get; set; }

        #region MonoBehaviour
        protected virtual void Awake()
        {
            healthBar.value = healthBar.maxValue = StatusData.so_StatusData.Max_HP;
            healthBar.minValue = 0f;
        }

        public abstract void Start();

        protected virtual void FixedUpdate()
        {
            AttackableCollider.radius = statusData.so_StatusData.ATK_RNG * offset_RNG;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.isTrigger == true)
            {
                return;
            }


            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collider.gameObject.GetComponentInChildren<BaseEntity>() == target)
                {
                    IsBattle = true;
                }
            }
        }

        protected virtual void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.isTrigger == true)
            {
                return;
            }

            if (IsBattle == false)
            {
                if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
                {
                    if (collider.gameObject.GetComponentInChildren<BaseEntity>() == target)
                    {
                        IsBattle = true;
                    }
                }
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.isTrigger == true)
            {
                return;
            }

            if (((1 << collider.gameObject.layer) & targetLayerMask.value) != 0)
            {
                if (collider.gameObject.GetComponentInChildren<BaseEntity>() == target)
                {
                    target = null;
                    IsBattle = false;
                }
            }
        }
        #endregion

        // BaseEntity
        protected abstract void InitializeEntity();
        protected abstract void InitializeStateDict();
        protected abstract void InitializeStatusData();
        protected abstract void AssignAnimationParameters();

        // IFiniteStateMachinable
        public abstract void ChangeStateFSM(EState nextState);
        public abstract IEnumerator UpdateFSM();

        // IAttackable
        public abstract void AttackTarget();
        public abstract bool AttackedEntity(float Damage);

        // For Debug
        [SerializeField]
        private float gizmoOffestX;
        [SerializeField]
        private float gizmoOffestY;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x + gizmoOffestX
                                              , transform.position.y + gizmoOffestY)
                                  , StatusData.so_StatusData.ATK_RNG * offset_RNG);
        }
    }
}