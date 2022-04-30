using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPicker : MonoBehaviour
{
    [SerializeField] private OldMoving player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Arrow")
        {
            Destroy(gameObject);
            TakeGem();
        }
    }

    public void TakeGem()
    {
        player.gems = Mathf.Clamp(player.gems + 1, 0, 3);
    }
}
