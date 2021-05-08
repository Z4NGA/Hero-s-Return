using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxLife : MonoBehaviour,in_targetable
{
    int hp = 5;
    [SerializeField] private GameObject loot = null;
    public void take_damage(int dmg)
    {
       hp -= 1;
       if (hp <= 0) break_barrel();
    }

   
    private void break_barrel()
    {
        if (loot != null) { GameObject g = Instantiate(loot, transform.position, Quaternion.identity); g.SetActive(true); }
        Destroy(gameObject, 1.5f);
        transform.Find("off").gameObject.SetActive(true);
        transform.Find("on").gameObject.SetActive(false);
    }
}
