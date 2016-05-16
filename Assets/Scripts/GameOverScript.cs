using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 350, 100, 40), "Repetir?"))
            SceneManager.LoadScene("Main");
    }
}
