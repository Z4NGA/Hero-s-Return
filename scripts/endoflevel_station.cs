using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endoflevel_station : MonoBehaviour
{
    [SerializeField] private bool is_active = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<Player>()!=null)
        {
            collision.transform.GetComponent<Player>().savePlayer();
            GameObject.Find("GameEngine").GetComponent<GameEngine>().nextLevel();
        }
    }
}
