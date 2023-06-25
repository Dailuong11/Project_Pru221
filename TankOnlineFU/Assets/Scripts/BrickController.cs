using Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BrickController : MonoBehaviour
{

    private int BrickHealth;

    Vector3Int posMaterial;

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
        BrickHealth--;
        if(BrickHealth <= 0)
        {
          
            Destroy(gameObject);


        }
    }

}
