using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : MonoBehaviour,in_targetable  
{
    public GameObject reward; 
    private int health, initial_health;
    private Vector3 fixed_position;
    private Animator anim;
    private bool is_right = true;
    private bool is_dead, is_hurt, is_walking, is_attacking;
    float bulletForce = 2f;
    public GameObject firePoint;
    public GameObject bulletPref;
    private Transform hp_container,bar;
    private float last_time_shot;
    [SerializeField] private float firerate;
    public bool player_in_range;
    public Transform p;
    public bool fixed_shooter = false;
    public int starting_angle = 0;
    public int angle=0;
    public float last_angle_change=0;
    public void take_damage(int dmg)
    {
        if (!is_dead)
        {
            if (health > dmg) { health -= dmg; set_state("is_hurt"); }
            else { 
                health = 0; set_state("is_dead");
                GameObject rwd = Instantiate(reward, transform.position, Quaternion.identity);
                rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(500);
                popup_text.popup_xp("+5000 XP", transform.position);
                Destroy(gameObject, 2f);
            }
            update_stats();
        }
    }
    void update_stats()
    {
        bar.GetComponent<RectTransform>().localScale = new Vector2((float)health / initial_health, 1);
    }
    void Awake()
    {
        hp_container = transform.Find("hp_container");
        bar = hp_container.Find("bar").Find("bar_sprite");
        set_state("idle");
        initial_health = 150; 
        health = initial_health;
        last_time_shot=Time.time;firerate = 3;
        anim = transform.GetComponent<Animator>();
        fixed_position = transform.position +new Vector3(3, 3);
        player_in_range = false;
        angle = starting_angle;
    }
    private void Start()
    {
        update_stats();
        set_animator();
    }
    // Update is called once per frame
    void Update()
    {
        set_animator();
        if(player_in_range)
        {
            if (fixed_shooter) 
            { 
                if (Time.time > last_angle_change + 1/firerate) angle += 15; 
                shoot_to_fixed_position(angle); 
            }
            else shoot(p.position);
        }
    }
    private void FixedUpdate()
    {
        if (!is_dead)
        {
            
        }
    }
    void set_animator()
    {
        transform.GetComponent<Animator>().SetBool("is_dead", is_dead);
        transform.GetComponent<Animator>().SetBool("is_hurt", is_hurt);
        transform.GetComponent<Animator>().SetBool("is_walking", is_walking);
        transform.GetComponent<Animator>().SetBool("is_attacking", is_attacking);
    }
    public void set_state(string state)
    {
        switch (state)
        {
            case "is_dead":
                is_dead = true; is_hurt = false; is_walking = false; is_attacking = false;
                break;
            case "is_hurt":
                is_dead = false; is_hurt = true; is_walking = false; is_attacking = false;
                break;
            case "is_walking":
                is_dead = false; is_hurt = false; is_walking = true; is_attacking = false;
                break;
            case "is_attacking":
                is_dead = false; is_hurt = false; is_walking = false; is_attacking = true;
                break;
            default:
                is_dead = false; is_hurt = false; is_walking = false; is_attacking = false;
                break;
        }
        set_animator();
    }
    void switch_direction()
    {
        is_right = !is_right;
        transform.RotateAroundLocal(new Vector3(0, 1), 180);
    }
    public void shoot(Vector3 target_pos)
    {
        if (Time.time>last_time_shot+1/firerate && !is_dead)
        {
            soundEngine.play_sound(soundEngine.sound_type.dragon_shoot, 0.15f);
            set_state("is_attacking");
            GameObject o = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
            o.GetComponent<Rigidbody2D>().AddForce((target_pos - firePoint.transform.position) * bulletForce, ForceMode2D.Impulse);
            last_time_shot = Time.time;
            // Debug.Log("cd is on");
        }
    }
    public void shoot_to_fixed_position(int angle)
    {
        fixed_position = transform.position + new Vector3(3*Mathf.Cos(Mathf.Deg2Rad * angle), 3*Mathf.Sin(Mathf.Deg2Rad*angle));
        if (Time.time>last_time_shot+1/firerate && !is_dead)
        {
            soundEngine.play_sound(soundEngine.sound_type.dragon_shoot, 0.1f);
            set_state("is_attacking");
            GameObject o = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
            o.GetComponent<Rigidbody2D>().AddForce((fixed_position - firePoint.transform.position) * bulletForce, ForceMode2D.Impulse);
            last_time_shot=Time.time;
            // Debug.Log("cd is on");
        }
    }
}
