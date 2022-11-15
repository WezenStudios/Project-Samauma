using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownController2D
{
    public class code_EndCutscene : MonoBehaviour
    {
        TopDownController2D topDownInput;

        void Awake()
        {
            topDownInput = new TopDownController2D();
        }

        private void Start()
        {
            topDownInput.Player.Escape.performed += ctx => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); };
        }

        void OnEnable()
        {
            topDownInput.Enable();
        }

        void OnDisable()
        {
            topDownInput.Disable();
        }

        public void EndCutscene(int SceneID)
        {
            SceneManager.LoadScene(SceneID);
        }

        public void EndFCutscene()
        {
            Application.Quit();
        }
    }
}
