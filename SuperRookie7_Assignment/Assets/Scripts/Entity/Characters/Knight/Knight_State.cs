using Entity.Base;
using FSM.Base.State;
using Singleton.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Knight : BaseCharacter
{
    #region IAttackable Method
    public override void AttackTarget()
    {
        Debug.Log("Attack target: " + target.name);
        target.AttackedEntity(StatusData.so_StatusData.STR);
    }

    public override void AttackedEntity(float damage)
    {
        (StatusData as Knight_Status).CurrentHP -= damage;
    }
    #endregion

    #region Move Method
    public void CheckNearestMonster()
    {
        float leastDistance = float.MaxValue;
        Vector2 charPos = new Vector2(transform.position.x, transform.position.y);

        foreach (var monster in EntityManager.Instance.spawnedMonstersDict.Values)
        {
            Vector2 monsPos = new Vector2(monster.transform.position.x, monster.transform.position.y);
            float distance = Vector2.Distance(charPos, monsPos);
            if (distance < leastDistance)
            {
                leastDistance = distance;
                target = monster;
            }
        }
    }

    public void MoveToTarget()
    {
        if (target == null)
        {
            Debug.LogError("Can not Move to Target which is null");
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, StatusData.so_StatusData.SPD * Time.deltaTime);
    }
    #endregion

    #region FSM Method
    public override void ChangeStateFSM(EState nextState)
    {
        base.ChangeStateFSM(nextState);
    }

    public override IEnumerator UpdateFSM()
    {
        Collider2D detectedTarget = null;

        while (true)
        {
            if ((StatusData as Knight_Status).CurrentHP <= 0)
            {
                curState = EState.Die;
            }

            switch (curState)
            {
                case EState.Idle:
                    TransitionFromIdle(detectedTarget);
                    break;

                case EState.Move:
                    TransitionFromMove(detectedTarget);
                    break;

                case EState.Battle:
                    TransitionFromBattle(detectedTarget);
                    break;

                case EState.Die:
                    TransitionFromDie();
                    break;

                default:
                    Debug.LogError("UpdateFSM Error");
                    break;
            }

            KnightFSM.UpdateState();
            yield return null;
        }
    }
    #endregion

    #region State Transition Method
    private void TransitionFromIdle(Collider2D detectedTarget)
    {
        if (EntityManager.Instance.spawnedCharactersDict.Count != 0)
        {
            ChangeStateFSM(EState.Move);
            return;
        }
    }

    private void TransitionFromMove(Collider2D detectedTarget)
    {
        if (EntityManager.Instance.spawnedCharactersDict.Count == 0)
        {
            ChangeStateFSM(EState.Idle);
            return;
        }

        detectedTarget = Physics2D.OverlapCircle(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), StatusData.so_StatusData.ATK_RNG * gizmoOffsetRadius, targetLayerMask);
        if (detectedTarget != null && detectedTarget.GetComponent<BaseEntity>() == target)
        {
            ChangeStateFSM(EState.Battle);
            return;
        }
    }

    private void TransitionFromBattle(Collider2D detectedTarget)
    {
        if (EntityManager.Instance.spawnedCharactersDict.Count == 0)
        {
            ChangeStateFSM(EState.Idle);
            return;
        }

        detectedTarget = Physics2D.OverlapCircle(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), StatusData.so_StatusData.ATK_RNG * gizmoOffsetRadius, targetLayerMask);
        if (detectedTarget == null || target == null)
        {
            ChangeStateFSM(EState.Move);
            return;
        }
    }

    private void TransitionFromDie()
    {
        ChangeStateFSM(EState.Die);
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
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), StatusData.so_StatusData.ATK_RNG * gizmoOffsetRadius);
    }
}
