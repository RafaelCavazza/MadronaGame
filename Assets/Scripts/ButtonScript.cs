using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        else
            SceneManager.LoadScene(scene);
    }
}
