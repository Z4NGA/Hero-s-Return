using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventory_ui : MonoBehaviour
{
    public bool is_active;
    public int items_in_inventory;
    private Transform selected_item;
    private Transform inventory_container, stats_container, potion_container;
    private Transform inventory, item_template,item_action;
    //different inventory actions
    private Transform sell_action, use_action, drop_action;
    private Transform success_text, fail_text;
    //potions 
    private Transform hp, dmg, spd;
    private Transform hp_pot_quantity, dmg_pot_quantity, spd_pot_quantity;
    //stats
    private Transform hp_current, hp_initial, shield_current, shield_initial, speed_current, speed_initial,
        damage_current, damage_initial, xp_current, xp_initial, coins, gems,level_current;
    private void Awake()
    {
        inventory_container = transform.Find("inventory_container");
        stats_container = transform.Find("stats_container");
        potion_container = transform.Find("potion_container");
        inventory = inventory_container.Find("inventory");
        //item actions
        item_action = inventory_container.Find("item_action");
        sell_action = item_action.Find("sell_action");
        use_action = item_action.Find("use_action");
        drop_action = item_action.Find("drop_action");
        fail_text = inventory_container.Find("fail_text");
        success_text = inventory_container.Find("success_text");
        //fail_text.gameObject.SetActive(false);
        //success_text.gameObject.SetActive(false);
        //potions
        hp = potion_container.Find("hp");
        dmg = potion_container.Find("damage");
        spd = potion_container.Find("speed");
        hp_pot_quantity = hp.Find("item_quantity");
        dmg_pot_quantity = dmg.Find("item_quantity");
        spd_pot_quantity = spd.Find("item_quantity");
        hp.GetComponent<Button>().onClick.AddListener(() => Player.use_item(Items.Item_type.health_potion));
        dmg.GetComponent<Button>().onClick.AddListener(() => Player.use_item(Items.Item_type.damage_potion));
        spd.GetComponent<Button>().onClick.AddListener(() => Player.use_item(Items.Item_type.speed_potion));
        //stats
        hp_current = stats_container.Find("health").Find("stat_values").Find("current_value");
        hp_initial = stats_container.Find("health").Find("stat_values").Find("initial_value");
        shield_current = stats_container.Find("shield").Find("stat_values").Find("current_value");
        shield_initial = stats_container.Find("shield").Find("stat_values").Find("initial_value");
        speed_current = stats_container.Find("speed").Find("stat_values").Find("current_value");
        speed_initial = stats_container.Find("speed").Find("stat_values").Find("initial_value");
        damage_current = stats_container.Find("damage").Find("stat_values").Find("current_value");
        damage_initial = stats_container.Find("damage").Find("stat_values").Find("initial_value");
        xp_current = stats_container.Find("xp").Find("stat_values").Find("current_value");
        xp_initial = stats_container.Find("xp").Find("stat_values").Find("initial_value");
        coins = stats_container.Find("coins").Find("coin_amount").Find("current_value");
        gems = stats_container.Find("gems").Find("gem_amount").Find("current_value");
        level_current = stats_container.Find("level").Find("current_value");
        item_template = inventory.Find("item_template");
        item_template.gameObject.SetActive(false);
        //setting item actions before deactivating th opbject
        sell_action.GetComponent<Button>().onClick.AddListener(() => { action_on_item("sell", selected_item); item_action.gameObject.SetActive(false); });
        use_action.GetComponent<Button>().onClick.AddListener(() => {action_on_item("use", selected_item); item_action.gameObject.SetActive(false); });
        drop_action.GetComponent<Button>().onClick.AddListener(() => {action_on_item("drop", selected_item); item_action.gameObject.SetActive(false); });
        item_action.gameObject.SetActive(false);
        is_active = false;
        gameObject.SetActive(is_active);
        GetComponent<CanvasScaler>().scaleFactor = 1;//starts active with scale 0 ,then when awake goes to unactive wih scale 1
        items_in_inventory = 0;
    }
    private void Start()
    {


        update_stats();
    }
    private void Update()
    {
        if (Player.inventory_changes)
        {
            update_stats();
            Player.inventory_changes = false;
        }
    }
    public void update_stats()
    {
        hp_pot_quantity.GetComponent<TextMeshProUGUI>().SetText(Player.num_health_pot.ToString());
        dmg_pot_quantity.GetComponent<TextMeshProUGUI>().SetText(Player.num_damage_pot.ToString());
        spd_pot_quantity.GetComponent<TextMeshProUGUI>().SetText(Player.num_speed_pot.ToString());
        //stats
        hp_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_health.ToString());
        hp_initial.GetComponent<TextMeshProUGUI>().SetText(Player.initial_health.ToString());
        shield_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_armor.ToString());
        shield_initial.GetComponent<TextMeshProUGUI>().SetText(Player.initial_armor.ToString());
        speed_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_speed.ToString());
        speed_initial.GetComponent<TextMeshProUGUI>().SetText(Player.initial_speed.ToString());
        damage_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_damage.ToString());
        damage_initial.GetComponent<TextMeshProUGUI>().SetText(Player.initial_damage.ToString());
        xp_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_xp.ToString());
        xp_initial.GetComponent<TextMeshProUGUI>().SetText(Player.current_lvl_xp().ToString());
        coins.GetComponent<TextMeshProUGUI>().SetText(Player.coins.ToString());
        gems.GetComponent<TextMeshProUGUI>().SetText(Player.gems.ToString());
        level_current.GetComponent<TextMeshProUGUI>().SetText(Player.current_level.ToString());
    }
    public void add_existing_pickable_item(string type, int amount)
    {
        Transform temp_item = inventory.Find(type);
        int new_amount = int.Parse(temp_item.Find("item_quantity").GetComponent<TextMeshProUGUI>().text) + amount;
        temp_item.Find("item_quantity").GetComponent<TextMeshProUGUI>().SetText(new_amount.ToString());
    }
    public void add_pickable_item(string type,int amount ,string description)
    {
        Transform temp_item = Instantiate(item_template,inventory);
        temp_item.GetComponent<RectTransform>().anchoredPosition = new Vector2((items_in_inventory%4) * 150, (items_in_inventory/4) * -150);
        temp_item.name = type;
        temp_item.Find("item_icon").GetComponent<Image>().sprite = Items.getSprite(Items.get_type(type));
        temp_item.Find("item_quantity").GetComponent<TextMeshProUGUI>().SetText(amount.ToString());
        temp_item.Find("item_description").GetComponent<TextMeshProUGUI>().SetText(description);
        Color tempcl;
        switch (Items.get_rarity(Items.get_type(type)))
        {   
            case Items.Item_rarity.common:
                tempcl = Color.white; tempcl.a = 0.25f;
                temp_item.Find("item_bg").GetComponent<Image>().color = tempcl;
                break;
            case Items.Item_rarity.rare:
                tempcl = Color.green; tempcl.a = 0.25f;
                temp_item.Find("item_bg").GetComponent<Image>().color = tempcl;
                break;
            case Items.Item_rarity.epic:
                tempcl = Color.magenta; tempcl.a = 0.25f;
                temp_item.Find("item_bg").GetComponent<Image>().color = tempcl;
                break;
            case Items.Item_rarity.legendary:
                tempcl = Color.yellow; tempcl.a = 0.25f;
                temp_item.Find("item_bg").GetComponent<Image>().color = tempcl;
                break;
        }
        temp_item.GetComponent<Button>().onClick.AddListener(()=> { selected_item = temp_item; item_action.gameObject.SetActive(!item_action.gameObject.activeSelf); });
        temp_item.gameObject.SetActive(true);
        items_in_inventory++;
    }
    private void action_on_item(string action,Transform item)
    {
        switch(action)
        {
            case "sell":
                if (Player.in_shop_range)
                {
                    //popup_text.popup_sell("Congrats you sold an item !!!", item.position, true,item.parent.parent);
                    soundEngine.play_sound(soundEngine.sound_type.sell);
                    Transform temp_s = Instantiate(success_text, item.parent.position, Quaternion.identity);
                    temp_s.gameObject.SetActive(true);
                    Player.sell_item(Items.getPrice(Items.get_type(item.name))*int.Parse(item.Find("item_quantity").GetComponent<TextMeshProUGUI>().text));
                    update_stats();
                    Destroy(item.gameObject);
                    items_in_inventory--;
                    rearrange_items(item.name);
                }
                else
                {
                    Transform temp_f= Instantiate(fail_text, item.parent.position, Quaternion.identity);
                    temp_f.gameObject.SetActive(true);
                    //popup_text.popup_sell("Error !! Get into shop range to sell items !!!", item.position, false,item.parent.parent);
                }
                break;
            case "use":
                if(Items.is_usable(Items.get_type(item.name)))
                {
                    if (Player.use_item(Items.get_type(item.name)))
                    {
                        Transform temp_s = Instantiate(success_text, item.parent.position, Quaternion.identity);
                        temp_s.GetComponent<TextMeshPro>().SetText("Item has been used successfully !");
                        temp_s.GetComponent<TextMeshPro>().color = Color.blue;
                        temp_s.gameObject.SetActive(true);
                        int new_quant = int.Parse(item.Find("item_quantity").GetComponent<TextMeshProUGUI>().text) - 1;
                        if (new_quant > 0)
                            item.Find("item_quantity").GetComponent<TextMeshProUGUI>().SetText(new_quant.ToString());
                        else
                        {
                            Destroy(item.gameObject);
                            items_in_inventory--;
                            rearrange_items(item.name);
                        }
                        update_stats();
                        break;
                    }
                    else
                    {
                        Transform temp_f = Instantiate(fail_text, item.parent.position, Quaternion.identity);
                        temp_f.GetComponent<TextMeshPro>().SetText("Try to use this item later !!");
                        temp_f.gameObject.SetActive(true);
                        break;
                    }
                }
                else {
                    Transform temp_f = Instantiate(fail_text, item.parent.position, Quaternion.identity);
                    temp_f.GetComponent<TextMeshPro>().SetText("This object can't be used !! Please sell it !!");
                    temp_f.gameObject.SetActive(true);
                    break; 
                }
                
            case "drop":
                //doesn't drop to the map, kinda of dropping out the bag and destroying the item
                soundEngine.play_sound(soundEngine.sound_type.drop_item);
                Destroy(item.gameObject);
                items_in_inventory--;
                rearrange_items(item.name);
                break;
            default:
                break;
        }
    }
    private void rearrange_items(string removed_child_name)//rearranges inventory weapons after removing or selling an item
    {
        int inv_count = 0;
        for(int i=1;i< inventory.childCount; i++)
        {
            if (!inventory.GetChild(i).name.Equals(removed_child_name)){
                inventory.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2((inv_count % 4) * 150, (inv_count / 4) * -150);
                Debug.Log(inventory.GetChild(i).name + " is being moved ");
                inv_count++;
            }
        }
    }
}
