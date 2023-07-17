using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int maxhealth = 10;
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown("d"))
        {

            takeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletEnemy")
        {
            takeDamage(1);
        }
    }
        public void takeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
