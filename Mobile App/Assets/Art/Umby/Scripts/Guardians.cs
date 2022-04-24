using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardians : MonoBehaviour
{
    [SerializeField] private Transform redG;
    [SerializeField] private Transform purpleG;
    [SerializeField] private Transform blueG;

    public void ClickRed()
    {
        redG.localScale = new Vector3(1.3f, 1.3f, 1f);
    }

    public void UnclickRed()
    {
        redG.localScale = Vector3.one;
    }

    public void ClickPurple()
    {
        purpleG.localScale = new Vector3(1.3f, 1.3f, 1f);
    }

    public void UnclickPurple()
    {
        purpleG.localScale = Vector3.one;
    }

    public void ClickBlue()
    {
        blueG.localScale = new Vector3(1.3f, 1.3f, 1f);
    }

    public void UnclickBlue()
    {
        blueG.localScale = Vector3.one;
    }
}
