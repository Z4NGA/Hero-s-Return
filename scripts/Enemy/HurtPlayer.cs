using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Saša
// Descirption: This scripts enables damage to the player if he touches an enemy
// THIS SCRIPT IS OLD!
public class HurtPlayer : MonoBehaviour
{
    public int damage = 10;
    public float waitToHurt;
    public bool isTouching;
    private PlayerHealthManager healthMan;

    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;

            if (waitToHurt <= 0)
            {
                healthMan.HurtPlayer(damage);
                waitToHurt = 2f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isTouching = true;
    }
}
