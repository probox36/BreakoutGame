using UnityEngine;

public class Ball : MonoBehaviour
{   

    public Canvas canvas;
    public BallState state = BallState.Attached;
    public ScoreController scoreController;
    public float speed = 10;
    private GameObject paddle;
    private Rigidbody2D rigidBody;
    private bool paused = true;
    private Vector2 savedVelocity = Vector2.zero;

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

    public void setPaused(bool paused) {

        if (this.paused != paused) {

            if (paused) {
                savedVelocity = rigidBody.velocity;
                rigidBody.velocity = Vector2.zero;
            } else {
                rigidBody.velocity = savedVelocity;
            }
            this.paused = paused;
        }
    }

    public void Update()
    {
        if (!paused) {
            if (state.Equals(BallState.Attached)) {
                
                var targetPosition = paddle.transform.position;
                targetPosition.y += 0.5f;
                transform.position = targetPosition;

                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    state = BallState.Released;
                    var yVelocity = 1f;
                    var xVelocity = Random.Range(0.4f, 1.4f);
                    var r = Random.Range(0f, 1f);
                    xVelocity *= r > 0.5 ? -1f : 1f;
                    rigidBody.velocity = new Vector2(xVelocity, yVelocity);
                    setSpeed(speed);
                }

            } else if (state.Equals(BallState.Released)) {
                
                if (transform.position.y < -5.3f) {
                    state = BallState.Lost;
                    scoreController.ballLost();
                }
            } else if (state.Equals(BallState.Lost)) {
                
                if (Input.GetKeyDown(KeyCode.Mouse1)) { 
                    state = BallState.Attached; 
                }
            }
        }
    }    
}
