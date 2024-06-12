using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    private int score = 0;
    private int baseLives = 5;
    private int lives;
    private float baseSpeed = 10;
    private float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI pauseText;
    public Ball ball;
    public UIController uiController;
    public PaddleController paddle;
    public BrickController brickController;
    private bool paused = true;

    public void setPaused(bool paused) {
        this.paused = paused;
        paddle.setPaused(paused);
        ball.setPaused(paused);
        pauseText.text = paused ? "▐▐" : "";
    }

    public void brickDestroyed(Brick brick) {

        var scoreToAdd = 0;
        var speedChange = 0f;
        switch(brick.type) {
            case BrickType.Regular:
            case BrickType.Bonus: { scoreToAdd = 1; speedChange = 0.2f; break; }
            case BrickType.Hard: { scoreToAdd = 3; speedChange = -2f; break; }
        }

        speed = ball.speed + speedChange;
        ball.setSpeed(speed);
        score += scoreToAdd;
        scoreText.text = score.ToString();
        speedText.text = Math.Round((decimal)speed, 1).ToString() + " mph";
    }

    public void resetScene() {
        paddle.resetToHomePosition();
        ball.state = BallState.Attached;
        ball.Update();
        setPaused(true);
        uiController.showGameOverDialog(score);
        brickController.Awake();
        Start();
    }

    public void ballLost() {

        lives--;
        if (lives < 0) {
            resetScene();
        } else {
            livesText.text = new string('■', lives);
        }
    }

    void Start(){
        score = 0;
        lives = baseLives;
        speed = baseSpeed;
        scoreText.text = "0";
        livesText.text = "■■■■■";
        speedText.text = "0 mph";
        pauseText.text = "";
    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.Mouse1) && ball.state.Equals(BallState.Released)) {
            setPaused(!paused);
        }
    }
}
