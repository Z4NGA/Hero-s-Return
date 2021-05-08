using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal_station : MonoBehaviour
{
    [SerializeField] private int heal_per_ratio=3;
    [SerializeField] private float ratio=5;
    private float last_healed = 0;
    private bool in_range = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
            in_range = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
            in_range = false;
    }
    private void Update()
    {
        if (in_range)
        {
            if (Time.time > last_healed + (1 / ratio))
            {
                Player.upgrade_stat(heal_per_ratio, "health", false);
                last_healed = Time.time;
            }
        }
    }
}
