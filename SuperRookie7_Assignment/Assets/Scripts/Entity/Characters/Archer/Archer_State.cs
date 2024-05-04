using Entity.Base;
using FSM.Base.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Archer : BaseCharacter
{
    public override void AttackTarget()
    {

    }

    public override void DamagedEntity(float damage)
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
