using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _nextSecene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMover player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            SceneManager.LoadScene(_nextSecene);
        }
    }
    public void PortalActivate()
    {
        gameObject.SetActive(true);
        
        BecomeVisible visibility = GetComponent<BecomeVisible>();
        visibility.StartVisible();
    }
}
