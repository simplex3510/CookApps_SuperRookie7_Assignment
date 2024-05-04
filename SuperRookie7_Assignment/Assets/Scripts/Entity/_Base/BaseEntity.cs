using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FSM.Base;
using FSM.Base.State;

namespace Entity.Base
{
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IFiniteStateMachinable
    {
        // BaseEntity
        protected abstract void AssignAnimationParameters();
        protected abstract void InitializeStateDict();
        protected abstract void InitializeStatusData();

        // IFiniteStateMachinable
        public abstract void DamagedEntity(float Damage);
        public abstract IEnumerator UpdateFSM();

        // IDamagable
        public abstract void AttackTarget();
        public abstract void ChangeStateFSM(EState nextState);
    }
}