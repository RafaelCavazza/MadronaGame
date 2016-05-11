using UnityEngine;
using System.Collections;

public class StageCompleteScript : MonoBehaviour
{
    int score;

    // Use this for initialization
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 -40, 150, 80, 30), "Parabéns !");
        GUI.Label(new Rect(Screen.width / 2 -40, 200, 80, 30), "Score: " + score);
    }
}
