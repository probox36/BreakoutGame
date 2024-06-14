using System;
using System.Collections.Generic;
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
    private List<BonusController> bonuses = new List<BonusController>();

    public void addBonus(BonusController bonus) {
        bonuses.Add(bonus);
    }

    public void removeBonus(BonusController bonus) {
        bonuses.Remove(bonus);
    }

    public void setPaused(bool paused) {
        this.paused = paused;
        paddle.setPaused(paused);
        ball.setPaused(paused);
        foreach (BonusController bonus in bonuses) {
            bonus.setPaused(paused);
        }
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
        refresh();
    }

    public void resetScene() {
        paddle.resetToHomePosition();
        ball.state = BallState.Attached;
        ball.Update();
        setPaused(true);
        ball.setSpeed(baseSpeed);
        uiController.showGameOverDialog(score);
        brickController.clear();
        brickController.Awake();
        Start();
    }

    public void caughtBonus() {
        if (lives < 5) {
            lives += 1;
        } else {
            score += 5;
        }
        refresh();
    }

    private void refresh() {
        livesText.text = new string('■', lives);
        scoreText.text = score.ToString();
        speedText.text = Math.Round((decimal)speed, 1).ToString() + " mph";
    }

    public void ballLost() {

        lives--;
        if (lives < 0) {
            resetScene();
        } else {
            refresh();
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
