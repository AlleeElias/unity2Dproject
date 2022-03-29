using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private float AttackCooldown = 2.0f;
    private float cooldownTimer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrow;

    private void attack() {
        cooldownTimer = 0;

        arrow.transform.position = firePoint.position;
        arrow.GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= AttackCooldown) {
            attack();
        }
    }
}
