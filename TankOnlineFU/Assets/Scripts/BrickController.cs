using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private int BrickHealth;
    // Start is called before the first frame update
    void Start()
    {
        BrickHealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            BrickHealth--;
            if (BrickHealth <= 0)
            {
                Destroy(gameObject);
            }
        }    
    }

}
