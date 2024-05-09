using UnityEngine;
using System.Collections;
using Entity.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class GoblinBoss
{
    #region IAttackable Method
    public new void AttackTarget()
    {
        Vector2 point = new Vector2(0f, -0.33f);
        Collider2D[] targets = Physics2D.OverlapCircleAll(point, 0.77f);

        foreach (var target in targets)
        {
            target.GetComponent<BaseEntity>().AttackedEntity(StatusData.so_StatusData.STR);
        }
    }

    public override bool AttackedEntity(float damage)
    {
        return base.AttackedEntity(damage);
    }
    #endregion

    #region Move Method
    public override void CheckNearestCharacter()
    {
        base.CheckNearestCharacter();
    }

    public override void MoveToTarget()
    {
        base.MoveToTarget();
    }
    #endregion

    #region FSM Method
    public override void ChangeStateFSM(EState nextState)
    {
        base.ChangeStateFSM(nextState);
    }

    public override IEnumerator UpdateFSM()
    {
        while (true)
        {
            switch (ECurState)
            {
                case EState.None:
                    Debug.LogWarning("None State");
                    break;

                case EState.Idle:
                    TransitionFromIdle();
                    break;

                case EState.Move:
                    TransitionFromMove();
                    break;

                case EState.Battle:
                    TransitionFromBattle();
                    break;

                case EState.Die:
                    ChangeStateFSM(EState.Die);
                    break;

                default:
                    Debug.LogError("UpdateFSM Error");
                    break;
            }

            GoblinFSM.UpdateState();

            yield return null;
        }
    }
    #endregion

    #region State Transition Method
    private void TransitionFromIdle()
    {
        if (0 < EntityManager.Instance.spawnedCharactersDict.Count)
        {
            ChangeStateFSM(EState.Move);
            return;
        }
    }

    private void TransitionFromMove()
    {
        if (target == null)
        {
            ChangeStateFSM(EState.Idle);
            return;
        }
        else if (IsBattle == true)
        {
            ChangeStateFSM(EState.Battle);
            return;
        }
    }

    private void TransitionFromBattle()
    {
        if (target == null)
        {
            IsBattle = false;
            ChangeStateFSM(EState.Idle);
            return;
        }
    }
    #endregion

    [SerializeField]
    private float gizmoOffestX;
    [SerializeField]
    private float gizmoOffestY;
    [SerializeField]
    private float gizmoOffsetRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + gizmoOffestX
                                          , transform.position.y + gizmoOffestY)
                              , StatusData.so_StatusData.ATK_RNG * gizmoOffsetRadius);
    }
}