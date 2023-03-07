using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (gameObject.name != "hpUP(Clone)" || collision.GetComponent<Player>().health != collision.GetComponent<Player>().hearts.Length)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation); 
                collision.GetComponent<Player>().Pickups(gameObject);
                Destroy(gameObject);
            }
        }
    }

}
