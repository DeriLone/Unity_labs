using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMover player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.CoinsAmount += 1;
            Debug.Log("You founded a coin");
            Destroy(gameObject);
        }
    }
}
