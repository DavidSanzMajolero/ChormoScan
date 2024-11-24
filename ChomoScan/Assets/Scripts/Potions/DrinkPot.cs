using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPot : MonoBehaviour
{
    #region Variables
    [SerializeField] private Potions potions;
    public bool potionDrinked = false;
    private bool playerInTrigger = false;
    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }
    private void Update()
    {
        if (potions.potionsSelected && Input.GetKeyDown(KeyCode.O) && playerInTrigger)
        {
            StartCoroutine(Drink());
        }
    }
    private IEnumerator Drink()
    {
        yield return new WaitForSeconds(2f);
        potions.msg.SetActive(false);
        potionDrinked = true;
    }
}

