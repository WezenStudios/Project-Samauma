using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownController2D
{
    public class code_CloseBook : MonoBehaviour
    {
        TopDownController2D topDownInput;
        [SerializeField] Animator anim;

        void Start()
        {
            topDownInput.Player.Escape.performed += ctx => { anim.SetBool("Closed", true); };
            topDownInput.Player.Escape.canceled += ctx => { anim.SetBool("Closed", false); };
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
    }
}