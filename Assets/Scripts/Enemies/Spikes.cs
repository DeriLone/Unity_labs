using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        PlayerMover player = other.gameObject.GetComponent<PlayerMover>();
        if (player != null)
        {
            int damage = player.GetMaxHp();
            player.TakeDamage(damage);
        }
    }
}
