using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private OldMoving player;
    [SerializeField] private Health playerH;
    [SerializeField] private Rigidbody2D body;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap" || collision.gameObject.tag == "Platform")
        {
            player.jump = false;
        }

        if (collision.gameObject.tag == "Tilemap" && playerH.dead == true)
        {
            body.bodyType = RigidbodyType2D.Static;
        }
    }
}
