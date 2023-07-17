using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BrickController : MonoBehaviour
{

    private int BrickHealth;

    Vector3Int posMaterial;
    public GameObject explore;
    [SerializeField] private AudioSource exploreSoundEffect;


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
        if(collision.gameObject.tag == "bullet" || collision.gameObject.tag == "bulletEnemy")
        {
            exploreSoundEffect.Play();
            BrickHealth--;
            if (BrickHealth <= 0)
            {

                //var obj = Instantiate<GameObject>(explore, transform.position, Quaternion.identity);
                Destroy(gameObject);

                //Destroy(obj, 0.2f);
            }
        }
        
    }

}
