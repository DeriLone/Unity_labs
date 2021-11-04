using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    void Update()
        {
            transform.position = new Vector3(player.transform.position.x, 0.5f, -10f);
        }
}