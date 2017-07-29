using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;

    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    private GameObject[] characterList;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Changing bird color
        characterList = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }
        int birdColor = PlayerPrefs.GetInt("BirdColor", 0);
        characterList[birdColor].SetActive(true);

        //Doing one flap after loading the scene
        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger("Flap");
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
            }
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
