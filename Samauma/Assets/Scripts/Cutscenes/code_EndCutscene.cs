using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class code_EndCutscene : MonoBehaviour
{
    public void EndCutscene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
