using UnityEngine;
using System.Collections;
using Entity.Base;
using FSM.Base.State;
using Singleton.Manager;

public partial class Goblin
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
    public void CheckNearestCharacter()
    {
        float leastDistance = float.MaxValue;

        if (EntityManager.Instance.spawnedCharactersDict.Count == 0)
        {
            target = null;
        }
        else
        {
            foreach (var character in EntityManager.Instance.spawnedCharactersDict.Values)
            {
                float distance = Vector2.Distance(Root.transform.position, character.transform.position);
                if (distance < leastDistance)
                {
                    leastDistance = distance;
                    target = character;
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

        if (target.gameObject.activeSelf == false)
        {
            CheckNearestCharacter();
            return;
        }

        Vector2 direction = (target.transform.position - Root.transform.position).normalized;
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        
        Root.transform.position = Vector2.MoveTowards(Root.transform.position, target.transform.position, StatusData.so_StatusData.SPD * Time.deltaTime);
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