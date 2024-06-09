using UnityEngine;

public class Brick : MonoBehaviour
{

    public BrickType type;
    public BrickController brickController;
    public ScoreController scoreController;
    private int hitsLeft = 1;
    public bool breakable;

    void Start()
    {
        if (type.Equals(BrickType.Hard)) { hitsLeft = 3; }
        scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        breakable = true;
    }

    void Update() { }

    void OnCollisionEnter2D(Collision2D collision)
    {           
        var ball = collision.gameObject;

        if (ball.CompareTag("Ball") && breakable) {
            hitsLeft--;
            if (hitsLeft < 1) {
                scoreController.brickDestroyed(this);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy() {
        if (brickController != null) {
            brickController.notifyWhenBrickDestroyed();
        }
    }
}
