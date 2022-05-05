using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemBar : MonoBehaviour
{
    public OldMoving player;
    [SerializeField] private Image currentG;

    private void Update()
    {
        currentG.fillAmount = player.gems / 3;
        if (currentG.fillAmount > 0)
        {
            Debug.Log("presa!");
        }
    }
}
