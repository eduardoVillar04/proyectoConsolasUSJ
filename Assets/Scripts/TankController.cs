using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : ObjectController
{

    [Header("Acceleration variables")]
    public float accelerationLinear = 0.0f;
    public float accelerationAngular = 0.0f;

    [Header("Projectile variables")]
    public GameObject projectilePrefab = null;
    public Transform projectileSpawnPoint = null;
    public float projectileSpeed = 10.0f;
    public float projectileCooldown = 0.1f;
    public float projectileCooldownMultiplier = 1.0f;
    private float projectileNextTime = 0.0f;

    [Header("Rigidbody")]
    public Rigidbody rigidBody = null;

    [Header("Score variables")]
    public TMPro.TextMeshProUGUI scoreText = null;
    private int score = 0;
    private int highScore = 0;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        accelerationLinear = 20f;
        accelerationAngular = 20f;
        projectileSpeed = 50f;
        projectileCooldown = 0.5f;

        SetScore(0);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(Time.time > projectileNextTime)
            {
                projectileNextTime = Time.time + GetProjectileCooldown() * Random.Range(0.9f,1.1f);
                Fire();
            }
        }
    }

    private void FixedUpdate()
    {
        Inputs();
    }

    public void Inputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(transform.forward * accelerationLinear, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(transform.forward * -accelerationLinear, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddTorque(transform.up * -accelerationAngular, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddTorque(transform.up * accelerationAngular, ForceMode.Acceleration);
        }

    }

    private void OnDestroy()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);

        newProjectile.transform.position = projectileSpawnPoint.position;
        newProjectile.transform.rotation = projectileSpawnPoint.rotation;

        newProjectile.GetComponent<Rigidbody>().velocity = rigidBody.velocity;
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * projectileSpeed, ForceMode.VelocityChange);

        newProjectile.GetComponent<ProjectileController>().SetOwner(this);
    }

    public void ApplyPowerUpWeaponry()
    {
        projectileCooldownMultiplier = 0.1f;
        Invoke("RevertPowerUpWeaponry", 10.0f);
    }

    public void RevertPowerUpWeaponry()
    {
        projectileCooldownMultiplier = 1.0f;
    }

    private float GetProjectileCooldown()
    {
        return projectileCooldownMultiplier * projectileCooldown;
    }

    public override void IncreaseScore(int increment)
    {
        SetScore(score + increment);
    }

    private void SetScore(int scoreValue)
    {
        score = scoreValue;
        scoreText.text = "Score: " + score + "\nHighScore: " + highScore;
    }
}
