using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{   

    public enum BallState {
        Attached, Released, Lost
    }

    public BallState state = BallState.Attached;
    public ScoreController scoreController;
    public float speed = 10;
    private GameObject paddle;
    private Rigidbody2D rigidBody;

    void Start() {
        paddle = GameObject.Find("Paddle");
        rigidBody = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
        var velocity = rigidBody.velocity;
        velocity.Normalize();
        velocity *= this.speed;
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
                var yVelocity = 1f;
                var xVelocity = Random.Range(0.4f, 1.4f);
                var r = Random.Range(0f, 1f);
                Debug.Log(r);
                Debug.Log(r > 0.5);
                xVelocity *= r > 0.5 ? -1f : 1f;
                Debug.Log(xVelocity); 
                rigidBody.velocity = new Vector2(xVelocity, yVelocity);
                setSpeed(speed);
            }

        } else if (state.Equals(BallState.Released)) {
            
            if (transform.position.y < -5.3f) {
                scoreController.ballLost();
                state = BallState.Lost;
            }
        } else if (state.Equals(BallState.Lost)) {
            
            if (Input.GetKeyDown(KeyCode.Mouse1)) { 
                state = BallState.Attached; 
            }
        }
    }
}
