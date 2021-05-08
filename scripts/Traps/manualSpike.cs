using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manualSpike : MonoBehaviour, in_trap
{
    private int damage;
    private bool is_on;
    private Animator anim;
    private in_targetable collider_obj;
    private float damage_cd;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        damage = 25;//less damage given it's always on
        is_on = true;
        anim.SetBool("Active", true);
        damage_cd = 1.5f;
    }
    
    //turn off 
    public void turn_off()
    {
        is_on = false;
        if(anim!=null) anim.SetBool("Active", false);
        soundEngine.play_3d_sound(soundEngine.sound_type.trap_off, transform.position);
    }
    //turn trap on
    public void turn_on()
    {
        is_on = true;
        if (anim != null) anim.SetBool("Active", true);
        soundEngine.play_3d_sound(soundEngine.sound_type.trap_on, transform.position);
    }
    //returns damage according to status
    public int get_damage()
    {
        if (is_on) return damage;
        return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.GetComponent<Transform>().gameObject.name+ " is detected !!! ");
        collider_obj = collision.GetComponent<in_targetable>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<in_targetable>()!=null)
            collider_obj = null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<in_targetable>() != null)
        {
            if (is_on)
            {
                damage_cd -= 0.1f;
                if (damage_cd < 0)
                {
                    if (collision.GetComponent<in_targetable>() != null)
                    {
                        popup_damage.popup(get_damage(), transform.position, true);
                        collision.GetComponent<in_targetable>().take_damage(get_damage());
                    }
                    damage_cd = 1;
                }
            }
        }
    }
    public bool get_status()
    {
        return is_on;
    }
}
