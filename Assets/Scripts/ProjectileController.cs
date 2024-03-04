using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private ObjectController owner = null;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destructible"))
        {
            if (GetOwner() != null)
            {
                GetOwner().IncreaseScore(10);
            }


            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public ObjectController GetOwner()
    {
        return owner;
    }

    public void SetOwner(TankController newOwner)
    {
        owner = newOwner;
    }


}
