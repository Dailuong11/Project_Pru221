using DefaultNamespace;
using Entity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TankControllerConstruction : MonoBehaviour
{
    // Start is called before the first frame update
    private Tank _tank;

    public Sprite tankUp;
    private TankMover _tankMover;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = 10,
            Point = 0,
            //Position = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0),
            Position = new Vector3(0, -1, 0),
            Guid = GUID.Generate()
        };
        gameObject.transform.position = _tank.Position;
        _tankMover = gameObject.GetComponent<TankMover>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        Move(Direction.Down);
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

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Fire();
        //}
    }

    private void Move(Direction direction)
    {
        _tank.Position = _tankMover.Move(direction);
        _tank.Direction = direction;
        _renderer.sprite = direction switch
        {
            Direction.Down => tankUp,
            Direction.Up => tankUp,
            Direction.Left => tankUp,
            Direction.Right => tankUp,
            _ => _renderer.sprite
        };
    }

    //private void Fire()
    //{
    //    var b = new Bullet
    //    {
    //        Direction = _tank.Direction,
    //        Tank = _tank,
    //        InitialPosition = _tank.Position
    //    };
    //    GetComponent<TankFirer>().Fire(b);
    //}
}