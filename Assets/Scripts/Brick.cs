using UnityEngine;

public class Brick : MonoBehaviour
{

    public BrickType type;
    public BrickController brickController;
    public ScoreController scoreController;
    private int hitsLeft = 1;
    public bool breakable;
    public BrickAnimator brickAnimator;

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
                brickAnimator.animateBrokenBrick(gameObject);
                Destroy(gameObject);
            } else {
                Animation animation = gameObject.GetComponent<Animation>();
                animation.Play();
            }
        }
    }

    void OnDestroy() {
        if (GameObject.Find("BrickController") != null) {
            brickController.notifyWhenBrickDestroyed();
        }
    }
}
