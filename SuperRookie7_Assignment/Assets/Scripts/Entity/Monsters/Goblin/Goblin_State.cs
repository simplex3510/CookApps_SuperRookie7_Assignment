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
        base.AttackTarget();
    }

    public override bool AttackedEntity(float damage)
    {
        return base.AttackedEntity(damage);
    }
    #endregion

    #region Move Method
    public virtual void CheckNearestCharacter()
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

            Vector2 direction = (target.transform.position - Root.transform.position).normalized;
            if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    public virtual void MoveToTarget()
    {
        if (target == null)
        {
            Debug.LogError("Can not Move to Target which is null");
            return;
        }

        CheckNearestCharacter();

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
        if (target == null)
        {
            IsBattle = false;
            ChangeStateFSM(EState.Idle);
            return;
        }
    }
    #endregion
}