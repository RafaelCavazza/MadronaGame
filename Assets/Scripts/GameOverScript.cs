using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 40, 200, 60, 30), "Repetir?"))
            SceneManager.LoadScene("Main");
    }
}
