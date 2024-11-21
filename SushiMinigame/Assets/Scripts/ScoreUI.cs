using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText; // Assign the UI Text component in the Inspector
    public static int score = 0;
    private static int winningScore = 5;
    public GameObject completeLevelUI;

    void Update()
    {
        score = RegisterSushiOnGoalPlate.score;
        scoreText.text = "Score: " + score; // Update the score text
        if (score == winningScore)
        {
            Debug.Log("You Won!!");
            completeLevelUI.SetActive(true);
            RegisterSushiOnGoalPlate.score = 0;
            Time.timeScale = 0;
        }
    }

    public void StartAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}