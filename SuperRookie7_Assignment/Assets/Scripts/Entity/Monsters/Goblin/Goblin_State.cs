using Entity.Base;
using FSM.Base.State;
using System.Collections;
using UnityEngine;
using Singleton.Manager;
using UnityEditorInternal;

public partial class Goblin
{
    #region IAttackable Method
    public override void AttackTarget()
    {
        Debug.Log("Attack target: " + target.name);
        target.AttackedEntity(StatusData.so_StatusData.STR);
    }

    public override void AttackedEntity(float damage)
    {
        (StatusData as Goblin_Status).CurrentHP -= damage;
    }
    #endregion

    #region (Re)Spawn Method
    public void StartRespawnCoroutine()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        float oldTime = Time.time;

        while (true)
        {
            if (StatusData.so_StatusData.RespawnTime < Time.time - oldTime)
            {
                break;
            }

            yield return null;
        }

        InitializeEntity();
        
        Debug.Log("Respawn Ready");
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }
    #endregion

    #region Move Method
    public void CheckNearestCharacter()
    {
        float leastDistance = float.MaxValue;
        Vector2 monsPos = new Vector2(Root.transform.position.x, Root.transform.position.y);

        foreach (var character in EntityManager.Instance.spawnedCharactersDict.Values)
        {
            Vector2 charPos = new Vector2(character.transform.position.x, character.transform.position.y);
            float distance = Vector2.Distance(monsPos, charPos);
            if (distance < leastDistance)
            {
                leastDistance = distance;
                target = character;
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
        Collider2D detectedTarget = null;

        while (true)
        {
            if ((StatusData as Goblin_Status).CurrentHP <= 0)
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

            GoblinFSM.UpdateState();
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