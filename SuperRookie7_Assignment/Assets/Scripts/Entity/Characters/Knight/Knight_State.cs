using UnityEngine;
using System.Collections;
using FSM.Base.State;
using Singleton.Manager;

public partial class Knight
{
    #region IAttackable Method
    public override void AttackTarget()
    {
        if (target != null && target.AttackedEntity(StatusData.so_StatusData.STR) == true)
        {
            target = null;
        }
    }

    public override bool AttackedEntity(float damage)
    {
        StatusData.CurrentHP -= damage;

        if (StatusData.CurrentHP <= 0)
        {
            ECurState = EState.Die;
            return true;
        }

        return false;
    }
    #endregion

    #region Move Method
    public void CheckNearestMonster()
    {
        float leastDistance = float.MaxValue;

        if (EntityManager.Instance.spawnedMonstersDict.Count == 0)
        {
            target = null;
        }
        else
        {
            foreach (var monster in EntityManager.Instance.spawnedMonstersDict.Values)
            {
                float distance = Vector2.Distance(transform.position, monster.transform.position);
                if (distance < leastDistance)
                {
                    leastDistance = distance;
                    target = monster;
                }
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

        if (target.transform.parent.gameObject.activeSelf == false)
        {
            CheckNearestMonster();
            return;
        }

        Vector2 direction = (target.transform.parent.position - transform.position).normalized;
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.parent.position, StatusData.so_StatusData.SPD * Time.deltaTime);
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

            KnightFSM.UpdateState();

            yield return null;
        }
    }
    #endregion

    #region State Transition Method
    private void TransitionFromIdle()
    {
        if (0 < EntityManager.Instance.spawnedMonstersDict.Count)
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
        if (target == null || IsBattle == false)
        {
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
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + gizmoOffestX, transform.position.y + gizmoOffestY), StatusData.so_StatusData.ATK_RNG * gizmoOffsetRadius);
    }
}
