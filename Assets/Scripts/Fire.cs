using UnityEngine;
using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour
{

    public float UpForce = 0.08F;
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if (player.Pow)
            transform.position += Vector3.up * UpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
            SceneManager.LoadScene("GameOver");
        else if (other.tag.Equals("Barrel"))
        {
            WoodenBarrel barril = other.GetComponent<WoodenBarrel>();
            if (barril.ItHasPlayer)
                SceneManager.LoadScene("GameOver");
            else
                Destroy(other.gameObject);
        }
    }
}
