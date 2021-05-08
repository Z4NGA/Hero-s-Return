using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manualSpitter : MonoBehaviour, in_trap
{
    [SerializeField] private int damage=25;
    [SerializeField] private bool is_on=false;
    [SerializeField] private float firerate = 5;
    [SerializeField] private float bulletForce = 5;
    [SerializeField] private bool direction_left = true;
    private float lastFired=0;
    public GameObject bulletPref;
    private Transform firePoint;
    private void Awake()
    {
        firePoint = transform.Find("firePoint");
    }

    private void Update()
    { 
        Vector3 fixed_position;

        if(direction_left)
            fixed_position = transform.position + new Vector3(-2*bulletForce,0 );
        else 
            fixed_position = transform.position + new Vector3(2*bulletForce, 0);

        if ((Time.time > lastFired + (1 / firerate)) && is_on)
        {
            soundEngine.play_sound(soundEngine.sound_type.dragon_shoot, 0.05f);
            GameObject o = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
            o.GetComponent<Rigidbody2D>().AddForce((fixed_position - firePoint.transform.position) , ForceMode2D.Impulse);
            lastFired = Time.time;
        }
    }

    //turn off 
    public void turn_off()
    {
        is_on = false;
        lastFired = 0;
    }
    //turn trap on
    public void turn_on()
    {
        is_on = true;
        lastFired = Time.time;
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
