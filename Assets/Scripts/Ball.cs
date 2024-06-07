using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{   

    private enum BallState {
        Attached, Released
    }

    private BallState state = BallState.Attached;
    private GameObject paddle;
    private Rigidbody2D rigidBody;

    void Start() {
        paddle = GameObject.Find("Paddle");
        rigidBody = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
    }

    void Update()
    {
        if (state.Equals(BallState.Attached)) {
            
            var targetPosition = paddle.transform.position;
            targetPosition.y += 0.5f;
            transform.position = targetPosition;

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                state = BallState.Released;
                Vector2 velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
                velocity.Normalize();
                velocity *= Random.Range(5f, 20f);
                rigidBody.velocity = velocity;
            } 

        }
    }
}
