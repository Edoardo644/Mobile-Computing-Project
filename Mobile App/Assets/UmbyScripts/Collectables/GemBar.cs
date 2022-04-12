using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemBar : MonoBehaviour
{
    [SerializeField] private GemPicker player;
    [SerializeField] private Image currentG;

    private void Update()
    {
        currentG.fillAmount = player.currentGem / 3;
    }
}
