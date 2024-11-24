using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSelector : MonoBehaviour
{
    #region Variables
    public List<GameObject> selectors;
    public List<GameObject> books;
    private int currentIndex = 0;
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    private bool isLooking = false;

    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject enter;

    [SerializeField] private DrinkPot potDrinked;

    private HashSet<int> rotatedBooks = new HashSet<int>(); // Índices de libros rotados

    private int rotatedBookCount = 0; // Contador de libros rotados
    private const int maxRotatedBooks = 4; // Límite de libros que se pueden rotar
    private bool booksRotated;

    [SerializeField] private GameObject shelf; // La estantería que se moverá
    #endregion

    void Start()
    {
        foreach (GameObject selector in selectors)
        {
            selector.SetActive(false);
        }

        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }

        if (cameraObj != null)
        {
            cameraObj.SetActive(false);
            arrow.SetActive(false);
            arrow2.SetActive(false);
            enter.SetActive(false);
        }
    }

    void Update()
    {
        if (isLooking && potDrinked.potionDrinked)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSelection(1);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!rotatedBooks.Contains(currentIndex)) // Solo rota si no está rotado
                {
                    RotateBook(books[currentIndex]);
                    rotatedBooks.Add(currentIndex); // Marca como rotado
                }
            }
        }

        if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
        {
            Look();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
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

    private void Look()
    {
        if (!booksRotated)
        {
            if (cameraObj != null)
            {
                cameraObj.SetActive(true);
                arrow.SetActive(true);
                arrow2.SetActive(true);
                enter.SetActive(true);
                isLooking = true;
                Time.timeScale = 0;
            }

            if (selectors.Count > 0)
            {
                UpdateSelectorVisibility();
            }
        }
    }

    private void Close()
    {
        if (cameraObj != null)
        {
            cameraObj.SetActive(false);
            arrow.SetActive(false);
            arrow2.SetActive(false);
            enter.SetActive(false);
            isLooking = false;
            Time.timeScale = 1;
        }

        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }

        foreach (GameObject selector in selectors)
        {
            selector.SetActive(false);
        }
    }

    private void ChangeSelection(int direction)
    {
        selectors[currentIndex].SetActive(false);
        do
        {
            currentIndex = (currentIndex + direction + selectors.Count) % selectors.Count;
        } while (rotatedBooks.Contains(currentIndex)); // Salta libros rotados

        UpdateSelectorVisibility();
    }

    private void UpdateSelectorVisibility()
    {
        if (!rotatedBooks.Contains(currentIndex))
        {
            selectors[currentIndex].SetActive(true);
        }
    }

    private void RotateBook(GameObject book)
    {
        if (book != null)
        {
            // Ajusta la rotación en el eje X a -60 grados, manteniendo Y y Z
            book.transform.rotation = Quaternion.Euler(-60f, 0f, -90f);

            // Marca el libro como rotado
            selectors[currentIndex].SetActive(false);
            rotatedBooks.Add(currentIndex);

            // Incrementa el contador de libros rotados
            rotatedBookCount++;

            // Verifica si se alcanzó el límite de libros rotados
            if (rotatedBookCount >= maxRotatedBooks)
            {
                booksRotated = true;
                Close(); // Llama a la función de cerrar
                StartCoroutine(MoveShelf()); // Inicia la corrutina para mover la estantería
            }
        }
    }

    private IEnumerator MoveShelf()
    {
        float elapsedTime = 0f;
        float duration = 2f; // Duración del movimiento en segundos
        Vector3 initialPosition = shelf.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(2f, 0f, 0f); // Mueve la estantería en +2 en X

        while (elapsedTime < duration)
        {
            shelf.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shelf.transform.position = targetPosition; // Asegura la posición final
    }
}
