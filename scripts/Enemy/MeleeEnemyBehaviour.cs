using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Saša
 * Description: This script lets an enemy follow the player if he is in range.
 * When the player is in range, the enemy will attack him.
 */
public class MeleeEnemyBehaviour : MonoBehaviour
{
    private Animator animator;
    public Transform target;
    public Transform initialPos;
    public float speed;
    public float maxRange;
    public float minRange;
    public float attackRange;
    private int damage;

    public float attackTime = .80f; // The higher the number, the slower the rate of damage to the player
    private float attackCounter = .01f; // countdown until the attack-state is over; this is not const
    public bool isAttacking;

    public float waitunitlknockback;
    public float knockbackcounter = 0.1f;

    public float knockbackpower;
    public float knockbackduration;

    //CameraShake shaker;
    public float shakeduration;
    public float shakeintensity;

    private void Awake()
    {
        damage = transform.GetComponent<Enemy>() != null ? transform.GetComponent<Enemy>().get_damage() : 50;
    }
    // Start is called before the first frame update
    void Start()
    {
        //shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange &&
            Vector3.Distance(target.position, transform.position) >= minRange)
        {
            if (!isAttacking)
            {
                followPlayer();
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        handleAttack();
    }

    public void handleAttack()
    {
        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            knockbackcounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
            if (knockbackcounter <= 0)
            {
                target.gameObject.GetComponent<movement>().knockback(knockbackduration, knockbackpower, this.transform);
                //shaker.Shake(shakeduration, shakeintensity);
            }
        }

        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            if (isAttacking)
            {

            }
            else
            {
                isAttacking = true;
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", true);
                attackCounter = attackTime;
                knockbackcounter = waitunitlknockback;
                popup_damage.popup(damage, target.position, true);
                target.gameObject.GetComponent<Player>().take_damage(damage);
                if(gameObject.name == "Orc")
                {
                    soundEngine.play_sound(soundEngine.sound_type.troll_attack);
                }
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
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
}
