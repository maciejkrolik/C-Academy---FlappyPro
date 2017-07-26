using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject shopButton;
    public GameObject homeButton;
    public Text highScoreText;
    public Text scoreText;

    public bool gameOver = false;
    public float scrollSpeed = -1.5f;

    private int score = 0;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void BirdScored()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
            PlayerPrefs.SetInt("HighScore", score);
        gameOverText.SetActive(true);
        shopButton.SetActive(true);
        homeButton.SetActive(true);
        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        highScoreText.gameObject.SetActive(true);
        gameOver = true;
    }

    public void LoadScene_Shop(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        gameOverText.SetActive(false);
        shopButton.SetActive(false);
        homeButton.SetActive(false);
    }

    public void UnloadScene_Shop()
    {
        gameOverText.SetActive(true);
        shopButton.SetActive(true);
        homeButton.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
