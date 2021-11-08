using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPotion : MonoBehaviour
{
    [SerializeField] private int _staminaPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMover player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.AddStamina(_staminaPoints);
            Destroy(gameObject);
        }
    }
}
