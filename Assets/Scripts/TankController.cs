using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    public float accelerationLinear = 0.0f;
    public float accelerationAngular = 0.0f;

    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 0.0f;

    public Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        accelerationLinear = 20f;
        accelerationAngular = 20f;
        projectileSpeed = 50f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
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

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab);

        newProjectile.transform.position = projectileSpawnPoint.position;
        newProjectile.transform.rotation = projectileSpawnPoint.rotation;

        newProjectile.GetComponent<Rigidbody>().velocity = rigidBody.velocity;
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * projectileSpeed, ForceMode.VelocityChange);
    }
}
