using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float bulletForce = 20f;
    public GameObject firePoint;
    public GameObject bulletPref;
    private Animator anim;
    [SerializeField] private float attack_duration = 0.3f;
    private float last_attack = 0;
    private bool is_attacking = false;
    //private bool has_ranged_weapon = false;
    Vector3 mousepos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("x pos :" +mousepos.x+ "y pos : "+ mousepos.y);
        if (is_attacking)
        {
            if (Time.time > last_attack + attack_duration)
            {
                is_attacking = false;
                anim.SetBool("isAttacking", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!is_attacking)
            {
            last_attack = Time.time;
            anim.SetBool("isAttacking", true);
            is_attacking = true;
            shoot();
            }
        }
  
    }
    void shoot()
    {
        soundEngine.play_sound(soundEngine.sound_type.player_shoot,0.3f);
        Vector3 bullet_rotation = mousepos - transform.position;
        float angle = Mathf.Atan2(bullet_rotation.y, bullet_rotation.x) * Mathf.Rad2Deg;
        GameObject o = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
        o.GetComponent<Rigidbody2D>().SetRotation(angle);
        o.GetComponent<Rigidbody2D>().AddForce((mousepos-transform.position).normalized * bulletForce, ForceMode2D.Impulse);

    }
}
