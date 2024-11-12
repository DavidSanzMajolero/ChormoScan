using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject potCamera;
    [SerializeField] private GameObject potArrows;
    private int[] combination = new int[4]; // Ahora la combinaci�n tiene 4 elementos
    private bool combinationDone = false;
    private int currentStep = 0;  // Paso actual en la combinaci�n
    private bool isInPotionCreationMode = false; // Estado para saber si el jugador est� ingresando la combinaci�n
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.O) && !isInPotionCreationMode)
        {
            StartPotionCreation();
        }

        if (isInPotionCreationMode)
        {
            HandlePotionCreation();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || combinationDone)
        {
            ClosePot();
        }
    }

    // Inicia el proceso de creaci�n de la poci�n
    private void StartPotionCreation()
    {
        isInPotionCreationMode = true;
        potCamera.SetActive(true);
        potArrows.SetActive(true);
        Time.timeScale = 0;  // Pausa el juego
    }

    // Maneja la entrada de la combinaci�n
    private void HandlePotionCreation()
    {
        if (currentStep < combination.Length)
        {
            // Detecta las teclas de flechas y las asigna a la combinaci�n
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                combination[currentStep] = 1;
                Debug.Log("Flecha arriba presionada (1)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                combination[currentStep] = 2;
                Debug.Log("Flecha abajo presionada (2)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                combination[currentStep] = 3;
                Debug.Log("Flecha izquierda presionada (3)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                combination[currentStep] = 4;
                Debug.Log("Flecha derecha presionada (4)");
                currentStep++;
            }
        }
        else
        {
            // La combinaci�n ya est� completa, verificamos si es correcta
            ValidateCombination();
        }
    }

    // Valida la combinaci�n ingresada
    private void ValidateCombination()
    {
        // Ejemplo simple: Si la combinaci�n es 1, 2, 3, 4
        if (combination[0] == 1 && combination[1] == 2 && combination[2] == 3 && combination[3] == 4)
        {
            Debug.Log("Combinaci�n correcta, poci�n creada!");
        }
        else
        {
            Debug.Log("Combinaci�n incorrecta. Intenta de nuevo.");
        }

        // Marca la combinaci�n como hecha y cierra la olla
        combinationDone = true;
        ClosePot();
    }

    // Cierra la olla y vuelve al estado normal del juego
    private void ClosePot()
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        Time.timeScale = 1;  // Reactiva el juego
        isInPotionCreationMode = false;  // Resetea el estado
        currentStep = 0;  // Resetea el paso actual
    }
}
