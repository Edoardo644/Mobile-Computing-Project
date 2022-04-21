using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHB : MonoBehaviour
{
    [SerializeField] private BossHealth boss;
    [SerializeField] private Image totalH;
    [SerializeField] private Image currentH;

    private void Start()
    {
        totalH.fillAmount = boss.currentHealth / 20;
    }

    private void Update()
    {
        currentH.fillAmount = boss.currentHealth / 20;
    }
}
