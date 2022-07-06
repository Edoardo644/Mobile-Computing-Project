using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBossHB : MonoBehaviour
{
    [SerializeField] private DBossHealth boss;
    [SerializeField] private Image totalH;
    [SerializeField] private Image currentH;

    private void Start()
    {
        totalH.fillAmount = boss.currentHealth / boss.startingHealth;
    }

    private void Update()
    {
        currentH.fillAmount = boss.currentHealth / boss.startingHealth;
    }
}
