using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedController : MonoBehaviour
{
    private float creationTime;
    void Start()
    {
        creationTime = Time.time;
    }

    void Update()
    {
        float timeSinceCreation = Time.time - creationTime;
        if (timeSinceCreation >= 20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tank")
        {
            Destroy(gameObject);
        }
    }
}
