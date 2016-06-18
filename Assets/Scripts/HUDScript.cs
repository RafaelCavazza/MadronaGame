using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public static float playerScrore;
    public Text text;

    // Use this for initialization
    void Start()
    {
        playerScrore = 60;
    }

    // Update is called once per frame
    void Update()
    {
        playerScrore -= Time.deltaTime;
        text.text = "SCORE: " + (int)playerScrore;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)playerScrore);
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
    }

}
