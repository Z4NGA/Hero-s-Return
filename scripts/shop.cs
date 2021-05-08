using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shop : MonoBehaviour
{
    public  GameObject shop_gui;
    private Collider2D player=null; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null) //only interacts with players
        {
            player = collision.GetComponent<Collider2D>();
            Transform txt = Instantiate(itemAssets.instance.popup_text, new Vector3(75,46.5f),Quaternion.identity);
            Player.in_shop_range = true;
            Debug.Log(Player.in_shop_range);
            txt.GetComponent<TextMeshPro>().SetText("press b to enter/exit the shop");
            Destroy(txt.gameObject, 3f);

            Debug.Log("press b to enter/exit the shop :");
 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player.in_shop_range = false;
            Debug.Log(Player.in_shop_range);
        }
    }
    private void Update()
    {
        
       if(player !=null&& (gameObject.GetComponent<BoxCollider2D>().IsTouching(player)))
       {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!shop_gui.activeSelf)
                {
                    shop_gui.SetActive(true);
                    if(shop_gui.GetComponent<shop_ui>()!=null) shop_gui.GetComponent<shop_ui>().update_currency();
                }
                else shop_gui.SetActive(false);
            }
       }
    }
}
