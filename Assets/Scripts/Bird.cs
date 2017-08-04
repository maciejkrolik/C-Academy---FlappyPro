using UnityEngine;
using UnityEngine.PostProcessing;

public class Bird : MonoBehaviour
{
    // Machine is on means that power-up is active

    public float upForce = 200f;

    private bool isDead = false, isNight = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    private GameObject[] characterList;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Changing bird color
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        int birdColor = PlayerPrefs.GetInt("BirdColor", 0);
        characterList[birdColor].SetActive(true);

        // Assigning right animation
        anim = characterList[birdColor].GetComponent<Animator>();

        // Doing one flap after loading the scene
        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger("Flap");

        // Checking if it's night mode
        if (GameObject.Find("Main Camera").GetComponent<PostProcessingBehaviour>() != null)
        {
            isNight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            // Doing flap when screen is touched
            if (Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
                GetComponent<AudioSource>().Play();
            }
        }

        if (isNight == true)
        {
            // Setting vignette effect when night mode is on
            Camera GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            PostProcessingBehaviour filters = GameCamera.GetComponent<PostProcessingBehaviour>();
            PostProcessingProfile profile = filters.profile;
            VignetteModel.Settings g = profile.vignette.settings;
            g.center.y = ((transform.position.y) + 5) / 10;
            profile.vignette.settings = g;
        }
    }

    // Late Update is called right after Update()
    void LateUpdate()
    {
        if (GameControl.instance.isMachineOn == true)
        {
            Vector2 desiredPosition = new Vector2(6, transform.position.y);
            Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, 0.125f);
            transform.position = smoothedPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameControl.instance.isMachineOn == false)
        {
            rb2d.velocity = new Vector2(2, 0);
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
        }

        if (GameControl.instance.isMachineOn == true)
        {
            if (collision.gameObject.name == "Columns(Clone)")
            {
                // Turning off rotation when the machine is on
                GameControl.instance.BirdScored();
                rb2d.freezeRotation = true;
                transform.localRotation = Quaternion.identity;

                // Deleting columns from a camera view 
                Vector2 pos = collision.transform.position;
                pos.y += 20f;
                collision.transform.position = pos;
            }
        }
    }
    
    // When bird is sliding on a grass
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameControl.instance.isMachineOn == false && isDead == false)
        {
            rb2d.velocity = new Vector2(2, 0);
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.BirdDied();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GameControl.instance.isMachineOn == true)
        {
            if (collision.gameObject.name == "Columns(Clone)")
            {
                // Turning on rotation when the machine is off
                rb2d.freezeRotation = false;
            }
        }
    }
}
