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
        gameOverText.SetActive(true);
        shopButton.SetActive(true);
        gameOver = true;
    }

    public void LoadScene_Shop(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        gameOverText.SetActive(false);
        shopButton.SetActive(false);
    }

    public void UnloadScene_Shop()
    {
        gameOverText.SetActive(true);
        shopButton.SetActive(true);
    }

    public void LoadScene_flappy(string name)
    {
        SceneManager.LoadScene(name);
    }
}
