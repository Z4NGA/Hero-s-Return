using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_melee : MonoBehaviour
{
    public Animator animator;
    public Animator camAnim;
    public bool isAttacking;

    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public float attackTime = .30f; // This influences, how long the attack-state animationwise is, .30f is ok
    public float attackCounter = .01f; // countdown until the attack-state is over, .01f is ok
    public LayerMask whatIsEnemies;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // float x = gameObject.GetComponent<movement>().facingDirection.x;
        // float y = gameObject.GetComponent<movement>().facingDirection.y;
        attackPos.SetPositionAndRotation(gameObject.GetComponent<movement>().facingDirection, new Quaternion(0, 0, 0, 0));

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isAttacking)
            {
                
            }
            else
            {
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
				attackCounter = attackTime;
				animator.SetBool("isAttacking", true);
				isAttacking = true;

				if (enemiesToDamage.Length == 0)
					return;

				if(enemiesToDamage[0].CompareTag("Boss"))
				{
					Debug.Log("Hit boss for " + damage);
					Boss b = enemiesToDamage[0].GetComponent<Boss>();
					b.TakeDamage(damage);
				}
				else if(enemiesToDamage[0].CompareTag("Boss Helmet"))
				{
					for (int i = 0; i < enemiesToDamage.Length; i++)
					{
						enemiesToDamage[i].GetComponent<SingleHelmet>().TakeDamage(damage);
					}
				}
				else
				{
					for (int i = 0; i < enemiesToDamage.Length; i++)
					{
						enemiesToDamage[i].GetComponent<EnemyHealthManager>().TakeDamage(damage);
					}
				}
			}
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
