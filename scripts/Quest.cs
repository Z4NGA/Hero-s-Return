using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string quest_title;
    public bool is_counter;
    public string quest_description;
    public quest_status status;
    public quest_type type;
    public enum quest_status
    {
        available,
        accepted,
        in_progress,
        completed
    }
    public enum quest_type
    {
        main,
        side,
        urgent
    }
    public void start_quest()
    {
        status = Quest.quest_status.in_progress;
        Player.nr_of_quests += 1;
    }
    public void complete_quest()
    {
        status = Quest.quest_status.completed;
        Player.nr_of_quests -= 1;
    }
}
