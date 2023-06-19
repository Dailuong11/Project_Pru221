using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    // Start is called before the first frame update

    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;

    void Start()
    {
        saveScreenSize();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public Vector3 Move(Direction direction, float speed)
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
        Quaternion lockedRotation = transform.rotation; 
        lockedRotation.eulerAngles = new Vector3(lockedRotation.eulerAngles.x, lockedRotation.eulerAngles.y, 0);
        gameObject.transform.rotation = lockedRotation;
        bool isOver = false;
        if(currentPos.x >= screenLeft 
            && currentPos.x <= screenRight
            && currentPos.y >= screenBottom
            && currentPos.y <= screenTop)
        {
            isOver = false;
        } else
        {
            isOver = true;
        }
        if(!isOver)
        {
            gameObject.transform.position = currentPos;
        }

        return currentPos;
    }

    private void saveScreenSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }
}