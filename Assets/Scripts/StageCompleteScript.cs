using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageCompleteScript : MonoBehaviour
{
    public Text score;

    // Use this for initialization
    void Start()
    {
        score.text = "SCORE: "+ PlayerPrefs.GetInt("Score");
    }
}
