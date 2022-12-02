using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    private Health playerHealth;
    [SerializeField] private Image fillHealth;
    [SerializeField] private Image curHealthBar;

    private void Awake()
    {
        playerHealth = GetComponentInParent<Health>();
    }

    private void Start()
    {
        fillHealth.fillAmount = playerHealth.getCurHealth() / 10;
    }
    private void Update()
    {
        curHealthBar.fillAmount = playerHealth.getCurHealth() / 10;
    }
}
