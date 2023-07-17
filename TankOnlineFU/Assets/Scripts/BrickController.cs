using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BrickController : MonoBehaviour
{

	private int BrickHealth;

	public GameObject explore;
	private int enemyDamage;
	private int tankDamage;

	[SerializeField] private AudioSource exploreSoundEffect;


	// Start is called before the first frame update
	void Start()
	{
		BrickHealth = 2;
	}

	// Update is called once per frame
	void Update()
	{
		enemyDamage = PlayerPrefs.GetInt("enemyDamage");
		tankDamage = PlayerPrefs.GetInt("tankDamage");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "bullet")
		{
			Debug.Log("Damage của tank là: " + tankDamage);
			BrickHealth -= tankDamage;
			if (BrickHealth <= 0)
			{
				Destroy(gameObject);
			}
		}
		else if (collision.gameObject.tag == "bulletEnemy")
		{
			Debug.Log("Damage của enemy là: " + enemyDamage);
			BrickHealth -= enemyDamage;
			if (BrickHealth <= 0)
			{
				Destroy(gameObject);
			}
		}

	}

}
