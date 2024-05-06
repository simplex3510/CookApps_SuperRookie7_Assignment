using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM.Base;
using FSM.Base.State;

namespace Entity.Base
{
    public abstract class BaseEntity : MonoBehaviour, IAttackable, IFiniteStateMachinable
    {
        // MonoBehaviour
        public abstract void Start();

        // BaseEntity
        protected abstract void AssignAnimationParameters();
        protected abstract void InitializeStateDict();
        protected abstract void InitializeStatusData();

        // IFiniteStateMachinable
        public abstract void ChangeStateFSM(EState nextState);
        public abstract IEnumerator UpdateFSM();

        // IAttackable
        public abstract void AttackTarget();
        public abstract bool AttackedEntity(float Damage);
    }
}