using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<DragAndPlace> stars = new List<DragAndPlace>();
    [SerializeField] bool gameIsWon;
    void Update()
    {
        if (gameIsWon) return;
        if (AllStarsInPlace())
        {
            gameIsWon = true;
            Time.timeScale = 0f;
            Debug.Log("Congratulations! You won the game!");
        }
    }
    public bool AllStarsInPlace()
    {
        foreach (DragAndPlace star in stars)
        {
            if (!star.isInPlace)
            {
                return false;
            }
        }
        return true;
    }
}
