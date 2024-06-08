using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public enum BrickType { 
        Regular, Bonus, Hard
    }

    public BrickType type;
    private int hitsLeft = 1;

    void Start()
    {
        if (type.Equals(BrickType.Hard)) { hitsLeft = 3; }   
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {           
        var ball = collision.gameObject;

        if (ball.CompareTag("Ball")) {
            hitsLeft--;
            if (hitsLeft < 1) {
                Destroy(gameObject);
            }
        }
    }
}
