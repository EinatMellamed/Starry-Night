using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> UIPanels = new List<GameObject>();
    [SerializeField] List<GameObject> introText = new List<GameObject>();
    [SerializeField] GameManager gameManager;

    private int currentTextIndex;
    [SerializeField] GameObject currectIntroText;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        CloseAllUIPanels();
        CloseAllIntroTexts();
        UIPanels[0].SetActive(true);
        introText[0].SetActive(true);
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
    public void CloseAllIntroTexts()
    {
        foreach (GameObject text in introText)
        {

            text.SetActive(false);
        }

    }

    public void OpenNextText()
    {
   
        if(!currectIntroText)
        {
            currentTextIndex = 1;
            currectIntroText = introText[1];
            introText[0].SetActive(false);

        }

       else if (introText[5].activeSelf)
        {
            StartGame();

        }
        else
        {
            currentTextIndex++;

        }
        var myText = introText[currentTextIndex];
        currectIntroText.SetActive(false);
        myText.gameObject.SetActive(true);
        currectIntroText = myText.gameObject;




    }

    public void OpenMiddlePanel()
    {

        CloseAllUIPanels();
        introText[2].SetActive(false);
    }
}
