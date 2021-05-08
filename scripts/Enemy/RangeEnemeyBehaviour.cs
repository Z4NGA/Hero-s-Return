using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Saša
 * Description: Ranged Enemies try to have a certain distance from the player and shoot 
 * him with projectiles
 */
public class RangeEnemeyBehaviour : MonoBehaviour
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
    public float attackCounter = .01f; // countdown until the attack-state is over; this is not const
    public bool isAttacking;

    public GameObject projectile;
    private void Awake()
    {
        damage = transform.GetComponent<Enemy>() != null ? transform.GetComponent<Enemy>().get_damage() : 50;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = FindObjectOfType<movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyHealthManager>().isDead)
        {

        }
        else
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

            handleAttack();
        }
    }

    public void handleAttack()
    {
        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            if (isAttacking)
            {

            }
            else
            {
                Vector2 difference = target.position - transform.position;
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle + -180f);

                animator.SetBool("isAttacking", true);
                Instantiate(projectile, transform.position, rotation); // see prefabs folder
                isAttacking = true;
                attackCounter = attackTime;
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
