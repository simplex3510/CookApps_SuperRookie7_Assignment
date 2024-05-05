using Entity.Base;
using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Knight : BaseCharacter
{
    public override void AttackTarget()
    {

    }

    public override void AttackedEntity(float damage)
    {

    }

    public override void ChangeStateFSM(EState nextState)
    {
        base.ChangeStateFSM(nextState);
    }

    public override IEnumerator UpdateFSM()
    {
        yield return null;
    }
}
