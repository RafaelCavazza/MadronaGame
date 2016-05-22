using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public string SceneToLoad = ""; 
    private bool _loaded;

    void OnMouseUp()
    {
        if (!string.IsNullOrEmpty(SceneToLoad) && !_loaded)
        {
            _loaded = true;
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
