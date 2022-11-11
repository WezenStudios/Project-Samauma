using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class code_FPSCounter : MonoBehaviour
{
    [SerializeField] TMP_Text fpsCounter;

    int lastFrameIndex;
    float[] frameDeltaTimeArray;

    void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        fpsCounter.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    float CalculateFPS()
    {
        float total = 0f;
        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }

        return frameDeltaTimeArray.Length / total;
    }
}
