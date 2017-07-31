using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsControl : MonoBehaviour
{
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
