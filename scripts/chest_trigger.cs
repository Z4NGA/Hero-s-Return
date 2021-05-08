using UnityEngine;

public class chest_trigger : MonoBehaviour
{
    private bool is_open;
    private Animator anim;
    [SerializeField] private int reward=0;
    [SerializeField] private bool is_gem=false;
    private Player pl; 
    private string type; 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        is_open = false;
        type = is_gem ? "Gems" : "Gold";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()!=null)
        {
            pl = collision.GetComponent<Player>(); 
            if (!is_open)
            {
                if (!is_gem) soundEngine.play_sound(soundEngine.sound_type.coin_chest);
                if (anim != null) anim.SetBool("Open", true);
                is_open = true;
                Debug.Log("Congrats, you opened a chest ! you got " + reward + " " + type);
                rewardPlayer();
            }
            transform.Find("particles").GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 3);
        }
    }
    //still need fts to explode chest into crystals or money 
    private void rewardPlayer()
    {
        if(pl!=null)
        {   
            string temp="+ " + reward + " " + type;
            if (is_gem)
                pl.add_gems(reward);
            else 
                pl.add_coins(reward);
            popup_text.popup(temp, transform.position, !is_gem);
        }
    }
}
