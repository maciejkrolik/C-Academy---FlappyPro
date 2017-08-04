using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsControl : MonoBehaviour
{
    // Reseting all Player Prefs keys
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
        MenuControl.instance.UnloadScene_Settings();
    }
}
