using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public Text moneyText;
    public Sprite blueButton, aquaButton, greenButton, orangeButton, pinkButton, yellowButton;

    private int[] birdsPrices;
    private Sprite[] sprites;
    private int numberOfBirds = 6;

    // Use this for initialization
    void Start()
    {
        // Setting default bird to bought in a shop
        PlayerPrefs.SetInt("isBird0Bought", 1);

        // Setting money text
        moneyText.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();

        // Setting bird's prices
        birdsPrices = new int[numberOfBirds];
        birdsPrices[0] = 0;
        birdsPrices[1] = 50;
        birdsPrices[2] = 100;
        birdsPrices[3] = 150;
        birdsPrices[4] = 200;
        birdsPrices[5] = 250;

        // Setting appropriate sprites for plates
        sprites = new Sprite[numberOfBirds];
        sprites[0] = blueButton;
        sprites[1] = aquaButton;
        sprites[2] = greenButton;
        sprites[3] = orangeButton;
        sprites[4] = pinkButton;
        sprites[5] = yellowButton;

        for (int index = 0; index <= numberOfBirds - 1; index++)
        {
            string isBirdBought = "isBird" + index + "Bought";
            string button = index + "Button";
            string buttonText = index + "Price";
            GameObject.Find(buttonText).GetComponent<Text>().text = birdsPrices[index].ToString();
            // If bird is bought setting plate without red line on it
            if (PlayerPrefs.GetInt(isBirdBought, 0) == 1)
            {
                GameObject.Find(button).GetComponent<Button>().image.sprite = sprites[index];
                GameObject.Find(buttonText).GetComponent<Text>().text = null;
            }
        }
    }

    public void LoadBird(int index)
    {
        string isBirdBought = "isBird" + index + "Bought";
        string button = index + "Button";

        // Buying a bird
        if (PlayerPrefs.GetInt(isBirdBought, 0) == 0 && PlayerPrefs.GetInt("Money", 0) >= birdsPrices[index])
        {
            PlayerPrefs.SetInt(isBirdBought, 1);
            int money = PlayerPrefs.GetInt("Money", 0);
            PlayerPrefs.SetInt("Money", money -= birdsPrices[index]);
            PlayerPrefs.SetInt("BirdColor", index);
            string buttonText = index + "Price";

            // Refreshing objects
            GameObject.Find(button).GetComponent<Button>().image.sprite = sprites[index];
            GameObject.Find(buttonText).GetComponent<Text>().text = null;
            moneyText.text = "Money: " + PlayerPrefs.GetInt("Money", 0).ToString();
            return;
        }
        // Setting a bought bird
        if (PlayerPrefs.GetInt(isBirdBought, 0) == 1)
        {
            PlayerPrefs.SetInt("BirdColor", index);
            UnloadScene("shop");
            return;
        }
        // Buying a bird (not enough money)
        if (PlayerPrefs.GetInt(isBirdBought, 0) == 0 && PlayerPrefs.GetInt("Money", 0) < birdsPrices[index])
        {
            return;
        }
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
        GameControl.instance.UnloadScene_Shop();
    }
}
