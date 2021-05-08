using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_ui : MonoBehaviour
{
    //sprites 
    public Sprite available, in_progress, completed;
    //containers
    private Transform hp_container, shield_container, pot_container, currency_container, quest_container;
    private Transform xp_container;
    private Transform lvl_up_container; 
    //templates
    private Transform hp_bar_sprite,shield_bar_sprite,xp_bar_sprite; 

    private Transform potion_template;
    private Transform coin_template,gem_template;
    private Transform quest_template;
    // Update is called once per frame
    //timer
    float last_lvl_up = 0;
    private int last_hp_pot, last_dmg_pot, last_speed_pot,last_coins,last_gems,last_level;//last registered values
    private void Awake()
    { 
        hp_container = transform.Find("hp_container");
        shield_container = transform.Find("shield_container");
        xp_container = transform.Find("xp_container");
        lvl_up_container = transform.Find("level_up_container");
        pot_container = transform.Find("pot_container");
        currency_container = transform.Find("currency_container");
        quest_container = transform.Find("quest_container");
        quest_template = quest_container.Find("quest_template");
        coin_template = currency_container.Find("coin_template");
        gem_template = currency_container.Find("gem_template");
        potion_template = pot_container.Find("potion_template");
        hp_bar_sprite = hp_container.Find("bar").Find("bar_sprite");
        shield_bar_sprite = shield_container.Find("bar").Find("bar_sprite");
        xp_bar_sprite = xp_container.Find("bar").Find("bar_sprite");
        potion_template.gameObject.SetActive(false);
        quest_template.gameObject.SetActive(false);
    }
    private void Start()
    {
        last_hp_pot =Player.num_health_pot; last_dmg_pot = Player.num_damage_pot; last_speed_pot = Player.num_speed_pot; last_coins = Player.coins;last_gems = Player.gems;
        last_level = Player.current_level;
        display_stats();
        display_potions();
        display_coins();
    }
    
    void Update() //shouldn't be used to redisplay sprites each second , only when damage is registered
    {
        if (lvl_up_container.gameObject.activeSelf)
        {
            if (Time.time > last_lvl_up + 3) lvl_up_container.gameObject.SetActive(false);
        }
        update_potions();
        update_coins();
        update_stats();
    }
    void update_stats()
    {
        //bar update
        hp_bar_sprite.localScale = new Vector3((float)Player.current_health / Player.initial_health, 1);
        xp_bar_sprite.localScale = new Vector3((float)Player.current_xp / Player.current_lvl_xp(), 1);
        //values update
        hp_container.Find("values").Find("initial_value").GetComponent<TextMeshProUGUI>().SetText(Player.initial_health.ToString());
        hp_container.Find("values").Find("current_value").GetComponent<TextMeshProUGUI>().SetText(Player.current_health.ToString());
        shield_container.Find("values").Find("initial_value").GetComponent<TextMeshProUGUI>().SetText(Player.initial_armor.ToString());
        shield_container.Find("values").Find("current_value").GetComponent<TextMeshProUGUI>().SetText(Player.current_armor.ToString());
        xp_container.Find("values").Find("initial_value").GetComponent<TextMeshProUGUI>().SetText(Player.current_lvl_xp().ToString());
        xp_container.Find("values").Find("current_value").GetComponent<TextMeshProUGUI>().SetText(Player.current_xp.ToString());
        if (Player.initial_armor > 0) shield_bar_sprite.localScale = new Vector3((float)Player.current_armor / Player.initial_armor, 1);
        else shield_bar_sprite.localScale = new Vector3(0, 1);
        if (Player.current_level != last_level)
        {
            last_level = Player.current_level;
            last_lvl_up = Time.time;
            lvl_up_container.gameObject.SetActive(true);
        }
    }
    void display_stats()
    {
        hp_bar_sprite.localScale = new Vector3((float)Player.current_health/Player.initial_health, 1);
        xp_bar_sprite.localScale = new Vector3((float)Player.current_xp / Player.current_lvl_xp(), 1);
        if (Player.initial_armor > 0) shield_bar_sprite.localScale = new Vector3((float)Player.current_armor / Player.initial_armor, 1);
        else shield_bar_sprite.localScale = new Vector3(0, 1);
    }
    void display_coins()
    {
        coin_template.Find("coin_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.coins.ToString());
        gem_template.Find("gem_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.gems.ToString());
    }
    void update_coins()
    {
        if (last_coins != Player.coins)
        {
            coin_template.Find("coin_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.coins.ToString());
            last_coins = Player.coins;
        }
        if (last_gems != Player.gems)
        {
            gem_template.Find("gem_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.gems.ToString());
            last_gems = Player.gems;
        }
    }
    void display_buffs()
    {

    }
    void display_potions()
    {
        Transform temp_pot = Instantiate(potion_template, pot_container);
        temp_pot.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        temp_pot.name = "hp_potion";
        temp_pot.Find("potion_icon").GetComponent<Image>().sprite = Items.getSprite(Items.Item_type.health_potion);
        temp_pot.Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_health_pot.ToString());
        temp_pot.gameObject.SetActive(true);
        temp_pot = Instantiate(potion_template, pot_container);
        temp_pot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150, 0);
        temp_pot.name = "dmg_potion";
        temp_pot.Find("potion_icon").GetComponent<Image>().sprite = Items.getSprite(Items.Item_type.damage_potion);
        temp_pot.Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_damage_pot.ToString());
        temp_pot.gameObject.SetActive(true);
        temp_pot = Instantiate(potion_template, pot_container);
        temp_pot.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, 0);
        temp_pot.name = "speed_potion";
        temp_pot.Find("potion_icon").GetComponent<Image>().sprite = Items.getSprite(Items.Item_type.speed_potion);
        temp_pot.Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_speed_pot.ToString());
        temp_pot.gameObject.SetActive(true);
    }
    private void update_potions()
    {
        if (last_hp_pot != Player.num_health_pot)
        {
            pot_container.Find("hp_potion").Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_health_pot.ToString());
            last_hp_pot = Player.num_health_pot;
        }
        if (last_dmg_pot != Player.num_damage_pot)
        {
            pot_container.Find("dmg_potion").Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_damage_pot.ToString());
            last_dmg_pot = Player.num_damage_pot;
        }
        if (last_speed_pot != Player.num_speed_pot)
        {
            pot_container.Find("speed_potion").Find("potion_quantity").GetComponent<TextMeshProUGUI>().SetText(Player.num_speed_pot.ToString());
            last_speed_pot = Player.num_speed_pot;
        }
    }
    public void add_quest(Quest q)
    {
        Transform temp_q = Instantiate(quest_template, quest_container);
        temp_q.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50+Player.nr_of_quests*-100);
        temp_q.name = q.quest_title;
        temp_q.Find("quest_description").GetComponent<TextMeshProUGUI>().SetText(q.quest_description);
        temp_q.Find("quest_counter").gameObject.SetActive(q.is_counter);
        switch(q.status)
        {
            case Quest.quest_status.available:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = available;
                break;
            case Quest.quest_status.in_progress:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = in_progress;
                break;
            case Quest.quest_status.completed:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = completed;
                break;
            default:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = available;
                break;
        }
        temp_q.gameObject.SetActive(true);
    }
    public void update_quest_status(Quest q)
    {
        Transform temp_q = quest_container.Find(q.quest_title);
        switch (q.status)
        {
            case Quest.quest_status.available:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = available;
                break;
            case Quest.quest_status.in_progress:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = in_progress;
                break;
            case Quest.quest_status.completed:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = completed;
                break;
            default:
                temp_q.Find("quest_icon").GetComponent<Image>().sprite = available;
                break;
        }
    }
    public void complete_quest(string q)
    {
        Transform temp_q = quest_container.Find(q);
        if (temp_q != null)
        {
            temp_q.Find("quest_icon").GetComponent<Image>().sprite = completed;
            temp_q.gameObject.SetActive(false);
        }
    }
}
