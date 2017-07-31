using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public static MenuControl instance;
    public GameObject playButton, settingsButton;

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

    public void LoadScene_Settings(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        playButton.SetActive(false);
        settingsButton.SetActive(false);
    }

    public void UnloadScene_Settings()
    {
        playButton.SetActive(true);
        settingsButton.SetActive(true);
    }
}
