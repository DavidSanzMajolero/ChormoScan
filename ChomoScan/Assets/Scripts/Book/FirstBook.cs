using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBook : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("El jugador ha tocado el libro.");
        }
    }
}
