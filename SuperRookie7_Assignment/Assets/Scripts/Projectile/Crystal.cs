using Entity.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Priest priest;

    [SerializeField]
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        priest = GetComponentInParent<Priest>();
        transform.SetParent(null);
    }

    private void Update()
    {
        if (priest.Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (priest.Target.transform.position) - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90f);

        transform.position = Vector2.MoveTowards(transform.position, priest.Target.transform.position, (priest.StatusData as Priest_Status).Projectile_SPD * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<BaseMonster>() == priest.Target)
        {
            priest.Target.AttackedEntity(priest.StatusData.so_StatusData.STR);
            Destroy(gameObject);
        }
    }
}
