using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] List<DragAndPlace> starsInPlace = new List<DragAndPlace>();
    

    [SerializeField] GameObject winEffect;
    [SerializeField] GameObject paintingWithOutStars;
   
    [SerializeField] bool gameIsWon;

    private void Start()
    {
       for(int i = 0; i < stars.Count; i++)
        {
          var dragAndPlace = stars[i].GetComponent<DragAndPlace>();
            starsInPlace.Add(dragAndPlace);
        }

        paintingWithOutStars.SetActive(true);
    }
    void Update()
    {
        if (gameIsWon) return;
        if (AllStarsInPlace())
        {
            gameIsWon = true;
            WinEffect();
          //  Time.timeScale = 0f;
            Debug.Log("Congratulations! You won the game!");
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
            Destroy(starWinEffect, 4);
            star.gameObject.SetActive(false);

        }

       

       

       

    }
}
