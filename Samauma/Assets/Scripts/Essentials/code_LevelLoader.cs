using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class code_LevelLoader : MonoBehaviour
{
    // Variavéis;
    [SerializeField] float transitionTime;
    Animator transition;

    // Função 'Start', é executada quando se inicia o projeto;
    void Start()
    {
        // Pega as propriedades do componente 'Animator' no objeto filho;
        transition = GetComponentInChildren<Animator>();
    }

    // Carrega a cena desejada e inicia a transição;
    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    // Realiza a transição das animações;
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