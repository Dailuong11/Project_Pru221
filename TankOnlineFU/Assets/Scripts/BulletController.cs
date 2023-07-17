using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public Bullet Bullet { get; set; }
	public GameObject explore;

	public int MaxRange { get; set; }

	// Start is called before the first frame update
	private void Start()
	{
	}

	// Update is called once per frame
	private void Update()
	{
		DestroyAfterRange();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Brick" || collision.gameObject.tag == "Wall_Steel")
		{
			var obj = Instantiate<GameObject>(explore, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(obj, 0.2f);
		}
		else if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "bulletEnemy")
		{
			var obj = Instantiate<GameObject>(explore, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(obj, 0.2f);
		}
		else if (collision.gameObject.tag == "Enemy" && Bullet.Tank.Name == "Tank")
		{
			Debug.Log(Bullet.Tank.Name);
			var obj = Instantiate<GameObject>(explore, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(obj, 0.2f);
		}
		else if (collision.gameObject.tag == "tank" && Bullet.Tank.Name == "Enemy")
		{
			Debug.Log(Bullet.Tank.Name);
			var obj = Instantiate<GameObject>(explore, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(obj, 0.2f);
		}
	}


	private void DestroyAfterRange()
	{
		var currentPos = gameObject.transform.position;
		var initPos = Bullet.InitialPosition;
		switch (Bullet.Direction)
		{
			case Direction.Down:
				if (initPos.y - MaxRange >= currentPos.y)
				{
					Destroy(gameObject);
				}

				break;
			case Direction.Up:
				if (initPos.y + MaxRange <= currentPos.y)
				{
					Destroy(gameObject);
				}

				break;
			case Direction.Left:
				if (initPos.x - MaxRange >= currentPos.x)
				{
					Destroy(gameObject);
				}

				break;
			case Direction.Right:
				if (initPos.x + MaxRange <= currentPos.x)
				{
					Destroy(gameObject);
				}

				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}