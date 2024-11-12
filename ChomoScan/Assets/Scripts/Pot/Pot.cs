using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject potCamera;
    [SerializeField] private GameObject potArrows;
    private int[] combination = new int[4]; // Ahora la combinación tiene 4 elementos
    private bool combinationDone = false;
    private int currentStep = 0;  // Paso actual en la combinación
    private bool isInPotionCreationMode = false; // Estado para saber si el jugador está ingresando la combinación
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

    // Inicia el proceso de creación de la poción
    private void StartPotionCreation()
    {
        isInPotionCreationMode = true;
        potCamera.SetActive(true);
        potArrows.SetActive(true);
        Time.timeScale = 0;  // Pausa el juego
    }

    // Maneja la entrada de la combinación
    private void HandlePotionCreation()
    {
        if (currentStep < combination.Length)
        {
            // Detecta las teclas de flechas y las asigna a la combinación
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
            // La combinación ya está completa, verificamos si es correcta
            ValidateCombination();
        }
    }

    // Valida la combinación ingresada
    private void ValidateCombination()
    {
        // Ejemplo simple: Si la combinación es 1, 2, 3, 4
        if (combination[0] == 1 && combination[1] == 2 && combination[2] == 3 && combination[3] == 4)
        {
            Debug.Log("Combinación correcta, poción creada!");
        }
        else
        {
            Debug.Log("Combinación incorrecta. Intenta de nuevo.");
        }

        // Marca la combinación como hecha y cierra la olla
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
