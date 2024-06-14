using UnityEngine;

public class BonusController : MonoBehaviour
{
    
    private ScoreController controller;
    private Rigidbody2D rigidBody;
    private bool paused = false;
    private Vector2 savedVelocity = Vector2.zero;

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
    
    void Start() {
        controller = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        controller.addBonus(this);
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        if (transform.position.y < -5.225) {
            controller.removeBonus(this);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {           
        var other = collision.gameObject;

        if (other.CompareTag("Paddle")) {
            controller.caughtBonus();
            controller.removeBonus(this);
            Destroy(gameObject);
        }
    }

}
