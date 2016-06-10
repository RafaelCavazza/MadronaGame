using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused { get; set; }

    public void togglePause()
    {
        //Remove pause
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            paused = false;
        }
        //Pause
        else
        {
            Time.timeScale = 0f;
            paused = true;
        }
    }

}
