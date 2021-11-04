using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private string _buff;
    [SerializeField] private GameObject _portal;

    private bool _activeBuff = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_activeBuff)
        {
            return;
        }

        PlayerMover player = other.GetComponent<PlayerMover>();
        Portal portal = _portal.GetComponent<Portal>();
        
        if (player != null)
        {
            portal.PortalActivate();
            
            player.AddBuff(_buff);
            _activeBuff = false;
        }
    }
}
