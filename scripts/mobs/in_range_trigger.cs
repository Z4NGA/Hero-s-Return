using UnityEngine;

public class in_range_trigger : MonoBehaviour
{
    private dragon instance;


    // Start is called before the first frame update
    void Awake()
    {
        instance = transform.GetComponentInParent<dragon>();
        instance.angle = instance.starting_angle; instance.last_angle_change = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            instance.p = collision.GetComponent<Transform>();
            instance.angle = instance.starting_angle;
            instance.last_angle_change = Time.time;
            instance.player_in_range = true;
        }
            
        
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            instance.set_state("idle");
            instance.p = null;
            instance.player_in_range = false;
        }
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {

    if (collision.GetComponent<Player>() != null)
    {
        //Debug.Log("player in range");
        p = collision.GetComponent<Transform>();
        if (!fixed_shooter)
        {
            instance.shoot(p.position);
        }
        else
        {
            if (Time.time > last_angle_change + 1) angle += 15;
            instance.shoot_to_fixed_position(angle);
        }
    }
        
    }*/
}
