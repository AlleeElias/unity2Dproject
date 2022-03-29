using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image fillHealth;
    [SerializeField] private Image curHealthBar;

    private void Start()
    {
        fillHealth.fillAmount = playerHealth.getCurHealth() / 10;
    }
    private void Update()
    {
        curHealthBar.fillAmount = playerHealth.getCurHealth() / 10;
    }
}
