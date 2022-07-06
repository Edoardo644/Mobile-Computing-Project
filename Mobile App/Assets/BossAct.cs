using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAct : MonoBehaviour
{
    [SerializeField] private Boss moving;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossHB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.SetActive(true);
            bossHB.SetActive(true);
            Destroy(gameObject);
        }
    }
}
