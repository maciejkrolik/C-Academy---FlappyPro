using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public GameObject gameOverText, shopButton, homeButton;
    public Text scoreText, highScoreText;
    public AudioSource audioDie, audioPoint, audioPowerUp;

    public bool gameOver = false, isMachineOn = false;
    public float scrollSpeed = -1.5f;

    private int score = 0, money;
    private bool Died = false;
    private GameObject powerUp;

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

    public void BirdScored()
    {
        if (gameOver)
        {
            return;
        }
        audioPoint.Play();
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        if (Died == false)
        {
            // Setting High Score
            if (PlayerPrefs.GetInt("HighScore", 0) < score)
                PlayerPrefs.SetInt("HighScore", score);

            // Adding money
            money = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", money += score);

            audioDie.Play();
            gameOverText.SetActive(true);
            shopButton.SetActive(true);
            homeButton.SetActive(true);
            highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
            highScoreText.gameObject.SetActive(true);
            gameOver = true;
            Died = true;
        }
    }

    public void BirdPowerUp()
    {
        isMachineOn = true;
        powerUp = GameObject.Find("PowerUp(Clone)");
        powerUp.GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Bird").GetComponent<SpriteRenderer>().enabled = true;
        audioPowerUp.Play();
        StartCoroutine(MachineTimer());
    }

    public void LoadScene_Shop(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        gameOverText.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        shopButton.SetActive(false);
        homeButton.SetActive(false);
    }

    public void UnloadScene_Shop()
    {
        gameOverText.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        shopButton.SetActive(true);
        homeButton.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    IEnumerator MachineTimer()
    {
        powerUp = GameObject.Find("PowerUp(Clone)");
        yield return new WaitForSeconds(10);
        GameObject.Find("Bird").GetComponent<SpriteRenderer>().enabled = false;
        isMachineOn = false;
        powerUp.GetComponent<SpriteRenderer>().enabled = true;
    }
}
