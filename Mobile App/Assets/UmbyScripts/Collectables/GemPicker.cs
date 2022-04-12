using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPicker : MonoBehaviour
{
    public float currentGem { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
            TakeGem();
        }
    }

    public void TakeGem()
    {
        currentGem = Mathf.Clamp(currentGem + 1, 0, 3);
    }
}
