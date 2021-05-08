using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_up : MonoBehaviour
{
    /*private Transform icon;
    private Color img_color;
    private float fade = 0.01f;
    private float tempscale = 0f;
    private bool fade_in = true;
    private void Awake()
    {
        icon = transform.Find("icon");
        img_color = icon.GetComponent<Image>().color;
    }
    private void Update()
    {
        if (fade_in)
        {
            img_color.a += fade;
            tempscale += 1.7f * Time.deltaTime;
        }
        else
        {
            img_color.a -= fade;
            tempscale -= 1.7f * Time.deltaTime;
        }
        if (tempscale > 1) fade_in = !fade_in;
        icon.GetComponent<Image>().color = img_color;
        icon.localScale = new Vector3(tempscale, tempscale);
        Destroy(gameObject, 2f);
    }*/
  
    private void Start()
    {
        GameObject o = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        o.name = gameObject.name;
        o.SetActive(false);
        Destroy(gameObject, 1.5f);


    }
}
    