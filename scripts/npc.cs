using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    [SerializeField] private bool in_cage = true;
    [SerializeField] private Quest[] quests=null;
    [SerializeField] private GameObject player_ui=null;
    [SerializeField] private int nr_of_quests = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            if (nr_of_quests <= 0)
            {
                transform.Find("end_dialog").gameObject.SetActive(true);
                Destroy(gameObject, 14f);
                return;

            }
            if (in_cage)
            {
                transform.Find("help").gameObject.SetActive(true);

                foreach (Quest q in quests)
                {
                    if (q.type == Quest.quest_type.urgent && q.status == Quest.quest_status.available)
                        start_quest(q);
                }
            }
            else
            {
                transform.Find("dialog1").gameObject.SetActive(true);
                foreach (Quest q in quests)
                {
                    if (q.status == Quest.quest_status.available)
                        start_quest(q);
                }
            }
            foreach (Quest q in quests)
            {
                if (q.status == Quest.quest_status.completed)
                    player_ui.GetComponent<player_ui>().complete_quest(q.quest_title);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            transform.Find("help").gameObject.SetActive(false);
            transform.Find("dialog1").gameObject.SetActive(false);
            transform.Find("end_dialog").gameObject.SetActive(false);
            foreach (Quest q in quests)
                if (q.status == Quest.quest_status.in_progress) player_ui.GetComponent<player_ui>().update_quest_status(q);
        }
    }
    private void start_quest(Quest q)
    {
        soundEngine.play_sound_timed(soundEngine.sound_type.quest_accepted, 5f);
        player_ui.GetComponent<player_ui>().add_quest(q);
        q.start_quest();
    }
    public void complete_quest(string quest_title)
    {
        foreach(Quest q in quests)
        {
            if (q.quest_title.Equals(quest_title)) { 
                q.complete_quest();
                player_ui.GetComponent<player_ui>().update_quest_status(q);
                nr_of_quests -= 1;
                soundEngine.play_sound_timed(soundEngine.sound_type.quest_completed, 5f);
            }
        }
    }
    public void free_npc()
    {
        in_cage = false;
    }
    
}




/*
 * npc old dude 
 * Help!!! O Hero thank god you're here! Look around for the key keeper, if you defeat him you can free me and i will reward you    
 * O Hero ! ! ! Thank you for saving my life, i thought i would die in this cage, the demon lord soldiers attacked our village and captured my family. As a reward of saving my life, I will tell you the secret of the Commander of this room. The legend says he has a very strong armor set that will help you in further quests! But you have to find the hidden barrel with the commander room key 
 * . . . . . Now I can entrust you with defeating the demon lord, i will use my magic to go back to the village and tell them about the hero that saved me
 * 
 * 
 * 
 * */
