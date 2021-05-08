using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class popup_damage : MonoBehaviour
{
    private float fade_counter = 0.5f ;
    private const float fade_counter_max = 0.5f;
    private float fade_aplha_speed = 4f;
    private TextMeshPro popup_mesh ;
    private Color popup_color;
    private Vector3 popup_movepos;
    public static popup_damage popup(float damage, Vector3 position , bool is_enemy)
    {
        Transform popup_transform = Instantiate(itemAssets.instance.damage_prefab, position, Quaternion.identity);
        popup_transform.GetComponent<TextMeshPro>().SetText(damage.ToString());
        popup_damage popup_dmg = popup_transform.GetComponent<popup_damage>();
        if (is_enemy) popup_dmg.make_enemy(popup_transform);
        return popup_dmg; 
    }
    void Awake()
    {
        popup_mesh = transform.GetComponent<TextMeshPro>();
        popup_color = popup_mesh.color;
        popup_movepos = new Vector3(0.2f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=  popup_movepos * Time.deltaTime;  //pop up damage goes bit up and then vanish
        popup_movepos -= 0.1f * popup_movepos; //movement gets slower and comes to a stop
        fade_counter -= Time.deltaTime;
        if (fade_counter > (fade_counter_max / 2f))
            transform.localScale += 1f * Time.deltaTime * new Vector3(1, 1); //gets bigger
        else transform.localScale -= 1f * Time.deltaTime * new Vector3(1, 1);//gets smaller

        if (fade_counter < 0) popup_color.a -=fade_aplha_speed*Time.deltaTime ; //when counter ends ,obj starts fading
        popup_mesh.color = popup_color; //because we can't modify the alpha directly
        if (popup_color.a < 0) Destroy(gameObject); //when fading ends , obj disappear
    }
    public void make_enemy(Transform t)
    {
        popup_color = Color.red;
        popup_mesh.color = popup_color;
        //popup_mesh.fontSize = 9;
    }
}
