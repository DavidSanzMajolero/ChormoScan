using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banners : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject test1;
    [SerializeField] private GameObject test2;
    [SerializeField] private GameObject test3;
    [SerializeField] private GameObject test4;
    #endregion
    private void Start()
    {
        cameraObj.SetActive(false);
        test1.SetActive(false);
        test2.SetActive(false);
        test3.SetActive(false);
        test4.SetActive(false);
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
            Look();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    private void Look()
    {
        cameraObj.SetActive(true);
        test1.SetActive(true);
        test2.SetActive(true);
        test3.SetActive(true);
        test4.SetActive(true);
        Time.timeScale = 0;
    }
    private void Close()
    {
        cameraObj.SetActive(false);
        test1.SetActive(false);
        test2.SetActive(false);
        test3.SetActive(false);
        test4.SetActive(false);
        Time.timeScale = 1;
    }
}
