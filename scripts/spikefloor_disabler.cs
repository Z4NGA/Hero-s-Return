using UnityEngine;

public class spikefloor_disabler : MonoBehaviour
{
    public GameObject trap_wall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        soundEngine.play_sound(soundEngine.sound_type.trap_off);
        trap_wall.transform.GetChild(0).gameObject.SetActive(false);//setting first child off , "trap tile on"
        trap_wall.transform.GetChild(1).gameObject.SetActive(true);//setting second child on , "trap tile off"
        Destroy(trap_wall.GetComponent<BoxCollider2D>());
        Destroy(gameObject);
    }
}
