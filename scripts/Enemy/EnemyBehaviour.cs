using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Saša
 * Description: This script only lets an enemy follow the player if he is in range
 */
public class EnemyBehaviour : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    public Transform initialPos;
    public float speed;
    public float maxRange;
    public float minRange;
    public int damage;

    public float knockbackpower;
    public float knockbackduration;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = FindObjectOfType<movement>().transform;
    }

    // Update is called once per frame
    void Update()
    { 
            if (Vector3.Distance(target.position, transform.position) <= maxRange &&
                Vector3.Distance(target.position, transform.position) >= minRange)
            {
                followPlayer();
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
    }

    public void followPlayer()
    {
        animator.SetBool("isMoving", true);
        animator.SetFloat("moveX", target.position.x - transform.position.x);
        animator.SetFloat("moveY", target.position.y - transform.position.y);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void goBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPos.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().take_damage(damage);
                other.gameObject.GetComponent<movement>().knockback(knockbackduration, knockbackpower, this.transform);
            }
    }

}
