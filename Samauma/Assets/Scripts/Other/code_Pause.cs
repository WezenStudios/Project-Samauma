using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TopDownController2D
{
    public class code_Pause : MonoBehaviour
    {
        public static bool isPaused = false;

        TopDownController2D topDownInput;
        code_Gameplay gameplay;

        void Start()
        {
            gameplay = GameObject.Find("Gameplay Manager").GetComponent<code_Gameplay>();
            topDownInput.Player.Escape.performed += ctx => { if (isPaused) { Resumed(); } else { Paused(); }; };
        }

        void Awake()
        {
            topDownInput = new TopDownController2D();
        }

        void OnEnable()
        {
            topDownInput.Enable();
        }

        void OnDisable()
        {
            topDownInput.Disable();
        }

        public void Resumed()
        {
            gameplay.hudPause.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Paused()
        {
            gameplay.hudPause.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void OnLeaveGame()
        {
            Application.Quit();
        }
    }
}