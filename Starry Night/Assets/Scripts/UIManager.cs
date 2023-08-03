using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> UIPanels = new List<GameObject>();
    [SerializeField] List<GameObject> introText = new List<GameObject>();
    [SerializeField] List<GameObject> backgroundElements = new List<GameObject>();
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject starsHolder;
    [SerializeField] GameObject starExample;
    [SerializeField] GameObject StarExampleArrow;
    [SerializeField] GameObject introTextButton;
    [SerializeField] Animator animator;
    [SerializeField] Animator scoreWindowAnim;
    [SerializeField] Animator winPanel;
    [SerializeField] Animator scoreTextAnim;


    private int currentTextIndex;
    [SerializeField] GameObject currectIntroText;

    void Start()
    {
        starsHolder.SetActive(false);
        starExample.SetActive(false);
        StarExampleArrow.SetActive(false);
        introTextButton.SetActive(true);
        animator = UIPanels[0].GetComponent<Animator>();
        GameManager.Instance.PlayMusic("CoverTheme");
        GameManager.Instance.musicSource.volume = 0.5f;
        GameManager.Instance.musicSource.loop = true;
        gameManager = FindObjectOfType<GameManager>();
        CloseAllUIPanels();
        CloseAllIntroTexts();
        UIPanels[0].SetActive(true);
        introText[0].SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (starExample.GetComponent<DragAndPlace>().firstStarInPlace && introText[4].activeSelf)
        {
            introText[4].SetActive(false);
            StarExampleArrow.SetActive(false);
            introText[5].SetActive(true);
            introTextButton.SetActive(true);

        }
    }
    public void CloseAllUIPanels()
    {
        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
           
        }
        Time.timeScale = 1f;
    }

    public async void OpenInstructionPanel1()
    {
        animator.SetTrigger("ButtonClicked");
        await Task.Delay(1000);
        GameManager.Instance.PlayMusic("IntroTheme");
        CloseAllUIPanels();
        UIPanels[1].SetActive(true);
        UIPanels[8].SetActive(true);
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
        starsHolder.SetActive(true);
        CloseAllUIPanels();
        UIPanels[5].SetActive(true);
        UIPanels[8].SetActive(true);
        gameManager.ActivateStars();
        GameManager.Instance.PlayMusic("GameTheme");
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
        if (!currectIntroText)
        {
            currentTextIndex = 1;
            currectIntroText = introText[1];
            introText[0].SetActive(false);
        }
        else if (introText[3].activeSelf)
        {
            starExample.SetActive(true);
            StarExampleArrow.SetActive(true);
            introTextButton.SetActive(false);
           starExample.GetComponent<DragAndPlace>().starAnim.SetTrigger("firstTouch");

            currentTextIndex++;
            // UIPanels[7].SetActive(true);

        }
       
        //  else if (introText[5].activeSelf)
        // {
        // UIPanels[7].SetActive(false);
        //    currentTextIndex++;
        // }
        else if (introText[6].activeSelf)
        {
           
            StartGame();
            starExample.SetActive(false);
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
        UIPanels[2].SetActive(true);
        UIPanels[8].SetActive(true);

    }
    public async void OpenWinPanel()
    {
        CloseAllUIPanels();
        UIPanels[3].SetActive(true);
        UIPanels[8].SetActive(true);
        scoreWindowAnim.SetTrigger("game won");
        GameManager.Instance.PlayMusic("VictoryTheme");
        GameManager.Instance.musicSource.loop = false;
        await Task.Delay(7000);
        Time.timeScale = 0f;
        scoreWindowAnim.SetTrigger("needsToFade");
       winPanel.SetTrigger("needsToFade");
       scoreTextAnim.SetTrigger("needsToFade");
        GameManager.Instance.PlayMusic("CoverTheme");
        GameManager.Instance.musicSource.loop = true; 
        await Task.Delay(1000);
        UIPanels[3].SetActive(false);
        UIPanels[8].SetActive(false);
        UIPanels[5].SetActive(true);
    }

    public void OpenMainMenuPanel()
    {
        CloseAllUIPanels();
        UIPanels[4].SetActive(true);
        UIPanels[8].SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.musicSource.volume = 0.2f;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
      
        CloseAllUIPanels();
       
        UIPanels[5].SetActive(true);
        UIPanels[8].SetActive(true);
        if (GameManager.Instance.gameIsWon)
        {
            UIPanels[8].SetActive(false);
        }
        Time.timeScale = 1f;
        GameManager.Instance.musicSource.volume = 0.5f;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenWindPanel()
    {
        foreach (GameObject element in backgroundElements)
        {
            element.SetActive(false);
        }
        CloseAllUIPanels();
        UIPanels[8].SetActive(true);
        UIPanels[6].SetActive(true);
    }

    public void CloseWinPanel()
    {
      
       CloseAllUIPanels();
        UIPanels[5].SetActive(true);
        GameManager.Instance.PlayMusic("CoverTheme");
        GameManager.Instance.musicSource.loop = true;
       
    }

    
}
