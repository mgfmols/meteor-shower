using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 35f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;

    [SerializeField] ScoreHandler handler;
    [SerializeField] ParticleSystem exhaustEffect;
    [SerializeField] ParticleSystem rocketBoostEffect;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] CameraMovement cameraMovement;

    [SerializeField] EventInstance rocketBooster;
    [SerializeField] EventInstance explosionSound;
    [SerializeField] EventInstance backgroundMusic;

    private bool playerAlive = true;
    private bool gameStartUp = true;

    public bool GameStartup { get { return gameStartUp; } set { gameStartUp = value; } }

    void Start()
    {
        rocketBooster = FMODUnity.RuntimeManager.CreateInstance("event:/RocketBooster");
        explosionSound = FMODUnity.RuntimeManager.CreateInstance("event:/MeteorHit");
        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance("event:/BackgroundMusic");

        rocketBooster.setVolume(0.5f);
        explosionSound.setVolume(0.5f);
        backgroundMusic.setVolume(0.5f);

        rocketBooster.start();
        rocketBooster.setParameterByName("Speed", 0);
        backgroundMusic.start();
    }

    void Update()
    {
        if (playerAlive && !gameStartUp)
        {
            LookAtMouse();
            MovePlayer();
        }
        rocketBooster.setParameterByName("Speed", rb.velocity.x - rb.velocity.y);
    }

    void LookAtMouse()
    {
        // Make object look at the mouse
        transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z)));
    }

    void MovePlayer()
    {
        //rb.AddForce(transform.forward * movementSpeed / 8);
        if (Input.GetKey(KeyCode.Space))
        {
            // Add speed relative to forward direction.
            rb.AddForce(transform.forward * movementSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketBoostEffect.gameObject.transform.localScale = rocketBoostEffect.gameObject.transform.localScale * 2;
            EmissionModule emission = exhaustEffect.emission;
            emission.rateOverTime = new MinMaxCurve(100);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rocketBoostEffect.gameObject.transform.localScale = rocketBoostEffect.gameObject.transform.localScale / 2;
            EmissionModule emission = exhaustEffect.emission;
            emission.rateOverTime = new MinMaxCurve(20);
        }
    }

    public void Die()
    {
        canvasAnimator.enabled = true;
        canvasAnimator.Play("GameOver");
        handler.SaveScore();
        exhaustEffect.Stop();
        rocketBoostEffect.Stop();
        explosionEffect.Play();
        cameraMovement.StopMovement();
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerAlive = false;

        explosionSound.start();
        backgroundMusic.stop(STOP_MODE.ALLOWFADEOUT);
        rocketBooster.stop(STOP_MODE.IMMEDIATE);
    }

    private void OnBecameInvisible()
    {
        Die();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Die();
        }
    }
}
