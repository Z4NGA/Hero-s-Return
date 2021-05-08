using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class shop_ui : MonoBehaviour
{
    public Player pl;
    private int current_page;
    private int num_pages=3;
    private Transform[] page_containers;
    private Transform currency;
    private TextMeshProUGUI currency_text;
    private Transform itemTemp;
    public Sprite tst;
    private void Awake()
    {
        page_containers = new Transform[4];
        current_page = 1;
        page_containers[1] = transform.Find("page1_container");
        page_containers[2] = transform.Find("page2_container");
        page_containers[3] = transform.Find("page3_container");
        currency = transform.Find("currency");
        currency_text = currency.Find("quantity").GetComponent<TextMeshProUGUI>();
        itemTemp = page_containers[1].Find("ShopItemTemplate");
        page_containers[2].gameObject.SetActive(false);
        page_containers[3].gameObject.SetActive(false);
        itemTemp.gameObject.SetActive(false);
    }
    private void Start()
    {
        currency_text.SetText(Player.coins.ToString());
        
        createShopItem(Items.Item_type.armor_lvl1, Items.getName(Items.Item_type.armor_lvl1), Items.getSprite(Items.Item_type.armor_lvl1), Items.getPrice(Items.Item_type.armor_lvl1), 1, 0);
        createShopItem(Items.Item_type.armor_lvl2, Items.getName(Items.Item_type.armor_lvl2), Items.getSprite(Items.Item_type.armor_lvl2), Items.getPrice(Items.Item_type.armor_lvl2), 1, 1);
        createShopItem(Items.Item_type.helmet_lvl1, Items.getName(Items.Item_type.helmet_lvl1), Items.getSprite(Items.Item_type.helmet_lvl1), Items.getPrice(Items.Item_type.helmet_lvl1), 1, 2);
        createShopItem(Items.Item_type.helmet_lvl2, Items.getName(Items.Item_type.helmet_lvl2), Items.getSprite(Items.Item_type.helmet_lvl2), Items.getPrice(Items.Item_type.helmet_lvl2), 1, 3);

        createShopItem(Items.Item_type.health_potion, Items.getName(Items.Item_type.health_potion), Items.getSprite(Items.Item_type.health_potion), Items.getPrice(Items.Item_type.health_potion), 2, 0);
        createShopItem(Items.Item_type.damage_potion, Items.getName(Items.Item_type.damage_potion), Items.getSprite(Items.Item_type.damage_potion), Items.getPrice(Items.Item_type.damage_potion), 2, 1);
        createShopItem(Items.Item_type.speed_potion, Items.getName(Items.Item_type.speed_potion), Items.getSprite(Items.Item_type.speed_potion), Items.getPrice(Items.Item_type.speed_potion), 2, 2);
        createShopItem(Items.Item_type.armor_lvl1, "God", Items.getSprite(Items.Item_type.armor_lvl2), 9999, 2, 3);

    }
    void createShopItem(Items.Item_type type , string name,Sprite sp ,int price , int pagelocation ,int posinpage) //creates an item on the specified location
    {
        
        float item_width = 220;
        Transform newitem = Instantiate(itemTemp, page_containers[pagelocation]);
        RectTransform newitem_rect = newitem.GetComponent<RectTransform>();
        newitem_rect.anchoredPosition = new Vector2(-300 +item_width * posinpage, 0);
        newitem.Find("item_icon").GetComponent<Image>().sprite = sp;
        newitem.Find("item_price").GetComponent<TextMeshProUGUI>().SetText(price.ToString());
        newitem.Find("item_name").GetComponent<TextMeshProUGUI>().SetText(name);
        newitem.gameObject.SetActive(true);
        newitem.GetComponent<Button>().onClick.AddListener(() => BuyItem(type));
    }
    public void loadNextPage()
    {
        page_containers[current_page].gameObject.SetActive(false);
        current_page = current_page<num_pages ? current_page+1 : 1;
        page_containers[current_page].gameObject.SetActive(true);
    }
    public void loadPreviousPage()
    {
        page_containers[current_page].gameObject.SetActive(false);
        current_page = current_page > 1 ? current_page - 1 : num_pages;
        page_containers[current_page].gameObject.SetActive(true);
    }
    public void BuyItem(Items.Item_type i)
    {
        Debug.Log("trying to buy " + i.ToString());
        /* if (i == Items.Item_type.armor_lvl2)
             if (pl.getArmor() != 1) Debug.LogError("Please buy Armor level 1 First !!");

         else if (i == Items.Item_type.helmet_lvl2)
             if (pl.getHelmet() != 1) Debug.LogError("Please buy Helmet level 1 First !!");
         else*/
        if (pl.canAfford(Items.getPrice(i))){
            //can afford the item
            if (pl.boughtItem(i))//can buy the item
            {
                soundEngine.play_sound(soundEngine.sound_type.buy);
                pl.spendCoins(Items.getPrice(i));
                currency_text.SetText(Player.coins.ToString());
            }
        }
    }
    public void update_currency()
    {
        currency_text.SetText(Player.coins.ToString());
    }
}
