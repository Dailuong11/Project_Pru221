using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public Vector3 Move(Direction direction)
    {
        var currentPos = gameObject.transform.position;
        switch (direction)
        {
            case Direction.Down:
                currentPos.y -= speed*Time.deltaTime;
                break;
            case Direction.Left:
                currentPos.x -= speed*Time.deltaTime;
                break;
            case Direction.Right:
                currentPos.x += speed*Time.deltaTime;
                break;
            case Direction.Up:
                currentPos.y += speed*Time.deltaTime;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        gameObject.transform.position = currentPos;
        Quaternion lockedRotation = transform.rotation;  // L?y quay hi?n t?i c?a ??i t??ng
        lockedRotation.eulerAngles = new Vector3(lockedRotation.eulerAngles.x, lockedRotation.eulerAngles.y, 0);  // ??t g�c quay Z mong mu?n
        gameObject.transform.rotation = lockedRotation;
        return currentPos;
    }
}