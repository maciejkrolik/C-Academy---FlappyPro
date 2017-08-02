using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    private bool isDead = false, isNight = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    private GameObject[] characterList;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //Changing bird color
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        int birdColor = PlayerPrefs.GetInt("BirdColor", 0);
        characterList[birdColor].SetActive(true);

        //Assigning right animation
        anim = characterList[birdColor].GetComponent<Animator>();

        //Doing one flap after loading the scene
        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger("Flap");

        //Checking if it's night mode
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
            if (Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
                GameObject.Find("Bird").GetComponent<AudioSource>().Play();
            }
        }

        if (isNight == true)
        {
            Camera GameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            PostProcessingBehaviour filters = GameCamera.GetComponent<PostProcessingBehaviour>();
            PostProcessingProfile profile = filters.profile;
            VignetteModel.Settings g = profile.vignette.settings;
            g.center.y = ((GameObject.Find("Bird").transform.position.y) + 5) / 10;
            profile.vignette.settings = g;
        }
    }

    private void OnCollisionEnter2D()
    {
        rb2d.velocity = new Vector2(2, 0);
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.BirdDied();
    }
}
