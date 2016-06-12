using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        switch (scene)
        {
            case "Retry":
                SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
                break;
            case "Done":
                int level = int.Parse(PlayerPrefs.GetString("Level").Substring(5));
                if (level < 10)
                    SceneManager.LoadScene("Level" + (level + 1));
                else
                    SceneManager.LoadScene("Menu");
                break;
            default:
                SceneManager.LoadScene(scene);
                break;
        }
    }
}
