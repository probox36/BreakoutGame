using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    private int score = 0;
    private int lives = 5;
    private float speed = 10;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI speedText;
    public Ball ball;

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

    public void ballLost() {

        lives--;
        livesText.text = new string('■', lives);
        if (lives < 0) {
            Debug.Log("Вы лох");
        }
    }

    void Start(){}

    void Update(){}
}
