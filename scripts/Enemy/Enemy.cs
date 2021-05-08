using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour, in_targetable
{
    private bool is_dead;
    public bool invincible=false;
    public Transform target;
    public GameObject reward_chest1;
    public GameObject reward_chest2;
    public GameObject reward_sword;
    [SerializeField] private GameObject special_drop = null;
    private int health, initial_health;
    private Transform hp_container, bar,lvl_container,name_container;
    [SerializeField] private int level = 1;
    [SerializeField] private string name = "unnamed";
    [SerializeField]private string enemy_type="undefined" ;
    [SerializeField] private GameObject npc = null;
    [SerializeField] private string quest = "";
    public GameObject bloodstain;
    public GameObject bloodsplash;

    void Awake()
    {
        is_dead = false;
        hp_container = transform.Find("hp_container");
        lvl_container = transform.Find("lvl_container");
        name_container = transform.Find("name_container");
        name_container.Find("name_text").GetComponent<TextMeshProUGUI>().SetText(name);
        lvl_container.Find("level").GetComponent<TextMeshProUGUI>().SetText(level.ToString());
        bar = hp_container.Find("bar").Find("bar_sprite");
        initial_health = 150;
        health = initial_health;
        target = FindObjectOfType<Player>().transform;
    }

    void Start()
    {
        update_stats();
        if (invincible)
        {   
            if(transform.Find("invincible_light")!=null)
                transform.Find("invincible_light").gameObject.SetActive(true);
        }
    }

    void update_stats()
    {
        bar.GetComponent<RectTransform>().localScale = new Vector2((float)health / initial_health, 1);
    }
    
    public void take_damage(int dmg)
    {

        if (bloodstain != null)
        {
            int spawn = Random.Range(1, 4);
            if (spawn == 1)
            { 
                Instantiate(bloodstain, transform.position, Quaternion.identity);
            }
            Instantiate(bloodsplash, transform.position, Quaternion.identity);
        }
       
        if (!invincible)
        {
            Vector3 difference = (transform.position - target.position).normalized;
            if (!is_dead)
            {
                if (health > dmg)
                {
                    health -= dmg;
                    transform.position = new Vector3(transform.position.x + difference.x * 2, transform.position.y + difference.y * 2);
                }
                else
                {
                    
                    GetComponent<Animator>().SetBool("isDead", true);
                    health = 0; is_dead = true;
                    reward_player();
                    Destroy(gameObject, 0.5f);
                    if (npc != null) npc.GetComponent<npc>().complete_quest(quest);
                }
                update_stats();
            }
        }
    }
    private void reward_player()
    {
        if (special_drop != null) { GameObject g = Instantiate(special_drop, transform.position, Quaternion.identity); g.SetActive(true); }
        GameObject rwd;
        switch (enemy_type)
        {
            case "rat":
                //rwd = Instantiate(reward, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(50);
                Player.gain_coins(20);
                popup_text.popup_xp("+50 XP", transform.position);
                popup_text.popup("+20", transform.position, true);
                break;
            case "skeleton":
                rwd = Instantiate(reward_sword, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(95);
                Player.gain_coins(55);
                popup_text.popup_xp("+95 XP", transform.position);
                popup_text.popup("+55", transform.position, true);
                break;
            case "troll":
                rwd = Instantiate(reward_chest2, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(220);
                Player.gain_coins(110);
                popup_text.popup_xp("+220 XP", transform.position);
                popup_text.popup("+110", transform.position, true);
                break;
            case "ghost":
                rwd = Instantiate(reward_chest1, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(100);
                Player.gain_coins(75);
                popup_text.popup_xp("+100 XP", transform.position);
                popup_text.popup("+75", transform.position, true);
                break;
            case "werewolf":
                rwd = Instantiate(reward_sword, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(175);
                Player.gain_coins(100);
                popup_text.popup_xp("+175 XP", transform.position);
                popup_text.popup("+100", transform.position, true);
                break;
            case "dragon":
                //rwd = Instantiate(reward, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(50);
                Player.gain_coins(40);
                popup_text.popup_xp("+50 XP", transform.position);
                popup_text.popup("+40", transform.position, true);
                break;
            default:
                //rwd = Instantiate(reward, transform.position, Quaternion.identity);
                //rwd.GetComponent<SpriteRenderer>().color = new Color(152, 93, 72);
                Player.gain_xp(50);
                Player.gain_coins(35);
                popup_text.popup_xp("+50 XP", transform.position);
                popup_text.popup("+35", transform.position, true);
                break;
        }
    }
    public int get_damage()
    {
        switch (enemy_type)
        {
            default: return 25;
            case "rat": return 15;
            case "skeleton": return 27;
            case "troll": return 75;
            case "ghost": return 50;
            case "werewolf": return 37;
            case "dragon": return 32;
        }
    }
}
