using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
        GameControl.instance.UnloadScene_Shop();
    }
}
