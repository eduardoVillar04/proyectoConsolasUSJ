using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : ObjectController
{
    private TankController tank;

    public float acceleration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        tank = FindObjectOfType<TankController>();    
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = (tank.transform.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(acceleration * direction, ForceMode.Acceleration);
    }
}
