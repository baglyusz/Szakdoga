using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseRoom : MonoBehaviour
{
    public string SceneName;

    public void OnPathChosen()
    {
        SceneManager.LoadScene(SceneName);
    }
}
