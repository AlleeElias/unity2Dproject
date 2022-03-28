using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCooldown;
    private Animator anim;
    private PlayerMovement movement;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 2;
        cooldownTimer = 800;
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && movement.canShoot()) {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack() {
        cooldownTimer = 0;
        anim.SetTrigger("shoot");
    }
}
