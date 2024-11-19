using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject potionNum1;
    [SerializeField] private GameObject potionNum2;
    [SerializeField] private GameObject potionNum3;
    Pot pot;
    #endregion
    private void Start()
    {
        cameraObj.SetActive(false);
        potionNum1.SetActive(false);
        potionNum2.SetActive(false);
        potionNum3.SetActive(false);
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
        Time.timeScale = 0;
        if (pot.hasEnteredFullCombination)
        {
            potionNum1.SetActive(true);
            potionNum2.SetActive(true);
            potionNum3.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1)) Debug.Log("1");
            if (Input.GetKeyDown(KeyCode.Alpha2)) Debug.Log("2");
            if (Input.GetKeyDown(KeyCode.Alpha3)) Debug.Log("3");
            if (Input.GetKeyDown(KeyCode.Alpha4)) Debug.Log("4");
        }
    }
    private void Close()
    {
        cameraObj.SetActive(false);
        Time.timeScale = 1;
    }
}
