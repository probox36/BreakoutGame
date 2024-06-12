using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject scoreController;
    public GameObject scorePanel;
    public GameObject greetingPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreCaption;
    private ScoreController controllerScript;

    public void showGameOverDialog(int score) {
        scoreCaption.text = "YOUR SCORE WAS " + score;
        scorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void startBtnClick() {
        greetingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        scorePanel.SetActive(true);
        controllerScript.setPaused(false);
    }

    public void quitBtnClick() {
        Application.Quit();
        Debug.Log("App closed");
    }

    void Start() {
        controllerScript = scoreController.GetComponent<ScoreController>();
        scorePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update() { }
}
