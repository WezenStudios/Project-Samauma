using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class code_Preloader : MonoBehaviour
{
    [SerializeField] int SceneID;
    [SerializeField] AudioSource logoAudio;
   
    CanvasGroup fadeGroup;
    float loadTime, minLogoTime = 3f;

    void Start()
    {
        // Pega somente o CanvasGroup na cena;
        fadeGroup = FindObjectOfType<CanvasGroup>();

        // Faz iniciar com a tela escura;
        fadeGroup.alpha = 1;

        if(Time.time < minLogoTime)
        {
            loadTime = minLogoTime;
        }
        else
        {
            loadTime = Time.time;
        }

        logoAudio.Play();
    }

    void Update()
    {
        // Fade-In;
        if (Time.time < minLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        // Fade-Out;
        if (Time.time > minLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minLogoTime;
            if(fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene(SceneID);
            }
        }
    }
}