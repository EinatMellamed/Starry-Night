using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] static int scoreCount;
    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 0; 
        score.text = scoreCount.ToString();
    }


    public void AddScore()
    {

        scoreCount += 50;
        score.text = scoreCount.ToString();
    }
  
}
