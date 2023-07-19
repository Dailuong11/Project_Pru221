using DefaultNamespace;
using Entity;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update  
    public Sprite tankUp;
    public Sprite tankDown;
    public Sprite tankLeft;
    public Sprite tankRight;

    private Tank _tank;
    private TankMover _tankMover;
    private SpriteRenderer _renderer;
    private int DefaultHealth = 2;
    private int DefaultDamage = 1;
    private float DefaulSpeed = 1;

	private float timer = 0f;
	public float interval = 1f;
	public bool isRunning = true;
	private System.Random random;
	private int randomNumber = 1;

	[SerializeField] private AudioSource exploreSoundEffect;
	private void Start()
	{
		random = new System.Random();
		_tank = new Tank
		{
			Name = "Enemy",
			Direction = Direction.Down,
			Hp = DefaultHealth,
			Point = 0,
			Damage = DefaultDamage,
			Speed = DefaulSpeed,
		};
		_tankMover = gameObject.GetComponent<TankMover>();
		_renderer = gameObject.GetComponent<SpriteRenderer>();
		Move(Direction.Down);

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
		PlayerPrefs.SetInt("enemyDamage", GetDamage());

	}
	public int GetDamage()
	{
		return _tank.Damage;
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
		if (collision.gameObject.tag == "bullet")
		{
			int tankDamage = PlayerPrefs.GetInt("tankDamage");
			if (exploreSoundEffect) exploreSoundEffect.Play();
			_tank.Hp -= tankDamage;
			if (_tank.Hp <= 0)
			{
				Destroy(gameObject, 0.15f);
                int tankPoint = PlayerPrefs.GetInt("Point");
                PlayerPrefs.SetInt("Point", tankPoint + 1);
			}
		}
		else if (collision.gameObject.tag == "tank")
		{
			Debug.Log("Va chạm với Tank");
			_tank.Hp--;
			if (_tank.Hp <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}