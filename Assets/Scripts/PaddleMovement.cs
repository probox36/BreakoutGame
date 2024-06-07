using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{   

    private float speed = 0;
    private float weight = 1.65f;
    private float resistanceCoeff = 0.955f;

    void Start() { }

    void Update()
    {   
        var paddleX = transform.position.x;
        var mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var force = mouseX - paddleX;     

        speed = (speed + force / weight) * resistanceCoeff;
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
