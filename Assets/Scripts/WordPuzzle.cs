using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordPuzzle : MonoBehaviour
{
    public TMP_InputField InputField;
    public GameObject Text;

    public void OnAnswered()
    {
        if (InputField.text.ToLower() == "ten")
        {
            Text.SetActive(true);
        }
    }
}
