using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public string SceneToLoad = ""; 

    void OnMouseUp()
    {
        if (!string.IsNullOrEmpty(SceneToLoad))
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
