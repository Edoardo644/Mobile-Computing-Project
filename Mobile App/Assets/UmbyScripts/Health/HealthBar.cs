using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalH;
    [SerializeField] private Image currentH;

    private void Start()
    {
        totalH.fillAmount = playerHealth.currentHealth / 3;
    }

    private void Update()
    {
        currentH.fillAmount = playerHealth.currentHealth /  3;
    }
}
