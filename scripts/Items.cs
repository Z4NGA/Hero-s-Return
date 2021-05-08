using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    /*public Sprite armor_lvl1_s;
    public Sprite armor_lvl2_s;
    public Sprite helmet_lvl1_s;
    public Sprite helmet_lvl2_s;
    //public Sprite weapon_1; // no weapons yet
    //public Sprite weapon_2;
    public Sprite health_potion_s;
    public Sprite damage_potion_s;
    public Sprite speed_potion_s;*/
    // Start is called before the first frame update
    public enum Item_rarity
    {
        common,
        rare,
        epic,
        legendary
    }
    public enum Item_type
    {
        undefined,
        no_armor,
        armor_lvl1,
        armor_lvl2,
        helmet_lvl1,
        helmet_lvl2,
        no_weapon,
        weapon_1,
        weapon_2,
        health_potion,
        damage_potion,
        speed_potion
    }
    //todo : add junk and other items
    public static int getPrice(Item_type itemtype)
    {
        switch (itemtype)
        {
            default:return 25;

            case Item_type.armor_lvl1: return 300;
            case Item_type.armor_lvl2: return 700;
            case Item_type.helmet_lvl1: return 200;
            case Item_type.helmet_lvl2: return 450;
            case Item_type.weapon_1: return 100;
            case Item_type.weapon_2: return 150;
            case Item_type.health_potion: return 50;
            case Item_type.damage_potion: return 75;
            case Item_type.speed_potion: return 75;
        }
    }
    public static string getName(Item_type itemtype)
    {
        switch (itemtype)
        {
            default: return "undefined";

            case Item_type.armor_lvl1: return "Armor 1";
            case Item_type.armor_lvl2: return "Armor 2";
            case Item_type.helmet_lvl1: return "Helmet 1";
            case Item_type.helmet_lvl2: return "Helmet 2";
            case Item_type.weapon_1: return "Weapon 1";
            case Item_type.weapon_2: return "Weapon 2";
            case Item_type.health_potion: return "Hp Potion";
            case Item_type.damage_potion: return "Dmg Potion";
            case Item_type.speed_potion: return "Speed Up";
        }
    }
    public static Sprite getSprite(Item_type itemtype)
    {
        switch (itemtype)
        {
            default: return itemAssets.instance.undefined;

            case Item_type.armor_lvl1: return itemAssets.instance.armor_lvl1;
            case Item_type.armor_lvl2: return itemAssets.instance.armor_lvl2;
            case Item_type.helmet_lvl1: return itemAssets.instance.helmet_lvl1;
            case Item_type.helmet_lvl2: return itemAssets.instance.helmet_lvl2;
            case Item_type.weapon_1: return null;
            case Item_type.weapon_2: return null;
            case Item_type.health_potion: return itemAssets.instance.health_potion;
            case Item_type.damage_potion: return itemAssets.instance.damage_potion;
            case Item_type.speed_potion: return itemAssets.instance.speed_potion;
        }
    }
    public static Item_type get_type(string name)
    {
        switch(name)
        {         

            case "Armor 1": return Item_type.armor_lvl1;
            case "Armor 2": return Item_type.armor_lvl2;
            case "Helmet 1": return Item_type.helmet_lvl1;
            case "Helmet 2": return Item_type.helmet_lvl2;
            case "Weapon 1": return Item_type.weapon_1;
            case "Weapon 2": return Item_type.weapon_2;
            case "Hp Potion": return Item_type.health_potion;
            case "Dmg Potion": return Item_type.damage_potion;
            case "Speed Up": return Item_type.speed_potion;

            default: return Item_type.undefined;
        }
    }
    public static bool is_usable(Item_type type)
    {
        switch (type)
        {
            case Item_type.armor_lvl1: return true;
            case Item_type.armor_lvl2: return true;
            case Item_type.helmet_lvl1: return true;
            case Item_type.helmet_lvl2: return true;
            case Item_type.weapon_1: return true;
            case Item_type.weapon_2: return true;
            case Item_type.health_potion: return true;
            case Item_type.damage_potion: return true;
            case Item_type.speed_potion: return true;
            default: return false;
        }
    }
    public static Item_rarity get_rarity(Item_type type)
    {
        switch (type)
        {
            case Item_type.armor_lvl1: return Item_rarity.common;
            case Item_type.armor_lvl2: return Item_rarity.rare;
            case Item_type.helmet_lvl1: return Item_rarity.common;
            case Item_type.helmet_lvl2: return Item_rarity.rare;
            case Item_type.weapon_1: return Item_rarity.common;
            case Item_type.weapon_2: return Item_rarity.rare;
            case Item_type.health_potion: return Item_rarity.common;
            case Item_type.damage_potion: return Item_rarity.common;
            case Item_type.speed_potion: return Item_rarity.common;
            default: return Item_rarity.common;
        }
    }
}
