using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMover player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            Debug.Log("portal");
            SceneManager.LoadScene("Level_2");
        }
    }
    public void PortalActivate()
    {
        gameObject.SetActive(true);
        
        BecomeVisible visibility = GetComponent<BecomeVisible>();
        visibility.StartVisible();
    }
}
