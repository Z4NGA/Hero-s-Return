using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_trigger : MonoBehaviour
{
    [SerializeField] private bool activate = true;
    [SerializeField] private GameObject[] traps = null;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() != null ){
            if (traps != null)
            {
                foreach (GameObject t in traps)
                {
                    if (activate) t.GetComponent<in_trap>().turn_on();
                    else t.GetComponent<in_trap>().turn_off();
                }
            }
        }
    }
}
