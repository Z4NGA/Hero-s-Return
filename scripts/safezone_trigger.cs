using UnityEngine;

public class safezone_trigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().set_invincible(true);
            Debug.Log("player just entered the safe zone !");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().set_invincible(false);
            Debug.Log("player left the safe zone !");
        }
    }
}
