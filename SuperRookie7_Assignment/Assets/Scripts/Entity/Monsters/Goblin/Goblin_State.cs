using Entity.Base;
using FSM.Base.State;
using System.Collections;
using UnityEngine;
using Singleton.Manager;
using UnityEditorInternal;

public partial class Goblin
{
    #region Damage and Attack Method
    public override void AttackTarget()
    {

    }

    public override void DamagedEntity(float damage)
    {

    }
    #endregion

    #region Move Method
    public void CheckNearestEnemy()
    {
        float leastDistance = float.MaxValue;
        Vector2 charPos = new Vector2(transform.position.x, transform.position.y);

        foreach (var enemy in EntityManager.Instance.spawnedCharactersDict.Values)
        {
            Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            float distance = Vector2.Distance(charPos, enemyPos);
            if (distance < leastDistance)
            {
                leastDistance = distance;
                target = enemy;
            }
        }

        return;
    }

    public void MoveToTarget()
    {
        if (target == null)
        {
            Debug.LogError("Can not Move to Target which is null");
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, statusData.so_StatusData.spd * Time.deltaTime);
    }
    #endregion

    public override void ChangeStateFSM(EState nextState)
    {
        base.ChangeStateFSM(nextState);
    }

    public override IEnumerator UpdateFSM()
    {
        Collider2D detectedTarget = null;

        while (true)
        {
            if ((statusData as Goblin_Status).CurrentHP <= 0)
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

                case EState.Attack:
                    TransitionFromAttack(detectedTarget);
                    break;

                case EState.Die:
                    TransitionFromDie();
                    break;

                default:
                    Debug.LogError("UpdateFSM Error");
                    break;
            }

            FSM.UpdateState();
            yield return null;
        }
    }

    #region Transition Method
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

        detectedTarget = Physics2D.OverlapCircle(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), statusData.so_StatusData.atk_rng * gizmoOffsetRadius, targetLayerMask);
        if (detectedTarget != null && detectedTarget.GetComponent<BaseEntity>() == target)
        {
            ChangeStateFSM(EState.Attack);
            return;
        }
    }

    private void TransitionFromAttack(Collider2D detectedTarget)
    {
        if (EntityManager.Instance.spawnedCharactersDict.Count == 0)
        {
            ChangeStateFSM(EState.Idle);
            return;
        }

        detectedTarget = Physics2D.OverlapCircle(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), statusData.so_StatusData.atk_rng * gizmoOffsetRadius, targetLayerMask);
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
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), statusData.so_StatusData.atk_rng * gizmoOffsetRadius);

    }
}