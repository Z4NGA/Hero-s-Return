using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable_items : MonoBehaviour
{
    [SerializeField] private string type="";
    [SerializeField] private int amount=1;
    [SerializeField] private string discription="";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            collision.collider.GetComponent<Player>().add_to_inventory(type, amount, discription);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().add_to_inventory(type, amount, discription);
            Destroy(gameObject);
        }
    }
}
