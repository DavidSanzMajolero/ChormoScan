using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBook : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject camera;
    #endregion
    private void Start()
    {
        book.SetActive(false);
        camera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;  
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
        {
            OpenBook();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBook();
        }
    }
    private void OpenBook()
    {
        book.SetActive(true);
        camera.SetActive(true);
        Time.timeScale = 0;
    }
    private void CloseBook()
    {
        book.SetActive(false);
        camera.SetActive(false);
        Time.timeScale = 1;
    }
}
