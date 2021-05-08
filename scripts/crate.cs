using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null)
        {
            transform.GetComponent<Rigidbody2D>().mass = 40;
        }
        else transform.GetComponent<Rigidbody2D>().mass = 999999;

    }
}
