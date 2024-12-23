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
    [SerializeField] private Pot pot;  // Referencia al script Pot

    private bool playerLooking = false;

    public bool potionsSelected = false;
    [SerializeField] public GameObject msg;

    private List<int> selectedPotions = new List<int>(); // Lista para rastrear las pociones seleccionadas
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
        // Solo permite interactuar si a�n no se han seleccionado dos pociones
        if (selectedPotions.Count < 2)
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
            {
                Look();
            }

            // Si el jugador presiona Escape, cerramos el men�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }

            // Si la combinaci�n est� completa, habilitamos las opciones de las pociones
            if (pot.HasEnteredFullCombination() && playerLooking)
            {
                ChoosePotion();
            }
        }
    }

    private void Look()
    {
        playerLooking = true;
        cameraObj.SetActive(true);
        if (pot.HasEnteredFullCombination())
        {
            potionNum1.SetActive(true);
            potionNum2.SetActive(true);
            potionNum3.SetActive(true);
        }
        Time.timeScale = 0; // Pausa el juego
    }

    private void ChoosePotion()
    {
        // Detectar la tecla "1" para seleccionar la primera poci�n
        if (Input.GetKeyDown(KeyCode.Alpha1) && !selectedPotions.Contains(1))
        {
            Debug.Log("Poci�n 1 seleccionada");
            selectedPotions.Add(1);  // A�adir la poci�n seleccionada a la lista
        }

        // Detectar la tecla "2" para seleccionar la segunda poci�n
        if (Input.GetKeyDown(KeyCode.Alpha2) && !selectedPotions.Contains(2))
        {
            Debug.Log("Poci�n 2 seleccionada");
            selectedPotions.Add(2);  // A�adir la poci�n seleccionada a la lista
        }

        // Detectar la tecla "3" para seleccionar la tercera poci�n
        if (Input.GetKeyDown(KeyCode.Alpha3) && !selectedPotions.Contains(3))
        {
            Debug.Log("Poci�n 3 seleccionada");
            selectedPotions.Add(3);  // A�adir la poci�n seleccionada a la lista
        }

        // Si ya se han seleccionado dos pociones, cerramos el men�
        if (selectedPotions.Count >= 2)
        {
            potionsSelected = true;
            msg.SetActive(true);
            Close();
        }
    }

    private void Close()
    {
        playerLooking = false;
        cameraObj.SetActive(false);
        potionNum1.SetActive(false);
        potionNum2.SetActive(false);
        potionNum3.SetActive(false);
        Time.timeScale = 1;  // Reactiva el juego
    }
}
