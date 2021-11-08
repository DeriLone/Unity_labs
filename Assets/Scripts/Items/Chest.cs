using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private int _amountOfCoins;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMover player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.CoinsAmount += _amountOfCoins;
            Debug.Log("You founded " + _amountOfCoins + " coins");
            Destroy(gameObject);
        }
    }
}
