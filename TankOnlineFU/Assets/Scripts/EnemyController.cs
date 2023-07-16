using DefaultNamespace;
using Entity;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
    private int DefaultHealth = 10;
    private int DefaultDamage = 1;
    private float DefaulSpeed = 1;
    private float TimeItem = 5;
    private Coroutine toggleCoroutineShield;

    private float timer = 0f;
    public float interval = 1f;
    public bool isRunning = true;
    private System.Random random;
    private int randomNumber = 1;
    private void Start()
    {
        random = new System.Random();
        _tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = DefaultHealth,
            Point = 0,
            Damage = DefaultDamage,
            Speed = DefaulSpeed,
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

        randomNumber = random.Next(1, 5);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isRunning)
        {

            timer += Time.deltaTime;
            if (timer <= interval)
            {
                if (randomNumber == 1)
                {
                    Fire();
                    Move(Direction.Left);
                }
                else if (randomNumber == 2)
                {
                    Fire();
                    Move(Direction.Right);
                }
                else if (randomNumber == 3)
                {
                    Fire();
                    Move(Direction.Up);
                }
                else if (randomNumber == 4)
                {
                    Fire();
                    Move(Direction.Down);
                }

            }
            else
            {
                timer = 0f;
                randomNumber = random.Next(1, 5);
            }
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
    }


    private void IncreaseHpForTank()
    {
        if (_tank.Hp < DefaultHealth)
        {
            _tank.Hp++;
        }
    }

    private void IncreaseDamageForTank()
    {
        _timerDamage.run();
        _tank.Damage = DefaultDamage + 1;
    }

    private void IncreaseSpeedForTank()
    {
        _timerSpeed.run();
        _tank.Speed = DefaulSpeed * 2;
    }

    private void CreateShieldForTank()
    {
        _timerShield.run();
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