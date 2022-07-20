using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EdoHealthBar : MonoBehaviour
{
    [SerializeField] private EdoHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

}
