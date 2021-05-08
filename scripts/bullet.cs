using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject explo;
    [SerializeField] private int bulletDamage = 0;
    [SerializeField] private bool player_bullet = false;
    private void Awake()
    {
        if (player_bullet) bulletDamage = (int)Player.current_damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        soundEngine.play_3d_sound(soundEngine.sound_type.energy_explosion,collision.collider.transform.position);
        GameObject exp = Instantiate(explo, gameObject.transform.position,Quaternion.identity);
        Destroy(exp, 1f);
        Destroy(gameObject);
        in_targetable targetable_obj = collision.collider.GetComponent<in_targetable>();
        if (targetable_obj != null)
        {
            if(player_bullet)
                popup_damage.popup(bulletDamage, collision.contacts[0].point, false);
            else
                popup_damage.popup(bulletDamage, collision.contacts[0].point, true);
            targetable_obj.take_damage(bulletDamage);
        }
        //add inteface to boss,helmet and orb later on and remove this later

		if (collision.transform.GetComponent<Boss>() != null)
		{
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<Boss>().TakeDamage(bulletDamage, true);
		}
        if (collision.transform.GetComponent<SingleHelmet>() != null)
        {
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<SingleHelmet>().TakeDamage(bulletDamage);
        }
        if (collision.transform.GetComponent<ChargeLightningOrb>() != null)
        {
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<ChargeLightningOrb>().TakeDamage(bulletDamage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        soundEngine.play_3d_sound(soundEngine.sound_type.energy_explosion, collision.transform.position);
        GameObject exp = Instantiate(explo, gameObject.transform.position, Quaternion.identity);
        Destroy(exp, 1f);
        Destroy(gameObject);
        in_targetable targetable_obj = collision.transform.GetComponent<in_targetable>();

        if (targetable_obj != null)
        {
            if(!player_bullet)
                popup_damage.popup(bulletDamage, collision.transform.position, true);
            else
                popup_damage.popup(bulletDamage, collision.transform.position, false);
            targetable_obj.take_damage(bulletDamage);
        }
        if (collision.transform.GetComponent<Boss>() != null)
        {
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<Boss>().TakeDamage(bulletDamage, true);
        }
        if (collision.transform.GetComponent<SingleHelmet>() != null)
        {
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<SingleHelmet>().TakeDamage(bulletDamage);
        }
        if (collision.transform.GetComponent<ChargeLightningOrb>() != null)
        {
            popup_damage.popup(bulletDamage, collision.transform.position, false);
            collision.transform.GetComponent<ChargeLightningOrb>().TakeDamage(bulletDamage);
        }
    }
    private void Update()
    {
       if (transform.position.x > 600 || transform.position.x < -200 || transform.position.y > 600 || transform.position.y < -200)
        {
            Destroy(gameObject); // out of bounds
        }
    }
}
