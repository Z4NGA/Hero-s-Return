using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_station : MonoBehaviour
{
    [SerializeField] private GameObject receiving_tp_station=null;
    private bool trigger_by_tp;
    private void Awake()
    {
        trigger_by_tp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            if (!trigger_by_tp)
            {
                collision.transform.position = receiving_tp_station.transform.position;
                receiving_tp_station.GetComponent<teleport_station>().trigger_by_tp = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<Player>()!=null)
            trigger_by_tp = false;
    }
}
