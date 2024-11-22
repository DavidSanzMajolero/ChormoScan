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
        }
    }

    void Update()
    {
        if (isLooking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectors[currentIndex].SetActive(false);
                currentIndex = (currentIndex - 1 + selectors.Count) % selectors.Count;
                selectors[currentIndex].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectors[currentIndex].SetActive(false);
                currentIndex = (currentIndex + 1) % selectors.Count;
                selectors[currentIndex].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                RotateBook(books[currentIndex]);
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
        if (cameraObj != null)
        {
            cameraObj.SetActive(true);
            arrow.SetActive(true);
            arrow2.SetActive(true);
            isLooking = true;
            Time.timeScale = 0;  
        }

        if (selectors.Count > 0)
        {
            selectors[currentIndex].SetActive(true);  
        }

        books[currentIndex].SetActive(true); 
    }

    private void Close()
    {
        if (cameraObj != null)
        {
            cameraObj.SetActive(false);
            arrow.SetActive(false);
            arrow2.SetActive(false);
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

    private void RotateBook(GameObject book)
    {
        if (book != null)
        {
            book.transform.Rotate(Vector3.up, 90f);  
        }
    }
}
