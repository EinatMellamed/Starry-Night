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
    [SerializeField] Animator animator;

    private int currentTextIndex;
    [SerializeField] GameObject currectIntroText;

    void Start()
    {
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
        
    }

    public void CloseAllUIPanels()
    {
        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public async void OpenInstructionPanel1()
    {
        animator.SetTrigger("ButtonClicked");
        await Task.Delay(1000);
        GameManager.Instance.PlayMusic("IntroTheme");
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
        UIPanels[5].SetActive(true);
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
            UIPanels[7].SetActive(true);
            currentTextIndex++;
        }
        else if (introText[4].activeSelf)
        {
            UIPanels[7].SetActive(false);
            currentTextIndex++;
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
        foreach (GameObject element in backgroundElements)
        {
            element.SetActive(false);
        }
        CloseAllUIPanels();
        UIPanels[2].SetActive(true);
    }
    public void OpenWinPanel()
    {
        CloseAllUIPanels();
        UIPanels[3].SetActive(true);
        GameManager.Instance.PlayMusic("VictoryTheme");
        GameManager.Instance.musicSource.loop = false;
        Time.timeScale = 0f;
    }

    public void OpenMainMenuPanel()
    {
        CloseAllUIPanels();
        UIPanels[4].SetActive(true);
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
        Time.timeScale = 1f;
        GameManager.Instance.musicSource.volume = 0.5f;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenWindPanel()
    {
        CloseAllUIPanels();
        UIPanels[6].SetActive(true);
    }
}
