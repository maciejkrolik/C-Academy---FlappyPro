using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public static MenuControl instance;
    public GameObject playButton, settingsButton;

    // Use this for initialization
    void Awake()
    {
        // Singleton pattern
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
        // Checking if the back button on Android is pressed and closing the app if so
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Dactivating menu sprites when opening settings and opening them
    public void LoadScene_Settings(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        playButton.SetActive(false);
        settingsButton.SetActive(false);
    }

    // Activating menu sprites when closing settings
    public void UnloadScene_Settings()
    {
        playButton.SetActive(true);
        settingsButton.SetActive(true);
    }
}
