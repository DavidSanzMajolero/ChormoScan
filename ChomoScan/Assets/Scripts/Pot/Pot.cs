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
    private int[] combination = new int[4]; // La combinaci�n tiene 4 elementos
    private int currentStep = 0;  // Paso actual en la combinaci�n
    private bool isInPotionCreationMode = false; // Estado para saber si el jugador est� ingresando la combinaci�n
    public bool hasEnteredFullCombination = false; // Controla si el jugador ya ingres� los 4 d�gitos
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
            ClosePot(resetCombination: true); // Reinicia la combinaci�n si el jugador sale sin completar
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

        // Verifica si la combinaci�n est� completa
        if (currentStep >= combination.Length)
        {
            hasEnteredFullCombination = true; // Marca que el jugador ingres� los 4 d�gitos
            potParticles.SetActive(true);
            greenPotion.SetActive(true);
            Debug.Log("Combinaci�n completa ingresada y almacenada: " + string.Join(", ", combination));
            ClosePot(resetCombination: false); // Cierra sin resetear ya que complet� la combinaci�n
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
            currentStep = 0;  // Reinicia el paso actual solo si el jugador sali� sin completar
            Array.Clear(combination, 0, combination.Length); // Borra la combinaci�n actual
            Debug.Log("Combinaci�n reiniciada. Debes ingresar los 4 d�gitos de nuevo.");
        }
    }

    // M�todo para obtener la combinaci�n almacenada
    public int[] GetCombination()
    {
        return combination;
    }

    // M�todo para verificar si el jugador ya ingres� la combinaci�n
    public bool HasEnteredFullCombination()
    {
        return hasEnteredFullCombination;
    }
}
