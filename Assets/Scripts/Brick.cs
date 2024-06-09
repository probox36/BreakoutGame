using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public enum BrickType { 
        Regular, Bonus, 
        Hard
    }

    public BrickType type;
    public BrickController controller;
    private int hitsLeft = 1;
    public bool breakable;

    void Start()
    {
        if (type.Equals(BrickType.Hard)) { hitsLeft = 3; }
        breakable = true;
    }

    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {           
        var ball = collision.gameObject;

        if (ball.CompareTag("Ball") && breakable) {
            hitsLeft--;
            if (hitsLeft < 1) {
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy() {
        if (controller != null) {
            controller.notifyWhenBrickDestroyed();
        }
    }
}
