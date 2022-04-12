using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    public int coins = 0;
    [SerializeField] private TextMeshProUGUI counter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            Pickup();
        }
    }

    public void Pickup()
    {
        coins++;
        counter.text = coins.ToString();
    }
}
