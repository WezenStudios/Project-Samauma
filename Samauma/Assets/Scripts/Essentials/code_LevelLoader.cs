using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class code_LevelLoader : MonoBehaviour
{
    // Variav�is;
    [SerializeField] float transitionTime;
    Animator transition;

    // Fun��o 'Start', � executada quando se inicia o projeto;
    void Start()
    {
        // Pega as propriedades do componente 'Animator' no objeto filho;
        transition = GetComponentInChildren<Animator>();
    }

    // Carrega a cena desejada e inicia a transi��o;
    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    // Realiza a transi��o das anima��es;
    IEnumerator LoadLevel(int index)
    {
        if(transition != null)
        {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(index);
        }
        else
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(index);
        }

        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    /*public void CompanyWeb()
    {
        Application.OpenURL("");
    }*/
}