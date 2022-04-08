using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoving : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform wall;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        while (wall.localPosition.y < 13.50f)
        {
            if (wall.localPosition.x == player.localPosition.x + 2.5f)
            {
                wall.Translate(0, movementSpeed, 0);
            }
        }
    }
}
