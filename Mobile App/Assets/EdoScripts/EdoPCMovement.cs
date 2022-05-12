using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoPCMovement : MonoBehaviour
{

    public Controller2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;

    // Update is called once per frame
    void Update()
    {

       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    }

    
    void FixedUpdate()
    {

        //move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);

    }
}
