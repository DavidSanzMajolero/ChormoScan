using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject potCamera;
    [SerializeField] private GameObject potArrows;
    [SerializeField] private GameObject potParticles;
    [SerializeField] private GameObject greenPotion;
    private int[] combination = new int[4]; // La combinación tiene 4 elementos
    private int currentStep = 0;  // Paso actual en la combinación
    private bool isInPotionCreationMode = false; // Estado para saber si el jugador está ingresando la combinación
    public bool hasEnteredFullCombination = false; // Controla si el jugador ya ingresó los 4 dígitos
    #endregion

    private void Start()
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        potParticles.SetActive(false);
        greenPotion.SetActive(false);
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.O) && !isInPotionCreationMode && !hasEnteredFullCombination)
        {
            StartPotionCreation();
        }

        if (isInPotionCreationMode)
        {
            HandlePotionCreation();
        }

        // Cierra la olla si el jugador presiona Escape o si ha completado los 4 pasos
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePot(resetCombination: true); // Reinicia la combinación si el jugador sale sin completar
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

        // Verifica si la combinación está completa
        if (currentStep >= combination.Length)
        {
            hasEnteredFullCombination = true; // Marca que el jugador ingresó los 4 dígitos
            potParticles.SetActive(true);
            greenPotion.SetActive(true);
            Debug.Log("Combinación completa ingresada y almacenada: " + string.Join(", ", combination));
            ClosePot(resetCombination: false); // Cierra sin resetear ya que completó la combinación
        }
    }

    // Cierra la olla y vuelve al estado normal del juego
    private void ClosePot(bool resetCombination)
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        isInPotionCreationMode = false;  // Resetea el estado de entrada al caldero
        Time.timeScale = 1;  // Reactiva el juego

        if (resetCombination)
        {
            currentStep = 0;  // Reinicia el paso actual solo si el jugador salió sin completar
            Array.Clear(combination, 0, combination.Length); // Borra la combinación actual
            Debug.Log("Combinación reiniciada. Debes ingresar los 4 dígitos de nuevo.");
        }
    }

    // Método para obtener la combinación almacenada
    public int[] GetCombination()
    {
        return combination;
    }

    // Método para verificar si el jugador ya ingresó la combinación
    public bool HasEnteredFullCombination()
    {
        return hasEnteredFullCombination;
    }
}
