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
    private TankMoverConstruction _tankMover;
    private SpriteRenderer _renderer;
    private Vector2 cellSize;
    private Vector2Int gridSize;
    private void Start()
    {
        cellSize = new Vector2(0.32f, 0.32f);
        gridSize = new Vector2Int(12, 26);
        _tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = 10,
            Point = 0,
            Position = new Vector3(0.16f, 0.16f, 0),
            Guid = GUID.Generate()
        };        
        gameObject.transform.position = _tank.Position;
        _tankMover = gameObject.GetComponent<TankMoverConstruction>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        Move(Direction.Down);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Direction.Up);
        }
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
    public Vector3Int ConvertToGridPosition(Vector3 position)
    {
        int gridX = (int)Mathf.FloorToInt(position.x / cellSize.x);
        int gridY = (int)Mathf.FloorToInt(position.y / cellSize.y);
        return new Vector3Int(gridX, gridY, 0);
    }
}