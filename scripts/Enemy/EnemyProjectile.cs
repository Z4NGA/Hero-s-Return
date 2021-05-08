using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Transform player;
    private Vector2 target;
    public float projectileSpeed;
    public int projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<movement>().transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, projectileSpeed * Time.deltaTime);

        if(transform.position.x == target.x && 
           transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            popup_damage.popup(projectileDamage, other.transform.position, true);
            other.gameObject.GetComponent<Player>().take_damage(projectileDamage);
        }
    }
}
