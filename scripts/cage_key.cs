using UnityEngine;

public class cage_key : MonoBehaviour
{

    [SerializeField] private GameObject cage = null;
    [SerializeField] private bool open = true;
    [SerializeField] private GameObject npc = null;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            if (open)
            {
                soundEngine.play_sound(soundEngine.sound_type.door_open);
                if (cage != null)
                {
                    npc.GetComponent<npc>().free_npc();
                    cage.transform.Find("on").gameObject.SetActive(false);//setting first child off , hidden door on
                    cage.transform.Find("off").gameObject.SetActive(true);//setting second child on , hidden door off
                }
                Destroy(gameObject);
            }
            else
            {
                soundEngine.play_sound(soundEngine.sound_type.door_closed);
                if (cage != null)
                {
                    cage.transform.Find("on").gameObject.SetActive(true);//setting first child off , hidden door on
                    cage.transform.Find("off").gameObject.SetActive(false);//setting second child on , hidden door off
                }
                Destroy(gameObject);
            }
        }
    }
}
