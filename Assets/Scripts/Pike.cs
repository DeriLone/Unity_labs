using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _pushPower;
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMover player = other.gameObject.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.TakeDamage(_damage, _pushPower, transform.position.x);
        }
    }
}
