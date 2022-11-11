using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class code_Subtitle : MonoBehaviour
{
    public Toggle subtitleToggle;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        OnSubtitle();
    }

    public void OnSubtitle()
    {
        GameObject[] sentencesArray = GameObject.FindGameObjectsWithTag("Sentences");

        if (subtitleToggle.isOn)
        {
            foreach (var obj in sentencesArray)
                obj.SetActive(true);
        }
        else
        {
            foreach (var obj in sentencesArray)
                obj.SetActive(false);
        }
    }
}
