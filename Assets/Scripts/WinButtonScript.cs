using UnityEngine;
using UnityEngine.SceneManagement;

public class WinButtonScript : MonoBehaviour
{

    public bool RedirectToBonus = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (RedirectToBonus)
                SceneManager.LoadScene("LevelBonus");
            else
                SceneManager.LoadScene("StageComplete");
        }
    }
}
