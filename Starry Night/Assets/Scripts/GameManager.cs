using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio Manager")]
    public MusicAndSFX[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [SerializeField] List<GameObject> stars = new List<GameObject>();
    [SerializeField] List<DragAndPlace> starsInPlace = new List<DragAndPlace>();
    [SerializeField] GameObject wind;


    [SerializeField] GameObject winEffect;
    [SerializeField] GameObject paintingWithOutStars;
    [SerializeField] UIManager uiManager;

    [SerializeField] bool gameIsWon;
    [SerializeField] bool firstWind;
    [SerializeField] float timer = 60f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        firstWind = true;
    }
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
        timer -= Time.deltaTime;
        if (timer <= 0) ActivateWind();
        if (gameIsWon) return;
        if (AllStarsInPlace())
        {
            gameIsWon = true;
            WinEffect();
            uiManager.OpenWinPanel();
            timer = 1f;
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
    public void PlayMusic(string name)
    {
        MusicAndSFX s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        MusicAndSFX s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopPlaySFX(string name)
    {
        MusicAndSFX s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.Stop();
        }
    }

    public async void ActivateWind()
    {

            wind.SetActive(true);
            foreach (GameObject star in stars)
            {

                star.GetComponent<StarMovement>().farwordSpeed = 10f;

            }

            await Task.Delay(3000);
            wind.SetActive(false);
        foreach (GameObject star in stars)
        {

            star.GetComponent<StarMovement>().farwordSpeed = 2f;

        }
        timer = 45f;
       if(firstWind)
        {
            uiManager.OpenWindPanel();
            firstWind= false;

        }

    }
}
