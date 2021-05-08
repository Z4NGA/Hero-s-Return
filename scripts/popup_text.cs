using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class popup_text : MonoBehaviour
{
    private float fade_counter = 0.5f;
    private const float fade_counter_max = 0.5f;
    private float fade_aplha_speed = 4f;
    private TextMeshPro popup_mesh;
    private Color popup_color;
    private Vector3 popup_movepos;
    public static popup_text popup(string text, Vector3 position, bool is_gold)
    {
        Transform popup_transform = Instantiate(itemAssets.instance.popup_text, position, Quaternion.identity);
        popup_transform.GetComponent<TextMeshPro>().SetText(text);
        popup_text popup_txt = popup_transform.GetComponent<popup_text>();
        if (is_gold) popup_txt.make_gold(popup_transform);
        else popup_txt.make_gems(popup_transform);
        return popup_txt;
    }
    public static popup_text popup_xp(string text, Vector3 position)
    {
        Transform popup_transform = Instantiate(itemAssets.instance.popup_text, position, Quaternion.identity);
        popup_transform.GetComponent<TextMeshPro>().SetText(text);
        popup_text popup_txt = popup_transform.GetComponent<popup_text>();
        popup_txt.make_xp(popup_transform);
        return popup_txt;
    }
    public static popup_text popup_sell(string text, Vector3 position, bool is_sold,Transform parent)
    {
        Transform popup_transform = Instantiate(itemAssets.instance.popup_text,position,Quaternion.identity,parent);
        popup_transform.position = new Vector3(0, 0);
        popup_transform.localScale = new Vector3(200,100);
        popup_transform.GetComponent<TextMeshPro>().SetText(text);
        popup_text popup_txt = popup_transform.GetComponent<popup_text>();
        if (is_sold) popup_txt.make_success(popup_transform);
        else popup_txt.make_fail(popup_transform);
        return popup_txt;
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
        transform.position += popup_movepos * Time.deltaTime;  //pop up damage goes bit up and then vanish
        popup_movepos -= 0.1f * popup_movepos; //movement gets slower and comes to a stop
        fade_counter -= Time.deltaTime;
        if (fade_counter > (fade_counter_max / 2f))
            transform.localScale += 1f * Time.deltaTime * new Vector3(1, 1); //gets bigger
        else transform.localScale -= 1f * Time.deltaTime * new Vector3(1, 1);//gets smaller

        if (fade_counter < 0) popup_color.a -= fade_aplha_speed * Time.deltaTime; //when counter ends ,obj starts fading
        popup_mesh.color = popup_color; //because we can't modify the alpha directly
        if (popup_color.a < 0) Destroy(gameObject); //when fading ends , obj disappear
    }
    public void make_gold(Transform t)
    {
        popup_color = Color.yellow;
        popup_mesh.color = popup_color;
        popup_mesh.fontSize = 9;
    }
    public void make_gems(Transform t)
    {
        popup_color = Color.magenta;
        popup_mesh.color = popup_color;
        popup_mesh.fontSize = 9;
    }
    public void make_xp(Transform t)
    {
        popup_color = Color.cyan;
        popup_mesh.color = popup_color;
        popup_mesh.fontSize = 9;
    }
    public void make_success(Transform t)
    {
        popup_color = Color.green;
        popup_mesh.color = popup_color;
        popup_mesh.fontSize = 7;
    }
    public void make_fail(Transform t)
    {
        popup_color = Color.red;
        popup_mesh.color = popup_color;
        popup_mesh.fontSize = 7;
    }
}
