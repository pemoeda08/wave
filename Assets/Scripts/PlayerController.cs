using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void ItemGetEvent();
    public event ItemGetEvent OnItemGetEvent;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    public delegate void CheckpointEvent();
    public event DeathEvent OnCheckpointHit;

    public GameObject deathEffect;
    public GameObject collectibleEffect;

    public Rigidbody rb;
    public float delta = 2.3f;
    public float lrSpeed = 2.5f;
    public float upSpeed = 2.5f;
    public float maxUpSpeed = 3f;

    public float currentSineX = 0f;
    public float currentVelocityX = 0f;

    public bool isBoosted = false;

    public AudioClip itemSound;
    public AudioClip deathSound;

    private bool isMouseUp = true;


    Vector3 startPos;
    bool isDead = false;

    GameController gameController;
    float hueValue;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = new Vector3(0, -3, 0);

        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    void Start()
    {
        hueValue = Random.Range(0, 10) / 10.0f;
        SetBackgroundColor();
    }

    void FixedUpdate()
    {
        if (isDead == true) return;

        Vector3 newPos = startPos;
        currentSineX = Mathf.Sin(Time.time * lrSpeed);
        newPos.x += delta * currentSineX;

        currentVelocityX = (newPos.x - transform.position.x) / Time.deltaTime;
        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

        gameController.UpdateDistance();
        //Use toggle control scheme
        //if (Input.GetMouseButton(0) && isMouseUp)
        //{
        //    isBoosted = !isBoosted;
        //    isMouseUp = false;
        //}
        //else if (!Input.GetMouseButton(0) && !isMouseUp)
        //{
        //    isMouseUp = true;
        //}


        // else if(!Input.GetMouseButton(0))
        // {
        //     isBoosted = false;
        // }

        if (isBoosted)
        {
            rb.AddForce(transform.up * upSpeed);
        }

    }

    // Audio delay fix
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            SoundManager.instance.PlaySingle(deathSound);
            // SoundManager.instance.musicSource.Stop(); // Stops the music
            Death();
        }
        else if (other.gameObject.tag == "Collectible")
        {
            SoundManager.instance.PlaySingle(itemSound);
            GetItem(other);
        }
        else if (other.gameObject.tag == "Checkpoint")
        {
            OnCheckpointHit();
        }
    }

    void GetItem(Collider other)
    {
        SetBackgroundColor();

        Destroy(Instantiate(collectibleEffect, other.gameObject.transform.position, Quaternion.identity), 0.5f);
        Destroy(other.gameObject);
        gameController.AddScore();

        OnItemGetEvent();
    }

    public void ResetState()
    {
        isDead = false;
        isBoosted = false;
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, -3, 0);
        rb.isKinematic = false;
    }

    void Death()
    {
        isDead = true;
        StopPlayer();
        StartCoroutine(Camera.main.gameObject.GetComponent<CameraShake>().Shake());

        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 0.7f);

        OnDeath();

        //gameController.CallGameOver();
        //gameController.Restart();
    }

    void StopPlayer()
    {
        //Destroy(this.GetComponent<SphereCollider>());
        rb.velocity = new Vector3(0, 0, 0);
        rb.isKinematic = true;
    }

    void SetBackgroundColor()
    {
        Camera.main.backgroundColor = Color.HSVToRGB(hueValue, 0.6f, 0.8f);

        hueValue += 0.1f;
        if (hueValue >= 1)
        {
            hueValue = 0;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    public Vector2 DistanceFromStart
    {
        get
        {
            return transform.position - startPos;
        }
    }
}
