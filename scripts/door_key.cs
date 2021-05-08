using UnityEngine;

public class door_key : MonoBehaviour
{

    [SerializeField] private GameObject[] doors=null;
    [SerializeField] private bool open = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            if (open)
            {
                soundEngine.play_sound(soundEngine.sound_type.door_open);
                if (doors != null)
                {
                    foreach (GameObject hidden_door in doors)
                    {
                        hidden_door.transform.Find("on").gameObject.SetActive(false);//setting first child off , hidden door on
                        hidden_door.transform.Find("off").gameObject.SetActive(true);//setting second child on , hidden door off
                        hidden_door.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                soundEngine.play_sound(soundEngine.sound_type.door_closed);
                if (doors != null)
                {
                    foreach (GameObject hidden_door in doors)
                    {
                        hidden_door.transform.Find("on").gameObject.SetActive(true);//setting first child off , hidden door on
                        hidden_door.transform.Find("off").gameObject.SetActive(false);//setting second child on , hidden door off
                        hidden_door.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
