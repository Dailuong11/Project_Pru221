using DefaultNamespace;
using Entity;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TankController : MonoBehaviour
{
	// Start is called before the first frame update  
	public GameObject shield_1;
	public GameObject shield_2;
	public Sprite tankUp;
	public Sprite tankDown;
	public Sprite tankLeft;
	public Sprite tankRight;

	private Tank _tank;
	private TankMover _tankMover;
	private SpriteRenderer _renderer;
	private Timer _timerDamage;
	private Timer _timerSpeed;
	private Timer _timerShield;
	private int DefaultHealth = 3;
	private int DefaultDamage = 1;
	private float DefaulSpeed = 1f;
	private bool DefaulShield = false;
	private float TimeItem = 5;
	private Coroutine toggleCoroutineShield;
	private void Start()
	{
		_tank = new Tank
		{
			Name = "Tank",
			Direction = Direction.Down,
			Hp = DefaultHealth,
			Point = 0,
			Damage = DefaultDamage,
			Speed = DefaulSpeed,
			IsShield = DefaulShield,
			Position = new Vector3(0, -1, 0),
			Guid = GUID.Generate()
		};
		gameObject.transform.position = _tank.Position;
		_timerSpeed = gameObject.AddComponent<Timer>();
		_timerDamage = gameObject.AddComponent<Timer>();
		_timerShield = gameObject.AddComponent<Timer>();
		_tankMover = gameObject.GetComponent<TankMover>();
		_renderer = gameObject.GetComponent<SpriteRenderer>();
		Move(Direction.Down);
		_timerDamage.Duration = TimeItem;
		_timerSpeed.Duration = TimeItem;
		_timerShield.Duration = TimeItem;
		shield_1.SetActive(false);
		shield_2.SetActive(false);
	}

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			Move(Direction.Left);
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			Move(Direction.Down);
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			Move(Direction.Right);
		}
		else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			Move(Direction.Up);
		}

		if (Input.GetKey(KeyCode.Space))
		{
			Fire();
		}

		if (_timerDamage.Finished)
		{
			_tank.Damage = DefaultDamage;
			_timerDamage.Duration = TimeItem;
		}

		if (_timerSpeed.Finished)
		{
			_tank.Speed = DefaulSpeed;
			_timerSpeed.Duration = TimeItem;
		}

		if (_timerShield.Finished)
		{
			if (toggleCoroutineShield != null)
			{
				StopCoroutine(toggleCoroutineShield);
				if (shield_1.activeSelf == true || shield_2.activeSelf == true)
				{
					shield_1.SetActive(false);
					shield_2.SetActive(false);
				}
			}
		}
		PlayerPrefs.SetInt("tankDamage", GetDamage());
		PlayerPrefs.SetInt("tankHP", GetHP());
		Debug.Log("Tank Shield: " + _tank.IsShield);
	}
	public int GetDamage()
	{
		return _tank.Damage;
	}
	public int GetHP()
	{
		return _tank.Hp;
	}

	private void Move(Direction direction)
	{
		_tank.Position = _tankMover.Move(direction, _tank.Speed);
		_tank.Direction = direction;
		_renderer.sprite = direction switch
		{
			Direction.Down => tankDown,
			Direction.Up => tankUp,
			Direction.Left => tankLeft,
			Direction.Right => tankRight,
			_ => _renderer.sprite
		};
	}

	private void Fire()
	{
		var b = new Bullet
		{
			Direction = _tank.Direction,
			Tank = _tank,
			InitialPosition = _tank.Position
		};
		GetComponent<TankFirer>().Fire(b);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Item_PowerUp_Shot")
		{
			IncreaseDamageForTank();
		}
		else if (collision.gameObject.tag == "Item_PowerUp_Health")
		{
			IncreaseHpForTank();
		}
		else if (collision.gameObject.tag == "Item_PowerUp_Speed")
		{
			IncreaseSpeedForTank();
		}
		else if (collision.gameObject.tag == "Item_PowerUp_Shield")
		{
			CreateShieldForTank();
		}
		else if (collision.gameObject.tag == "bulletEnemy")
		{
			if (!_tank.IsShield)
			{
				int enemyDamage = PlayerPrefs.GetInt("enemyDamage");
				_tank.Hp -= enemyDamage;
				if (_tank.Hp <= 0)
				{
					Destroy(gameObject);
				}
			}
			else
			{
				Debug.Log("T có khiên :))");
			}
		}
		else if (collision.gameObject.tag == "Enemy")
		{
			Debug.Log("Va chạm với Enemy");
			_tank.Hp--;
			if (_tank.Hp <= 0)
			{
				Destroy(gameObject);
			}
		}
	}


	private void IncreaseHpForTank()
	{
		if (_tank.Hp < DefaultHealth)
		{
			_tank.Hp++;
			Debug.Log("Tank Hp: " + _tank.Hp);
		}
	}

	private void IncreaseDamageForTank()
	{
		_timerDamage.run();
		_tank.Damage = DefaultDamage + 1;
		Debug.Log("Tank Damage: " + _tank.Damage);
	}

	private void IncreaseSpeedForTank()
	{
		_timerSpeed.run();
		_tank.Speed = DefaulSpeed * 2;
		Debug.Log("Tank Speed: " + _tank.Speed);
	}


	private void CreateShieldForTank()
	{
		_timerShield.run();
		_tank.IsShield = true;
		Debug.Log("Tank Shield: " + _tank.IsShield);
		toggleCoroutineShield = StartCoroutine(ToggleShield());
	}

	private IEnumerator ToggleShield()
	{
		while (true)
		{
			shield_1.SetActive(true);

			yield return new WaitForSeconds(1.0f);

			shield_1.SetActive(false);
			shield_2.SetActive(true);

			yield return new WaitForSeconds(1.0f);

			shield_1.SetActive(true);
			shield_2.SetActive(false);

			yield return new WaitForSeconds(1.0f);
		}
	}
}