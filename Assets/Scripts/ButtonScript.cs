using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        switch (scene)
        {
            case "Retry":
                if (PlayerPrefs.GetString("Level") == "LevelBonus")
                    SceneManager.LoadScene("Level10");
                else
                    SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
                break;
            case "RetryPause":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "Done":
                if (PlayerPrefs.GetString("Level") == "LevelBonus")
                    SceneManager.LoadScene("Creditos");
                else
                {
                    int level = int.Parse(PlayerPrefs.GetString("Level").Substring(5));
                    if (level < 10)
                        SceneManager.LoadScene("Level" + (level + 1));
                    else
                        SceneManager.LoadScene("Creditos");
                }
                break;
            default:
                SceneManager.LoadScene(scene);
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
