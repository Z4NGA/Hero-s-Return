using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyhealth;
    public Transform target;
    public bool isDead;
    public GameObject bloodstain;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodstain, transform.position, Quaternion.identity);
        Vector3 difference = transform.position - target.position;
        transform.position = new Vector3(transform.position.x + difference.x, transform.position.y + difference.y);
        enemyhealth -= damage;
        Debug.Log("enemy has taken damage!");
        if (enemyhealth <= 0)
        {
            isDead = true;
            GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject, 3f);
        }
    }
   
}
