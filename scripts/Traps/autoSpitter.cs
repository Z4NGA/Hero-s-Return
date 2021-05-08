using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSpitter : MonoBehaviour, in_trap
{
    [SerializeField] private int damage = 50;
    [SerializeField] private bool is_on = false;
    [SerializeField] private float firerate = 5;
    [SerializeField] private float bulletForce = 5;
    [SerializeField] private bool direction_left = true;
    [SerializeField] private bool top_down = false;
    [SerializeField] private float cool_down_interval = 2;
    private float last_cd = 0;
    private bool cd_on = false;
    private float last_fired = 0;
    public GameObject bulletPref;
    private Transform firePoint;
    private void Awake()
    {
        firePoint = transform.Find("firePoint");
    }

    private void Update()
    {
        Vector3 fixed_position;

        if (direction_left)
            fixed_position = transform.position + new Vector3(-2 * bulletForce, 0);
        else if(top_down)
            fixed_position = transform.position + new Vector3(0, -2 * bulletForce);
        else
            fixed_position = transform.position + new Vector3(2 * bulletForce, 0);

        if (Time.time > last_cd + cool_down_interval)
        {
            switch_cd();
        }

        if ((Time.time > last_fired + (1 / firerate)) && is_on && !cd_on)
        {
            soundEngine.play_3d_sound(soundEngine.sound_type.dragon_shoot,transform.position);
            GameObject o = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
            o.GetComponent<Rigidbody2D>().AddForce((fixed_position - firePoint.transform.position), ForceMode2D.Impulse);
            last_fired = Time.time;
        }
        
    }
    public void switch_cd()
    {
        cd_on = !cd_on;
        last_fired = Time.time;
        last_cd = Time.time;
    }
    //turn off 
    public void turn_off()
    {
        is_on = false;
        last_fired = Time.time;
        last_cd = Time.time;
    }
    //turn trap on
    public void turn_on()
    {
        is_on = true;
        last_fired = Time.time;
        last_cd = Time.time;
    }
    //returns damage according to status
    public int get_damage()
    {
        if (is_on) return damage;
        return 0;
    }
    public bool get_status()
    {
        return is_on;
    }
}
