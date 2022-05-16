using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject deadMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private health player;

    private void YouDead()
    {
        StartCoroutine(DeadWait());
    }

    IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(1);

        deadMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void YouWin()
    {
        StartCoroutine(WinWait());
    }

    IEnumerator WinWait()
    {
        yield return new WaitForSeconds(1);

        Time.timeScale = 0f;
        winMenu.SetActive(true);
    }
}
