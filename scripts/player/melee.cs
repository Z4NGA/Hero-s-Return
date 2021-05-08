using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    private Animator animator;

    private bool isAttacking;

    [SerializeField] private Transform attackPos = null;
    [SerializeField] private float attackRangeX=2.5f;
    [SerializeField] private float attackRangeY=2.5f;
    [SerializeField] private float attack_duration = .30f; // This influences, how long the attack-state animationwise is, .30f is ok
    private float last_attack = 0; // countdown until the attack-state is over, .01f is ok
    public LayerMask whatIsEnemies;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackPos.SetPositionAndRotation(transform.position+gameObject.GetComponent<movement>().facingDirection, new Quaternion(0, 0, 0, 0));

        if (isAttacking)
        {
            if (Time.time > last_attack + attack_duration)
            {
                Debug.Log("player stopped ");
                animator.SetBool("isAttacking", false);
                isAttacking = false;
                last_attack = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isAttacking)
            {
                Debug.Log("player alraedy attacking");
            }
            else
            {
                soundEngine.play_sound(soundEngine.sound_type.player_melee2);
                Debug.Log("player started ");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
                last_attack = Time.time;
                animator.SetBool("isAttacking", true);
                isAttacking = true;

                if (enemiesToDamage.Length == 0)
                    return;
                if (enemiesToDamage[0].CompareTag("Boss"))
                {
                    Boss b = enemiesToDamage[0].GetComponent<Boss>();
                    b.TakeDamage(Player.current_damage);
                    popup_damage.popup(Player.current_damage, b.transform.position, false);
                }
                else if (enemiesToDamage[0].CompareTag("Boss Helmet"))
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].GetComponent<SingleHelmet>() != null)
                        {
                            enemiesToDamage[i].GetComponent<SingleHelmet>().TakeDamage(Player.current_damage);
                            popup_damage.popup(Player.current_damage, enemiesToDamage[i].transform.position, false);
                        }
                    }
                    
                }
				else if (enemiesToDamage[0].CompareTag("Boss Lightning Orb"))
				{
					for (int i = 0; i < enemiesToDamage.Length; i++)
					{
                        if (enemiesToDamage[i].GetComponent<ChargeLightningOrb>() != null)
                        {
                            enemiesToDamage[i].GetComponent<ChargeLightningOrb>().TakeDamage(Player.current_damage);
                            popup_damage.popup(Player.current_damage, enemiesToDamage[i].transform.position, false);
                        }
                    }
				}
				else
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].GetComponent<Enemy>() != null)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().take_damage((int)Player.current_damage);
                            popup_damage.popup(Player.current_damage, enemiesToDamage[i].transform.position, false);
                        }
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
