using UnityEngine;

public class PowerUpPool : MonoBehaviour {

    public GameObject powerUpPrefab;
    public float powerUpMin = -1f;
    public float powerUpMax = 3.5f;

    private GameObject powerUp;
    private Vector2 objectPoolPosition = new Vector2 (-12f, -15f);
    private float timeSinceLastSpawned;
    private float spawnRate = 25f;
    private float spawnXPostion = 12f;

    // Use this for initialization
    void Start ()
    {
        // Spawning a power-up
        powerUp = Instantiate(powerUpPrefab, objectPoolPosition, Quaternion.identity);
        // Lottery spawn y position from the given range
        float spawnYPosition = Random.Range(powerUpMin, powerUpMax);
        // Setting first position of the power-up
        powerUp.transform.position = new Vector2(spawnXPostion, spawnYPosition);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawned += Time.deltaTime;

        // Changing power-up position
        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(powerUpMin, powerUpMax);
            powerUp.transform.position = new Vector2(spawnXPostion, spawnYPosition);
        }
    }
}
