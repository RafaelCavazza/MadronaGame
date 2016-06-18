using UnityEngine;

public class Coin : MonoBehaviour {

    public AudioSource CoinSound;
    public int points = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            HUDScript.playerScrore += points;
            CoinSound.Play();
            DestroyObject(gameObject);
        }
    }
}
