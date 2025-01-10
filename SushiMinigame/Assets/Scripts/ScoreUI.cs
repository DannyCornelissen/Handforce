using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText; // Assign the UI Text component in the Inspector
    public static int score = 0;
    [SerializeField] private int winningScore = 5;
    public GameObject completeLevelUI;

    void Update()
    {
   
        scoreText.text = "Score: " + score; // Update the score text
        if (score == winningScore)
        {
            Debug.Log("You Won!!");
            completeLevelUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void UpdateScore()
    {
        score++;
    }

    private void resetScore()
    {
        score = 0;
    }

    public void StartAgain()
    {
        resetScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}