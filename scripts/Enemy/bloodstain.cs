using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodstain : MonoBehaviour
{
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waituntildestruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waituntildestruction()
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
