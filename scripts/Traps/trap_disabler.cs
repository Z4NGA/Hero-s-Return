using UnityEngine;

public class trap_disabler : MonoBehaviour
{
    [SerializeField] public GameObject[] trapobj;
    private in_trap trap;
    private Animator anim;
    private void Awake()
    {
        //trap = trapobj.GetComponent<in_trap>();
        anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Active", true);
            anim.SetBool("Idle", false);
        }
    }
    private void Update()
    {
        if (trapobj != null)
        {
            foreach (GameObject o in trapobj)
            {
                if (o.GetComponent<in_trap>().get_status())
                    anim.SetBool("Active", true);
                else
                    anim.SetBool("Active", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (trapobj != null)
            {
                foreach (GameObject o in trapobj)
                    o.GetComponent<in_trap>().turn_off();
                if (anim != null) anim.SetBool("Active", false);
            }
        }
    }
}
