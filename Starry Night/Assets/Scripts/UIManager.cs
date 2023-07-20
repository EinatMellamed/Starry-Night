using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> UIPanels= new List<GameObject>();
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        CloseAllUIPanels();
        UIPanels[0].SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAllUIPanels()
    {

        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
 
    public void OpenInstructionPanel1()
    {

        CloseAllUIPanels();
        UIPanels[1].SetActive(true);
        Time.timeScale = 0f;
    }
    public void OpenInstructionPanel2()
    {

        CloseAllUIPanels(); 
        UIPanels[2].SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        CloseAllUIPanels();
        gameManager.ActivateStars();

    }
}
