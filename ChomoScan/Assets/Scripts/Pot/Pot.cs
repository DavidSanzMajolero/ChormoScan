using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject potCamera;
    [SerializeField] private GameObject potArrows;
    #endregion
    private void Start()
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
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
            LookPot();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePot();
        }
    }
    private void LookPot()
    {
        potCamera.SetActive(true);
        potArrows.SetActive(true);
        Time.timeScale = 0;
    }
    private void ClosePot()
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        Time.timeScale = 1;
    }

}
