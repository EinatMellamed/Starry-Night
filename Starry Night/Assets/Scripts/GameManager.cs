using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] List<DragAndPlace> starsInPlace = new List<DragAndPlace>();
   
    

    [SerializeField] GameObject winEffect;
    [SerializeField] GameObject paintingWithOutStars;
    [SerializeField] UIManager uiManager;
   
    [SerializeField] bool gameIsWon;

    private void Start()
    {
       
       for (int i = 0; i < stars.Count; i++)
        {
          var dragAndPlace = stars[i].GetComponent<DragAndPlace>();
            starsInPlace.Add(dragAndPlace);
        }

        paintingWithOutStars.SetActive(true);
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (gameIsWon) return;
        if (AllStarsInPlace())
        {
            gameIsWon = true;
            WinEffect();
            uiManager.OpenWinPanel();
        }
    }
    public bool AllStarsInPlace()
    {
        foreach (DragAndPlace starInPlace in starsInPlace)
        {
            if (!starInPlace.isInPlace)
            {
                return false;
            }
        }
        return true;
    }

    public void WinEffect()
    {
        paintingWithOutStars.SetActive(false);
        foreach (GameObject star in stars)
        {

          
           GameObject starWinEffect = Instantiate(winEffect, star.transform.position, Quaternion.identity);
            Destroy(starWinEffect, 6);
            star.gameObject.SetActive(false);

        }

    }

    public void ActivateStars()
    {
        foreach (GameObject star in stars)
        {

            star.gameObject.SetActive(true);

        }


    }
}
