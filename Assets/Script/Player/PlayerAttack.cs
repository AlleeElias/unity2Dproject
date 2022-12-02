using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAttack : NetworkBehaviour
{
    private float attackCooldown;
    private Animator anim;
    private PlayerMovement movement;
    private float cooldownTimer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

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
        if (IsLocalPlayer)
        {
            if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.R)) && cooldownTimer > attackCooldown &&
                movement.canShoot())
            {
                Attack();
            }
            cooldownTimer += Time.deltaTime;
        }
    }

    //Plays the animation of throwing
    private void Attack()
    {
        cooldownTimer = 0;
        anim.SetTrigger("shoot");
    }

    //Wait for actual animation before the shot is fired
    private void setShoot()
    {
        //pooling fireballs
        fireBalls[0].transform.position = firePoint.position;
        fireBalls[0].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
}