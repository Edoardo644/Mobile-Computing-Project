using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D body;
    
    private void Start()
    {
        body.velocity = transform.right * speed;
    }
}
