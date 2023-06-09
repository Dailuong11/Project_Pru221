﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnItem : MonoBehaviour
{
    public Tilemap tilemap;
    public List<RuleTile> items;

    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;
    private Vector3 cellSize;
    private Timer timer;
    private float timeSpawnItem = 3;
    private List<Vector3Int> listPos;
    void Start()
    {
        cellSize = tilemap.cellSize;
        timer = gameObject.AddComponent<Timer>();      
        timer.Duration = timeSpawnItem;
        timer.run();
        saveScreenSize();
    }

    void Update()
    {
        if(timer.Finished)
        {
            bool isContinue = false;
            Transform parentTransform = transform;
            listPos = new List<Vector3Int>();
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform childTransform = parentTransform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                Vector3Int materialPos = ConvertToGridPosition(childObject.transform.position);
                listPos.Add(materialPos);
                Debug.Log(materialPos);
            }
            do
            {
                Vector3 temp = new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop));
                Vector3Int randomPosItem = ConvertToGridPosition(temp);
                bool isExist = false;

                foreach (Vector3Int item in listPos)
                {
                    if(item.Equals(randomPosItem))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist == false)
                {
                    int index = Random.Range(0, items.Count);
                    tilemap.SetTile(randomPosItem, items[index]);
                    isContinue = false;
                }
                else
                {
                    isContinue = true;
                }
            } while (isContinue);
            timer.Duration = timeSpawnItem;
            timer.run();
        }
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

    public Vector3Int ConvertToGridPosition(Vector3 position)
    {
        int gridX = (int)Mathf.FloorToInt(position.x / cellSize.x);
        int gridY = (int)Mathf.FloorToInt(position.y / cellSize.y);

        return new Vector3Int(gridX, gridY, 1);
    }
}
