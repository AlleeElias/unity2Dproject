using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float attackCooldown;
   [SerializeField] private float range;
    [SerializeField] private float colDistance;
    private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private BoxCollider2D boxcol;
    [SerializeField] private LayerMask playerLayer;
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        attackCooldown = 5;
        damage = 2;
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Only attack if player is seen
        if (PlayerVisible())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attackMelee");
            }
        }

        if (enemyPatrol != null) { enemyPatrol.enabled = !PlayerVisible(); }
    }

    private bool PlayerVisible() {
        RaycastHit2D hit = Physics2D.BoxCast(boxcol.bounds.center +transform.right*range * transform.localScale.x*colDistance,
            new Vector3(boxcol.bounds.size.x *range,boxcol.bounds.size.y,boxcol.bounds.size.z)
            ,0,Vector2.left,0,playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcol.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(boxcol.bounds.size.x * range, boxcol.bounds.size.y, boxcol.bounds.size.z));
    }
}
