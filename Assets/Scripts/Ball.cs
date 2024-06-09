using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{   

    public enum BallState {
        Attached, Released
    }

    public BallState state = BallState.Attached;
    private float speed = 10;
    private GameObject paddle;
    private Rigidbody2D rigidBody;

    void Start() {
        paddle = GameObject.Find("Paddle");
        rigidBody = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
    }

    void setSpeed(float speed) {
        var velocity = rigidBody.velocity;
        velocity.Normalize();
        velocity *= speed;
        rigidBody.velocity = velocity;
    }

    void Update()
    {
        if (state.Equals(BallState.Attached)) {
            
            var targetPosition = paddle.transform.position;
            targetPosition.y += 0.5f;
            transform.position = targetPosition;

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                state = BallState.Released;
                var yVelocity = Random.Range(0f, 1f);
                var tg35 = 1.428148f;
                var xVelocity = Random.Range(-yVelocity*tg35, yVelocity*tg35);
                rigidBody.velocity = new Vector2(xVelocity, yVelocity);
                setSpeed(speed);
            }

        } else if (state.Equals(BallState.Released)) {
            
            if (Input.GetKeyDown(KeyCode.S)) { setSpeed(20); }
            if (Input.GetKeyDown(KeyCode.Mouse1)) { state = BallState.Attached; }

        }

    }

}
