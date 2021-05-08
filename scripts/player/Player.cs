using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour,in_targetable
{
    public bool isAlive;
    bool is_invincible;
    public GameObject inventory_gui,DeathMenu,bg_pause,counter;
    public static bool inventory_changes = false;
    public static bool in_shop_range = false;
    private string[] pickable_items = { "", "", "", "", "", "", "", "", "", "", "", "" };
    
    public static int current_level,current_xp;
    private static int[] level_xp = { 125, 378, 598, 1356, 2145, 3565, 4445, 6800 };
    public static int coins;
    public static int gems;
    public static int num_health_pot;
    public static int num_speed_pot;
    public static int num_damage_pot;
    private static bool has_armor ;
    public static int current_health;
    public static int initial_health;
    public static int current_armor;
    public static int initial_armor;
    public static float current_speed;
    public static float initial_speed;
    public static float current_damage;
    public static float initial_damage;
    /*public  int Coins { //using propreties
    get{
            return Coins;
        }
    set
        {
            if (value < 0) Debug.LogError("Can't assign a negative value for coins !!");
            else Coins = value;
        }
    }

    public  int Armor { 
        get => armor_level;
        set
        {
            if (value < 0) Debug.LogError("Can't add a negative Value");
            else if (armor_level == 2) Debug.LogWarning("armor level is Already maxed");
            else armor_level = value;
        }
    }
    public  int Helmet {
        get => helmet_level;
        set {
            if (value < 0) Debug.LogError("Can't add a negative Value");
            else if (helmet_level == 2) Debug.LogWarning("Helmet level is Already maxed");
            else helmet_level = value; 
        }
    }*/
    public static int nr_of_quests = 0;

    private static int armor_level;
    private float death_time = 0;
    public int getArmor() { return armor_level; }
    private int helmet_level;
    public int getHelmet() { return helmet_level; }
    public void savePlayer()
    {
        PlayerPrefs.SetInt("initial_health", initial_health);
        PlayerPrefs.SetInt("initial_armor", initial_armor);
        PlayerPrefs.SetInt("armor_level", armor_level);
        PlayerPrefs.SetInt("num_health_pot", num_health_pot);
        PlayerPrefs.SetInt("num_speed_pot", num_speed_pot);
        PlayerPrefs.SetInt("num_damage_pot", num_damage_pot);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("gems", gems);
        PlayerPrefs.SetInt("current_level", current_level);
        PlayerPrefs.SetInt("current_xp", current_xp);
        PlayerPrefs.SetFloat("initial_damage", initial_damage);
        PlayerPrefs.SetFloat("initial_speed", initial_speed);

    }
    public static void newplayer()
    {
        PlayerPrefs.DeleteKey("initial_health");
        PlayerPrefs.DeleteKey("initial_armor");
        PlayerPrefs.DeleteKey("armor_level");
        PlayerPrefs.DeleteKey("num_health_pot");
        PlayerPrefs.DeleteKey("num_speed_pot");
        PlayerPrefs.DeleteKey("num_damage_pot");
        PlayerPrefs.DeleteKey("coins");
        PlayerPrefs.DeleteKey("gems");
        PlayerPrefs.DeleteKey("current_level");
        PlayerPrefs.DeleteKey("current_xp");
        PlayerPrefs.DeleteKey("initial_damage");
        PlayerPrefs.DeleteKey("initial_speed");
    }
    public void loadPlayer()
    {
        initial_health= PlayerPrefs.GetInt("initial_health",initial_health);
        initial_armor=  PlayerPrefs.GetInt("initial_armor", initial_armor);
        armor_level =   PlayerPrefs.GetInt("armor_level", armor_level);
        num_health_pot= PlayerPrefs.GetInt("num_health_pot", num_health_pot);
        num_speed_pot=  PlayerPrefs.GetInt("num_speed_pot", num_speed_pot);
        num_damage_pot= PlayerPrefs.GetInt("num_damage_pot", num_damage_pot);
        coins=          PlayerPrefs.GetInt("coins", coins);
        gems=           PlayerPrefs.GetInt("gems", gems);
        current_level=  PlayerPrefs.GetInt("current_level", current_level);
        current_xp=     PlayerPrefs.GetInt("current_xp", current_xp);
        initial_damage= PlayerPrefs.GetFloat("initial_damage", initial_damage);
        initial_speed=  PlayerPrefs.GetFloat("initial_speed", initial_speed);
        has_armor = initial_armor > 0 ? true : false;
        current_health = initial_health;
        current_armor = initial_armor;
        current_speed = initial_speed;
        current_damage = initial_damage;

    }
    private void Awake()
    {
        is_invincible = false;
        isAlive = true;
        initial_health = 500;
        current_health = initial_health; //starts at full hp
        coins = 200; //starts with 200 base coins 
        gems = 0;//starts with 0 gems
        num_health_pot =0 ;
        num_speed_pot=0;
        num_damage_pot=0;
        initial_armor = 0; //starts with 0 armor
        current_armor = initial_armor; // current armor starts same  as initial
        has_armor = false; //player starts without armor
        current_level = 0;
        current_xp = 0;
        initial_speed = 10;
        current_speed = initial_speed;
        initial_damage = 25;
        current_damage = initial_damage;
        armor_level = 0;
        loadPlayer();
    }
    private void Update()
    {
        if (current_health <= 0)
        {   if (!isAlive)
            {
                if ((death_time - Time.time) <= 0) { 
                    GameObject.Find("GameEngine").GetComponent<GameEngine>().selectMain();
                    return;
                }
                counter.GetComponent<TextMeshProUGUI>().SetText((Mathf.Floor(death_time - Time.time)).ToString());
                return;
            }
            death_time = Time.time+10;
            isAlive = false;
            GetComponent<Animator>().SetBool("isDead", true);
            if(DeathMenu!=null)DeathMenu.SetActive(true);
            if(bg_pause!=null) bg_pause.SetActive(true);
        }
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventory_gui.GetComponent<inventory_ui>().is_active = !inventory_gui.GetComponent<inventory_ui>().is_active;
                inventory_gui.SetActive(inventory_gui.GetComponent<inventory_ui>().is_active);
                inventory_gui.GetComponent<inventory_ui>().update_stats();
            }
        }
    }

    public  void spendCoins(int amount)
    {
        if (coins >= amount)
            coins -= amount;
    }
    public bool canAfford(int amount)
    {
        Debug.Log("trying to buy !:!");
        if (coins >= amount)
        {
            Debug.Log("can be purchased");
            return true;
        }
        Debug.Log("can not be purchased");
        return false;
    }

    public  bool boughtItem(Items.Item_type i)
    {
        switch (i)
        {
            case Items.Item_type.armor_lvl1:
                return(add_armor(1));
            case Items.Item_type.armor_lvl2:
                return(add_armor(2));
            case Items.Item_type.helmet_lvl1:
                helmet_level = 1;
                return true;
            case Items.Item_type.helmet_lvl2:
                helmet_level = 2;
                return false;
            case Items.Item_type.health_potion:
                num_health_pot++;
                return true;
            case Items.Item_type.damage_potion:
                num_damage_pot++;
                return true;
            case Items.Item_type.speed_potion:
                num_speed_pot++;
                return true;
            default:
                return true;
        }
    }

    public void take_damage(int dmg)
    {
        if (!GetComponent<movement>().isDashing) // prevents damage while dashing
        {
            if (!is_invincible)
            {
                if (has_armor)
                {
                    current_armor = current_armor > dmg ? current_armor - dmg : 0;
                    current_health = current_armor > dmg ? current_health : current_health - (dmg - current_armor);
                    has_armor = current_armor > 0 ? true : false;
                }
                else
                {
                    current_health = (current_health - dmg) < 0 ? 0 : current_health - dmg;//stop it from going under 0 hp
                }
                Debug.Log("current armor : " + current_armor + ", current health : " + current_health);
            }
        }
        else Debug.Log("player is invincible in the safe zone thus takes no damage !");
    }
    public void set_invincible(bool i)
    {
        is_invincible = i;
    }
    public void add_coins(int amount)
    {
        coins += amount;
    }
    public static void gain_coins(int amount)
    {
        coins += amount;
    }
    public static void gain_gems(int amount)
    {
        gems += amount;
    }
    public static void sell_item(int price)
    {
        coins += price; 
    }
    public void add_gems(int amount)
    {
        gems += amount;
    }
    public static int current_lvl_xp()
    {
        if(current_level<level_xp.Length)
            return level_xp[current_level];
        return 999999;
    }
    public static void check_level_up()
    {
        if (current_xp > current_lvl_xp())
        {
            soundEngine.play_sound_timed(soundEngine.sound_type.level_up, 4f);
            current_level += 1;
            upgrade_stat(10, "health", true);
            upgrade_stat(10, "damage", true);
            upgrade_stat(10, "speed", true);
            Debug.Log("LEVEL UP !! YOU are now level " + current_level + " !!");
            check_level_up();
        }
        //popup_text.popup("LEVEL UP !!", transform.position, false);
    }
    public static void gain_xp(int amount)
    {
        current_xp += amount;
        check_level_up();
    }
    public static bool use_item(Items.Item_type i)
    {
        switch (i)
        { //case Items.Item_type.xp_potion : 
            case Items.Item_type.health_potion:
                if(num_health_pot>0)
                {
                    if (current_health != initial_health)
                    {
                        soundEngine.play_sound(soundEngine.sound_type.potion);
                        upgrade_stat(15, "health", false);
                        num_health_pot--;
                        inventory_changes = true;
                        return true;
                    }
                    else return false;
                }
                return false;
            case Items.Item_type.damage_potion:
                if (num_damage_pot > 0)
                {
                    soundEngine.play_sound(soundEngine.sound_type.potion);
                    upgrade_stat(20, "damage", false);
                    num_damage_pot--;
                    inventory_changes = true;
                    return true;
                }
                return false;
            case Items.Item_type.speed_potion:
                if (num_speed_pot > 0)
                {
                    soundEngine.play_sound(soundEngine.sound_type.potion);
                    upgrade_stat(25, "speed", false);
                    num_speed_pot--;
                    inventory_changes = true;
                    return true;
                }
                return false;
            case Items.Item_type.armor_lvl1:
                return(add_armor(1));
            case Items.Item_type.armor_lvl2:
                return(add_armor(2));
            case Items.Item_type.weapon_1:
                return false;
            case Items.Item_type.weapon_2:
                return false;
            default: return false;
        }
    }
    public static void upgrade_stat(int percent,string stat_type,bool is_permanant)
    {
        if (!is_permanant)
        {
            switch (stat_type)
            {
                case "health":
                    current_health = current_health + (int)(((float)percent / 100) * current_health) > initial_health ? initial_health : current_health + (int)(((float)percent / 100) * current_health);
                    break;
                case "speed":
                    current_speed = (int)(current_speed + ((float)percent / 100) * current_speed);
                    break;
                case "damage":
                    current_damage = (int)(current_damage + ((float)percent / 100) * current_damage);
                    break;
                default: break;
            }
        }
        else
        {
            switch (stat_type)
            {
                case "health":
                    initial_health = initial_health + (int)(((float)percent / 100) * initial_health);
                    current_health = initial_health;
                    break;
                case "speed":
                    initial_speed = (int)(initial_speed + ((float)percent / 100) * initial_speed);
                    current_speed = initial_speed;
                    break;
                case "damage":
                    initial_damage = (int)(initial_damage + ((float)percent / 100) * initial_damage);
                    current_damage = initial_damage;
                    break;
                default: break;
            }
        }
    }
    public void add_to_inventory(string type  ,int amount ,string discription)
    {
        soundEngine.play_sound(soundEngine.sound_type.pick_item);
        if (inventory_gui.GetComponent<inventory_ui>().items_in_inventory > 0)
        {
            foreach(string t in pickable_items)
            {
                if (t.Equals(type))
                {
                    inventory_gui.GetComponent<inventory_ui>().add_existing_pickable_item(type, amount);
                    return;
                }
            }
        }
        inventory_gui.GetComponent<inventory_ui>().add_pickable_item(type, amount, discription);
        pickable_items[inventory_gui.GetComponent<inventory_ui>().items_in_inventory] = type;
    }
    //add remove armor helmets etc 
    private static bool add_armor(int armor_lvl)
    {
        if(!has_armor)
        {//sets new armor
            current_armor = armor_lvl == 1 ? 250 : 500;
            initial_armor = armor_lvl == 1 ? 250 : 500;
            has_armor = true;
            armor_level = armor_lvl;
            return true;
        }
        else
        {
            if (armor_lvl == armor_level)
            {
                if (current_armor < initial_armor)
                {
                    //replenishes armor
                    current_armor = initial_armor;
                    has_armor = true;
                    return true;
                }
                has_armor = true;
                return false;
            }
            else if (armor_lvl > armor_level)
            {
                //upgrade armor
                armor_level = armor_lvl;
                initial_armor = 500;//temp then *45%
                current_armor = initial_armor;
                has_armor = true;
                return true;
            }
            return false;
        }
    }

}
