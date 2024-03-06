using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public enum PowerUpType
    {
        POWER_UP_TYPE_NONE = -1,
        POWER_UP_TYPE_WEAPON,
        POWER_UP_TYPE_VELOCITY,
        POWER_UP_TYPES
    }

    public PowerUpType powerUpType = PowerUpType.POWER_UP_TYPE_NONE;

    private void DeactivatePowerUp()
    {
        gameObject.SetActive(false);
    }
    private void ReactivatePowerUp()
    {
        Vector3 destinationPosition = Random.insideUnitSphere * 20.0f;
        destinationPosition.y = 2.5f;

        transform.position = destinationPosition;

        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DeactivatePowerUp();

            other.gameObject.GetComponent<TankController>().ApplyPowerUpWeaponry();

            Invoke("ReactivatePowerUp", 10.0f);
        }
    }

    
}
