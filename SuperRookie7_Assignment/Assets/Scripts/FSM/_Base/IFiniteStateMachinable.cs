using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.Base
{
    public interface IFiniteStateMachinable
    {
        public IEnumerator UpdateFSM();
        public void ChangeStateFSM(EState nextState);

    }
}