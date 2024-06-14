using UnityEngine;

public class Brick : MonoBehaviour
{

    public BrickType type;
    public BrickController brickController;
    public ScoreController scoreController;
    private int hitsLeft = 1;
    public bool breakable;
    public GameObject bonusPrefab;
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
                if (type.Equals(BrickType.Bonus)) {
                    GameObject bonus = Instantiate(bonusPrefab);
                    bonus.transform.SetParent(null);
                    bonus.transform.position = transform.position;
                    var bonusRigidBody = bonus.GetComponent<Rigidbody2D>();
                    bonusRigidBody.AddForceY(-300);
                }
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
