using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Archer archer;

    [SerializeField]
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        archer = GetComponentInParent<Archer>();
        transform.SetParent(null);
    }

    private void Update()
    {
        if (archer.Target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.Slerp(transform.position, (archer.Target.transform.position), (archer.StatusData as Archer_Status).Projectile_SPD * Time.deltaTime);

        Vector2 direction = (archer.Target.transform.position) - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<BaseMonster>() == archer.Target)
        {
            archer.Target.AttackedEntity(archer.StatusData.so_StatusData.STR);
            Destroy(gameObject);
        }
    }
}
