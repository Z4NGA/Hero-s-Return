using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSpike : MonoBehaviour,in_trap
{
    private int damage;
    [SerializeField] public bool is_on=true;
    private bool is_auto;
    private float cd;
    private Animator anim;
    private in_targetable collider_obj;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        damage = 50;
       // is_on = true;
        is_auto = true;
        cd = 1;
    }

    // fixed Update is called on fixed framerate
    void FixedUpdate()
    {
        if (is_auto)
        {
            cd -= 0.01f;
            if (cd < 0)
            {
                is_on = !is_on;
                cd = 1;
                if (anim != null) anim.SetBool("Active", is_on);
                if (is_on) soundEngine.play_3d_sound(soundEngine.sound_type.trap_on, transform.position);
                else soundEngine.play_3d_sound(soundEngine.sound_type.trap_off, transform.position);
                if (is_on)//object gets hit once per status change
                {
                    if (collider_obj != null)
                    {
                        popup_damage.popup(get_damage(), transform.position, true);
                        collider_obj.take_damage(get_damage());
                    }
                }
            }
        }
    }
    //turn off 
    public void turn_off()
    {
        is_on = false;
        is_auto = false; 
        if (anim != null) anim.SetBool("Active", false);
        soundEngine.play_3d_sound(soundEngine.sound_type.trap_off, transform.position);
    }
    //turn trap on
    public void turn_on()
    {
        is_on = true;
        is_auto = true; 
        if (anim != null) anim.SetBool("Active", true);
        soundEngine.play_3d_sound(soundEngine.sound_type.trap_on, transform.position);
        if (collider_obj != null)
        {
            popup_damage.popup(get_damage(), transform.position, true);
            collider_obj.take_damage(get_damage());
        }
    }
    //returns damage according to status
    public int get_damage()
    {
        if (is_on) return damage;
        return 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider_obj= collision.GetComponent<in_targetable>();
        if(collider_obj!=null)
        {
            popup_damage.popup(get_damage(), transform.position, true);
            collider_obj.take_damage(get_damage());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<in_targetable>() != null)
            collider_obj = null;
    }
    public bool get_status()
    {
        return is_on;
    }
}
